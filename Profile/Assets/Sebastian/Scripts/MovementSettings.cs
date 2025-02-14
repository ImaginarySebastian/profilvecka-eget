using UnityEngine;

namespace PlayerScript
{
    [CreateAssetMenu]
    internal class MovementSettings : MonoBehaviour
    {

        [Header("LAYERS")]
        [Tooltip("Vilket layer marken är")]
        [SerializeField] internal LayerMask GroundLayer;

        [Header("DETECTION")]
        [Tooltip("Hur långt ifrån spelaren den ska kolla efter tak/mark")]
        [SerializeField] internal float DetectionDistance = 0.05f;

        [Header("GRAVITY")]
        [Tooltip("Den snabbaste hastigheten spelaren kan falla")]
        [SerializeField] internal float TerminalVelocity = 40f;
        [Tooltip("Hur snabbt man accelererar när man faller")]
        [SerializeField] internal float AccelerationWhileFalling = 60f;
        [Tooltip("Hur mycket gravitation som ska sättas på spelaren när den är på marken")]
        [SerializeField] internal float GroundGravity = 1.5f;

        [Header("MOVEMENT")]
        [Tooltip("Hastigheten spelaren ska röra sig på")]
        [SerializeField] internal float Speed = 3.5f;
        [Tooltip("Hur snabbt spelaren accelererar")]
        [SerializeField] internal float Acceleration = 120f;
        [Tooltip("Hur snabbt spelaren saktar ner på marken")]
        [SerializeField] internal float OnGroundDeceleration = 60f;
        [Tooltip("Hur snabbt spelaren saktar ner i luften")]
        [SerializeField] internal float InAirDeceleration = 30f;

        [Header("JUMP")]
        [Tooltip("Hur starkt hoppet ska vara")]
        [SerializeField] internal float JumpPower =30f;
        [Tooltip("Hur mycket spelaren ska tryckas ned när den släpper hopp")]
        [SerializeField] internal float JumpEndedEarlyModifier = 3f;
        [Tooltip("Hur långt in i hoppet spelaren ska vara innan den temporärt stannar")]
        [SerializeField] internal float HangInAirThreshold = 0.5f;
        [Tooltip("Hur mycket spelaren ska saktas ned vid toppen av hoppet")]
        [SerializeField] internal float HangInAirMultiplier = 0.2f;

    }
}
