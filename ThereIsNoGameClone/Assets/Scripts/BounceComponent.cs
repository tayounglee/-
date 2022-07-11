using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceComponent : MonoBehaviour
{
    float y;
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        y = 9.0f;
        var rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "TitleCube_Ball_Rebond")
        {
            if (name == "TitleCube_Excla" && collision.gameObject.name == "TitleCube_Ball_Rebond")
            {
                y = 11.0f;
            }
            velocity = collision.rigidbody.velocity;
            velocity.y = y;
            collision.rigidbody.velocity = velocity;

            y = 7.0f;

        }

        if (name == "TitleCube_Excla")
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }

       
    }
}