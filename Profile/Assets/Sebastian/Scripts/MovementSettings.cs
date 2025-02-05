using UnityEngine;

namespace Movement
{
    [CreateAssetMenu]
    internal class MovementSettings : MonoBehaviour
    {

        [Header("LAYERS")]
        [Tooltip("Vilket layer marken är")]
        [SerializeField] internal LayerMask GroundLayer;

        [Header("DETECTION")]
        [Tooltip("Hur långt ifrån spelaren den ska kolla efter tak/mark")]
        [SerializeField] internal float DetectionDistance;

        [Header("GRAVITY")]
        [Tooltip("Den snabbaste hastigheten spelaren kan falla")]
        [SerializeField] internal float TerminalVelocity;
        [Tooltip("Hur snabbt man accelererar när man faller")]
        [SerializeField] internal float AccelerationWhileFalling;
        [Tooltip("Hur mycket gravitation som ska sättas på spelaren när den är på marken")]
        [SerializeField] internal float GroundGravity;

        [Header("MOVEMENT")]
        [Tooltip("Hastigheten spelaren ska röra sig på")]
        [SerializeField] internal float Speed;
        [Tooltip("Hur snabbt spelaren accelererar")]
        [SerializeField] internal float Acceleration;
        [Tooltip("Hur snabbt spelaren saktar ner på marken")]
        [SerializeField] internal float OnGroundDeceleration;
        [Tooltip("Hur snabbt spelaren saktar ner i luften")]
        [SerializeField] internal float InAirDeceleration;

        [Header("JUMP")]
        [Tooltip("Hur starkt hoppet ska vara")]
        [SerializeField] internal float JumpPower;
        [Tooltip("Hur mycket spelaren ska tryckas ned när den släpper hopp")]
        [SerializeField] internal float JumpEndedEarlyModifier;
        [Tooltip("Hur långt in i hoppet spelaren ska vara innan den temporärt stannar")]
        [SerializeField] internal float HangInAirThreshold;
        [Tooltip("Hur mycket spelaren ska saktas ned vid toppen av hoppet")]
        [SerializeField] internal float HangInAirMultiplier;

    }
}
