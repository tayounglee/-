using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    [SerializeField]
    Vector3 moveY;

    public static List<ObjectShake> obj = new List<ObjectShake>();
    float currentPositionX; //현재 위치(x) 저장
    float currentPositionZ; //현재 위치(y) 저장

    // Start is called before the first frame update
    void Start()
    {
        obj.Add(this);
        enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        currentPositionX = transform.localPosition.x;
        currentPositionZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = moveY;
        transform.rotation = Quaternion.Euler(0, 0, -30);
        GetComponent<BoxCollider2D>().enabled = true;
        enabled = false;
    }
}