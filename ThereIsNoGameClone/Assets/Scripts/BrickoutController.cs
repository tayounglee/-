using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickoutController : MonoBehaviour
{
    ClickManager clickManager;
    public bool isBrickout;
    Rigidbody2D rb2d;
    float direction;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 dir = collision.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        Vector2 incomingVector = dir;
        incomingVector = incomingVector.normalized;
        Vector2 normalVector = collision.contacts[0].normal;
        Vector2 reflectVector = Vector2.Reflect(incomingVector, normalVector);
        Debug.Log(reflectVector);
        dir = reflectVector.normalized;

        switch (collision.collider.tag)
        {
                case "Floor":
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 5f);
                    break;
                case "Floor2":
                    break;
                case "Roof":
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 5f);
                    break;
                case "Roof2":
                    break;
                case "Left Wall":
                    //direction = 2f;
                    break;
                case "Right Wall":
                    //direction = -2f;
                    break;
                default:
                    break;
        }
    }


    void Awake()
    {
        isBrickout = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = -2f;
        clickManager = GetComponent<ClickManager>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickManager.TriggerCount == 1)
        {
            isBrickout = true;
            clickManager.enabled = false;
        }

        if(isBrickout && gameObject.name == "TitleCube_Ball_Rebond")
        {
            //transform.position *= dir * speed * Time.deltaTime;
            rb2d.velocity = new Vector2(direction, rb2d.velocity.y);
        }
    }
}