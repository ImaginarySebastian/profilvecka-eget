using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerScript
{
    public class PowerUps : MonoBehaviour
    {
        private PlayerMovement playerMovementScript;
        private MovementSettings movementSettingsScript;

        private void Start()
        {
            playerMovementScript = GameObject.FindObjectOfType<PlayerMovement>();
            movementSettingsScript = GameObject.FindObjectOfType<MovementSettings>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("DoubleJump"))
            {
                playerMovementScript.doubleJumps += 1;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("SpeedUp"))
            {
                movementSettingsScript.Speed += 0.5f;
                Invoke("RestoreSpeed", 10);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("JumpBoost"))
            {
                movementSettingsScript.JumpPower += 10f;
                Invoke("RestoreJump", 10);
                Destroy(collision.gameObject);
            }
        }

        void RestoreSpeed()
        {
            movementSettingsScript.Speed -= 0.5f;
        }
        void RestoreJump()
        {
            movementSettingsScript.JumpPower -= 10f;
        }
    }
}
