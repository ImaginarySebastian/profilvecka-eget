using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("RemoveBullet", 5);
    }

    void RemoveBullet()
    {
        Destroy(gameObject);
    }
}
