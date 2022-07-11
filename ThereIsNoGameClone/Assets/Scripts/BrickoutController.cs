using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickoutController : MonoBehaviour
{
    ClickManager clickManager;
    public static bool isBrickout;
    public static bool isMyTitle;
    Rigidbody2D rb2d;
    Vector2 direction;
    Vector2 lastVelocity;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border" || collision.gameObject.tag == "Title")
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 velocity = lastVelocity;
            Vector2 velocityNormal = velocity.normalized;
            rb2d.velocity = Vector2.Reflect(velocityNormal, normal) * velocity.magnitude;
            if(collision.gameObject.tag == "Title")
            {
                Vector2 dir = transform.position - collision.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                if(angle >=0 && angle <= 90)
                {
                    collision.transform.localPosition += new Vector3(0, -1, 0);
                }
                else if(angle > 90 && angle <= 180)
                {
                    collision.transform.localPosition += new Vector3(0, -1, 0);
                }
                else if(angle < 0 && angle >= -90)
                {
                    collision.transform.localPosition += new Vector3(0, 1, 0);
                }
                else if(angle < -90 && angle >= -180)
                {
                    collision.transform.localPosition += new Vector3(0, 1, 0);
                }
            }
        }
    }


    void Awake()
    {
        isMyTitle = false;
        isBrickout = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        clickManager = GetComponent<ClickManager>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = GetComponent<Rigidbody2D>().velocity;
        direction = rb2d.velocity;

        if (clickManager.TriggerCount == 1 && name == "TitleCube_Ball_Rebond")
        {
            isBrickout = true;
            isMyTitle = true;
            clickManager.enabled = false;
            direction.x = -2f;
        }

        if(isBrickout && gameObject.name == "TitleCube_Ball_Rebond")
        {
            if(clickManager.TriggerCount == 1)
            {
                rb2d.velocity = direction;
            }
            
            if (rb2d.velocity.x > 0 && rb2d.velocity.x <= 2)
            {
                direction.x = 2f;
            }
            if (rb2d.velocity.x <= 0 && rb2d.velocity.x >= -2)
            {
                direction.x = -2f;
            }
            rb2d.velocity = direction;
        }

        if (isBrickout && gameObject.name == "TitleCube_Excla")
        {
            enabled = false;
        }

    }
}