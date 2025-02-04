using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class Movement : MonoBehaviour
    {
        public bool _playerCanMove = true;

        [SerializeField] Stats _stats;
        [SerializeField] new private CapsuleCollider2D collider;
        [SerializeField] new private Rigidbody2D rigidbody;


        private Vector2 _movementVelocity;
        private bool grounded;
        private float timeLeftGround;
        private float horizontal;
        private bool jumpEndedEarly;
        private bool pressedJump;
        private bool isAtPeakJump;
        private bool hasReleasedJump = true;
        private bool alwaysTrue = true;
       

        private bool doubleJumpOffCooldown = true;
        private float doubleJumpsLeft;
        private bool gliding;
        private float yLevelOnDoubleJump;

        public bool isFacingRight = true;

        private bool isGrabbing;
        private bool canGrab;
        private Vector2 direction;

        private float colliderRadius = 0.1f;
        
        private float whenWasGrabBuffered;
        private bool hasResetVelocity;


        void Start()
        {
            _playerCanMove = true;
        }

        void Update()
        {

            if((!isFacingRight &&  horizontal > 0f) || isFacingRight && horizontal < 0f) Flip();


        }

        #region Input



        public void JumpInput(InputAction.CallbackContext context) 
        {
            
            if (context.performed && _playerCanMove)
            {
                pressedJump = true;
            }
            else if (context.canceled || _playerCanMove) 
            {
                pressedJump = false;
                gliding = false;
                hasReleasedJump = true;
            }
            jumpToUse = true;
            timeAtJump = Time.time;

        }

        #endregion


        private void FixedUpdate()
        {
            CollisionCheck();
            HandleDirection();
            CheckJump();
            Gravity();
            Move();          
        }





        #region Collision Check

        private void CollisionCheck() 
        {
            Physics2D.queriesStartInColliders = false;
            Vector2 rightOffset = new Vector2(0.4f, 0f);
            Vector2 leftOffset = new Vector2(-0.4f, 0f);
            

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(collider.bounds.center, collider.size, collider.direction, 0, Vector2.down, _stats.DetectionDistanceY, _stats.GroundLayer);
            bool ceilingHit = Physics2D.CapsuleCast(collider.bounds.center, collider.size, collider.direction, 0, Vector2.up, _stats.DetectionDistanceY, _stats.GroundLayer);


            // Hit a Ceiling
            if (ceilingHit) _movementVelocity.y = Mathf.Min(0, _movementVelocity.y);

            // Landed on the Ground
            if (!grounded && groundHit)
            {
                doubleJumpOffCooldown = true;
                grounded = true;
                coyoteAllowed = true;
                canUseBufferedJump = true;
                jumpEndedEarly = false;
                gliding = false;
                doubleJumpsLeft = _stats.MaxDoubleJumps;
            }
            else if (grounded && !groundHit)
            {
                timeLeftGround = Time.time;
                grounded = false;
            }

        }

        #endregion

        #region Movement

        private void HandleDirection() 
        {
            if (horizontal == 0) 
            {
                var deceleration = grounded ? _stats.OnGroundDeceleration : _stats.InAirDeceleration;
                _movementVelocity.x = Mathf.MoveTowards(_movementVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else 
            {
                _movementVelocity.x = Mathf.MoveTowards(_movementVelocity.x, horizontal * _stats.Speed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }



        #endregion

        #region Jump

        private bool jumpToUse;
        private bool canUseBufferedJump;
        private bool endedJumpEarly;
        private bool coyoteAllowed;
        private float timeAtJump;

        private bool HasBufferedJump => canUseBufferedJump && Time.time < timeAtJump + _stats.JumpBufferTime;
        private bool CanUseCoyote => coyoteAllowed && !grounded && Time.time < timeLeftGround + _stats.CoyoteTime;
        private bool AllowNormalJump => (pressedJump && grounded || pressedJump && CanUseCoyote) && hasReleasedJump;
        

        private void CheckJump() 
        {
            float modifier = 1f;

            if(!jumpEndedEarly && !grounded && !pressedJump && rigidbody.velocity.y > 0) 
            {
                jumpEndedEarly = true;
            }
            if (!jumpToUse) return;
            if (AllowNormalJump) Jump(modifier);
            if (!AllowNormalJump && !grounded)
            {
                if(pressedJump)
                {
                    // ÄNDRA HÄR
                    if(doubleJumpOffCooldown && hasReleasedJump && doubleJumpsLeft > 0)
                    {
                        doubleJumpOffCooldown = false;
                        doubleJumpsLeft--;
                        yLevelOnDoubleJump = transform.position.y;
                        modifier = 0.80f;
                        Jump(modifier);
                    }
                    else if (!doubleJumpOffCooldown) 
                    {
                        if (yLevelOnDoubleJump + _stats.DoubleJumpThreshold >= transform.position.y && hasReleasedJump && doubleJumpsLeft > 0)
                        {
                            doubleJumpOffCooldown = false;
                            doubleJumpsLeft--;
                            modifier = 0.5f;
                            yLevelOnDoubleJump = transform.position.y;
                            Jump(modifier);
                            return;
                        }
                        else if (rigidbody.velocity.y < 0) 
                        {
                            gliding = true;
                        }
                    }

                }
            }
        }

        private void Jump(float modifier) 
        {
            jumpEndedEarly = false;
            timeAtJump = 0;
           // canUseBufferedJump = false;
            coyoteAllowed = false;
            _movementVelocity.y = _stats.JumpPower * modifier;
            hasReleasedJump = false;
        }

        public void getJumpDirection(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
        }

        public void JumpGrabInput(InputAction.CallbackContext context)
        {
            if (context.performed && isGrabbing && hasReleasedJump)
            {
                JumpAway();
            }
        }

        #endregion

        private void CheckForGrab() 
        {
            
            if ((whenWasGrabBuffered + _stats.grabBuffer) > Time.time && canGrab) 
            {
                isGrabbing = true;
                whenWasGrabBuffered = 0;
            }
        }

        public void Grab(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (canGrab)
                {
                    isGrabbing = !isGrabbing;
                }
                else 
                {
                    whenWasGrabBuffered = Time.time;
                }
            }
        }

        #region Gravity

        private void Gravity()
        {
            
            if (grounded && _movementVelocity.y <= 0f)
            {
                _movementVelocity.y = _stats.GroundGravity;
            }

            else
            {
                float inAirGravity = _stats.AccelerationWhileFalling;

                if (!grounded && Mathf.Abs(rigidbody.velocity.y) < _stats.HangInAirThreshold)
                {
                    inAirGravity = inAirGravity * _stats.HangInAirMultiplier;
                }
                else if (jumpEndedEarly && _movementVelocity.y > 0)
                {
                    inAirGravity *= _stats.JumpEndEarlyModifier;
                }
                if(!gliding)
                {
                    _movementVelocity.y = Mathf.MoveTowards(_movementVelocity.y, -_stats.TerminalVelocity, inAirGravity * Time.fixedDeltaTime);
                    
                }
                else if(gliding)
                {
                     _movementVelocity.y = Mathf.MoveTowards(0, -_stats.GlideFallSpeed, _stats.AccelerationWhileFalling);
                }
                
            }
        }

        #endregion
        private void Move()
        {

                if (!gliding)
                {
                    rigidbody.velocity = new Vector2(_movementVelocity.x * _stats.Speed, _movementVelocity.y);
                }
                else
                {
                    rigidbody.velocity = new Vector2((_movementVelocity.x * _stats.Speed) * _stats.GlideMoveSpeedModifier, _movementVelocity.y);
                }
            
        }
    }
}
