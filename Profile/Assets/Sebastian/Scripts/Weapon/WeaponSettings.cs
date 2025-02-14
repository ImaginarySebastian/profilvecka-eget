using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu]
    internal class WeaponSettings : MonoBehaviour
    {

        [SerializeField] internal float BulletSpeed;
        [SerializeField] internal float BulletAmmount;
        [SerializeField] internal float BulletSpread;
        [SerializeField] internal float BulletCooldown;
    }
}
