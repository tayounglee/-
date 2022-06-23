using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    int ClickCount;
    public int TriggerCount;
    Camera mainCamera;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Floor")
        {
            TriggerCount += 1;
        }
    }

    void Awake()
    {
        TriggerCount = 0;
        ClickCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            //RaycastHit hit;
            //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (hit.collider != null)
            {
                ClickCount += 1;
                if(ClickCount == 3)
                {
                    if(gameObject.name == "TitleCube_Excla")
                    {
                        hit.transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                    else
                    {
                        hit.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    
                    hit.collider.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    ClickCount = 0;
                }
                else
                {
                    Physics2D.IgnoreLayerCollision(6, 7, true);
                    hit.transform.localPosition += new Vector3(0f, -0.5f, 0f);
                    Quaternion originalRotation = hit.transform.rotation;
                    Quaternion plusRotation = Quaternion.Euler(new Vector3(0, 0, -5f));
                    Quaternion targetRotation = originalRotation * plusRotation;
                    hit.transform.rotation = targetRotation;
                }
            }
        }
    }
}