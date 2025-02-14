using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class Weapons : MonoBehaviour
    {
        [SerializeField] WeaponSettings _settings;
        [SerializeField] GameObject _bullet;
        Rigidbody2D rigidbody;
        GameObject player;
        
        bool allowShot = true;
        bool isEquiped;
        bool allowPickup = true;

        private void Start()
        {
            player = GameObject.Find("Spelare");
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Fire(InputAction.CallbackContext context)
        {
            if(context.performed && allowShot && isEquiped)
            {
                Debug.Log("Shot");
                for (int i = 0; i < _settings.BulletAmmount; i++)
                {
                    float random = Random.Range(-_settings.BulletSpread, _settings.BulletSpread);
                    /*Lägg till så att de är riktade rätt*/
                    
                    Quaternion rotation = new Quaternion(_bullet.transform.rotation.x, _bullet.transform.rotation.y, random, _bullet.transform.rotation.w);
                    Quaternion inverseRotation = new Quaternion(_bullet.transform.rotation.x, _bullet.transform.rotation.y, random, -_bullet.transform.rotation.w);
                    Vector2 force = new Vector2(1f, random);
                    GameObject bullet;
                    if (player.GetComponent<PlayerMovement>()._isFacingRight)
                    {
                        bullet = Instantiate(_bullet, transform.position, rotation);
                        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                        rigidbody.AddForce(force * _settings.BulletSpeed, ForceMode2D.Impulse);
                    }
                    else
                    {
                        bullet = Instantiate(_bullet, transform.position, inverseRotation);
                        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                        rigidbody.AddForce(-force * _settings.BulletSpeed, ForceMode2D.Impulse);
                    }
                    
                }
                allowShot = false;
                Invoke("CoolDown", _settings.BulletCooldown);
            }

        }
        void CoolDown()
        {
            allowShot = true;
        }
        void PickupCooldown()
        {
            allowPickup = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<WeaponHandle>().hasWeapon == false && allowPickup)
                {
                    player.GetComponent<WeaponHandle>().hasWeapon = true;
                    transform.SetParent(collision.transform);
                    rigidbody.simulated = false;
                    transform.localPosition = new Vector3(1f, 0.5f, 0);
                    isEquiped = true;
                }
            }
        }

        public void DropWeapon(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                isEquiped = false;
                allowPickup = false;
                transform.SetParent(null);
                rigidbody.simulated = true;
                
                player.GetComponent<WeaponHandle>().hasWeapon = false;
                Invoke("PickupCooldown", 1);
            }
        }


    }
}
