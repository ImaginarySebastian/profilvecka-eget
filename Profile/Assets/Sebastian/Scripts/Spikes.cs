using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    
    Vector2 targetPosDown = new Vector2(0f, -1.05f);
    Vector2 targetPosUp = new Vector2(0f, -0.3f);
    bool shouldMoveUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("Move", 4);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMoveUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosUp, 0.1f);
        }
        else if (shouldMoveUp == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosDown, 0.1f);
        }
    }

    void Move()
    {
        shouldMoveUp = !shouldMoveUp;
        Invoke("Move", 4);
    }
}
