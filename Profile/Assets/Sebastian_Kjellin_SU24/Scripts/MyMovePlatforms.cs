using UnityEngine;

public class MyMovePlatforms : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Vector3 _startPos;
    [SerializeField] Vector3 _endPos;
    [SerializeField] bool goesUp;
    [SerializeField] bool inverted;
    bool playerIsTouching;
    bool running;
    bool isAtEnd = false;
    bool ShouldMoveBackward;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(inverted)
        {
            MoveInverted();
        }
        else
        {
            MoveNormal();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerIsTouching = true;
            running = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerIsTouching = false;
        }
    }

    void MoveNormal()
    {
        if (running && !isAtEnd)
        {
            if (goesUp)
            {
                rigidbody.velocity = new Vector2(0f, 1.5f);
            }
            else if (!goesUp)
            {
                rigidbody.velocity = new Vector2(1.5f, 0);
            }
        }
        if ((goesUp && (transform.position.y - _endPos.y) >= 0) || (!goesUp && (transform.position.x - _endPos.x) >= 0))
        {
            rigidbody.velocity = new Vector2(0f, 0f);
            isAtEnd = true;
            running = false;
        }
    }
    void MoveInverted()
    {
        if (running && !isAtEnd)
        {
            if (goesUp)
            {
                rigidbody.velocity = new Vector2(0f, -1.5f);
            }
            else if (!goesUp)
            {
                rigidbody.velocity = new Vector2(-1.5f, 0);
            }
        }
        if ((goesUp && (_endPos.y - transform.position.y) >= 0) || (!goesUp && (_endPos.x - transform.position.x) >= 0))
        {
            rigidbody.velocity = new Vector2(0f, 0f);
            isAtEnd = true;
            running = false;
        }
    }
}
