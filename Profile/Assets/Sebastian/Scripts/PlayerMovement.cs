using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScript
{
    public class PlayerMovement : MonoBehaviour
    {
        /*SerializeFields*/
        [SerializeField] MovementSettings _settings;
        [SerializeField] new private CapsuleCollider2D _collider;
        [SerializeField] new private Rigidbody2D _rigidbody;

        /*Variabler*/
        public bool _isFacingRight = true;

        /*hoppa*/

        private bool _jumpEndedEarly;
        private bool _pressedJump;
        private bool _grounded;
        internal int doubleJumps;
        private float _horizontal;
        private bool _hasReleasedForDouble;
        private Vector2 _movementVelocity;



    #region Input

            public void JumpInput(InputAction.CallbackContext context)
            {
                if (context.performed)
                {
                    _pressedJump = true;
                }
                else if (context.canceled)
                {
                    _pressedJump = false;
                if (!_grounded) _hasReleasedForDouble = true;
                }
            }
            public void MoveInput(InputAction.CallbackContext context)
            {
                _horizontal = context.ReadValue<Vector2>().x;
            }

    #endregion

        void Update()
        {
            if ((!_isFacingRight && _horizontal > 0f) || (_isFacingRight && _horizontal < 0f)) Flip();
        }
        void FixedUpdate()
        {
            CollisionCheck();
            HandleDirection();
            CheckJump();
            Gravity();
            Move();
        }

#region CollisionCheck

        void CollisionCheck()
        {
            Physics2D.queriesStartInColliders = false;

            bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, _settings.DetectionDistance, _settings.GroundLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, _settings.DetectionDistance, _settings.GroundLayer);

            if (ceilingHit) _movementVelocity.y = Mathf.Min(0, _movementVelocity.y);
    
        if (!_grounded && groundHit)
            {
                _grounded = true;
                _jumpEndedEarly = false;
                _hasReleasedForDouble = false;
            }
            else if (_grounded && !groundHit)
            {
                _grounded = false;
            }
        }

#endregion

#region Movement

        void HandleDirection()
        {
            if (_horizontal == 0)
            {
                var deceleration = _grounded ? _settings.OnGroundDeceleration : _settings.InAirDeceleration;
                _movementVelocity.x = Mathf.MoveTowards(_movementVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _movementVelocity.x = Mathf.MoveTowards(_movementVelocity.x, _horizontal * _settings.Speed, _settings.Acceleration * Time.fixedDeltaTime);
            }
        }

        void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

#endregion

#region Jump

        private bool AllowJump => (_pressedJump && _grounded);

        void CheckJump()
        {

            if(!_jumpEndedEarly && !_grounded && !_pressedJump && _rigidbody.velocity.y > 0f)
            {
                _jumpEndedEarly = true;
            }
            if(AllowJump) Jump();
            if(doubleJumps > 0 && _pressedJump && _hasReleasedForDouble)
            {
                doubleJumps--;
                Jump();
            }

        }

        void Jump()
        {
            _jumpEndedEarly = false;
            _movementVelocity.y = _settings.JumpPower;
        }

#endregion

#region Gravity

        void Gravity()
        {
            if(_grounded && _movementVelocity.y <= 0f)
            {
                _movementVelocity.y = _settings.GroundGravity;
            }
            else
            {
                float inAirGravity = _settings.AccelerationWhileFalling;

                if(!_grounded && Mathf.Abs(_rigidbody.velocity.y) < _settings.HangInAirThreshold && !_jumpEndedEarly)
                {
                    inAirGravity = inAirGravity * _settings.HangInAirMultiplier;
                }
                else if(_jumpEndedEarly && _movementVelocity.y > 0f)
                {
                    inAirGravity *= _settings.JumpEndedEarlyModifier;
                }
                _movementVelocity.y = Mathf.MoveTowards(_movementVelocity.y, -_settings.TerminalVelocity, inAirGravity * Time.fixedDeltaTime);
            }
        }

#endregion


        void Move()
        {
            _rigidbody.velocity = new Vector2(_movementVelocity.x * _settings.Speed, _movementVelocity.y);
        }

    }
}
