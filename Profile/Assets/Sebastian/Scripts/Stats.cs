using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovement
{
    [CreateAssetMenu]
    internal class Stats : MonoBehaviour
    {
        
        
        

        [Header("LAYERS")]
        [Tooltip("Set this to the layer the ground is on")]
        [SerializeField] internal LayerMask GroundLayer;

        [Header("Detection")]
        [Tooltip("The detection distance for grounding and roof detection")]
        [SerializeField] internal float DetectionDistanceY = 0.05f;
        [Tooltip("How far away should be counted as on the wall")]
        [SerializeField] internal float TouchingWallDistance = 0.05f;
        [Tooltip("The detection distance for walls")]
        [SerializeField] internal float GrabbingDistance = 1f;

        [Header("Gravity")]
        [Tooltip("The fastest speed that the object can fall")]
        [SerializeField] internal float TerminalVelocity = 40f;
        [Tooltip("How fast you accelerate when you fall")]
        [SerializeField] internal float AccelerationWhileFalling = 60f;
        [Tooltip("How much gravity is applied on the ground. Should be negative")]
        [SerializeField] internal float GroundGravity = -1.5f;

        [Header("Movement")]
        [Tooltip("How fast the player can move")]
        [SerializeField] internal float Speed = 3.5f;
        [Tooltip("How fast the player gains speed")]
        [SerializeField] internal float Acceleration = 120f;
        [Tooltip("How fast the player loses speed while on the ground")]
        [SerializeField] internal float OnGroundDeceleration = 60f;
        [Tooltip("How fast the player loses speed while in the air")]
        [SerializeField] internal float InAirDeceleration = 30f;


        [Header("Jump")]
        [Tooltip("to lazy to fix, do not use <3 <3")]
        [SerializeField] internal float JumpBufferTime = 0.01f;
        [Tooltip("The time you have to jump after leaving ground")]
        [SerializeField] internal float CoyoteTime = 0.2f;
        [Tooltip("How strong the jump is")]
        [SerializeField] internal float JumpPower = 30f;
        [Tooltip("How much gravity should be increased with when releasing jump")]
        [SerializeField] internal float JumpEndEarlyModifier = 3f;
        [Tooltip("How far in the jump the player should be before temporary stopping")]
        [SerializeField] internal float HangInAirThreshold = 0.5f;
        [Tooltip("How much you should slow down while being at the top of your jump")]
        [SerializeField] internal float HangInAirMultiplier = 0.2f;

        [Header("DoubleJump")]
        [Tooltip("The ammount of times you can doublejump")]
        [SerializeField] internal float MaxDoubleJumps = 3;
        [Tooltip("When in your doublejump the next one should be available")]
        [SerializeField] internal float DoubleJumpThreshold = 0.5f;


        [Header("Gliding")]
        [Tooltip("How fast you should glide")]
        [SerializeField] internal float GlideFallSpeed = 10;
        [Tooltip("How fast you should be able to move horizontally when gliding")]
        [SerializeField] internal float GlideMoveSpeedModifier = 0.25f;

        [Header("Other")]
        [Tooltip("How long the dash should be")]
        [SerializeField] internal float DashTime = 0.25f;
        [Tooltip("How long you should be able to buffer a grab for")]
        [SerializeField] internal float grabBuffer = 0.3f;
    }
}
