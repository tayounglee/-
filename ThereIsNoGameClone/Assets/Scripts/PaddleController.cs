using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float PaddleSpeed = 0.1f;
    private Vector3 playerPos;

    // Update is called once per frame
    private void Update()
    {
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * PaddleSpeed);
        playerPos = transform.position;
        playerPos.x = Mathf.Clamp(xPos, -2f, 2f);
        transform.position = playerPos;
    }
}
