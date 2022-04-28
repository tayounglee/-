using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    float waitTime;
    float rightMax; //좌로 이동가능한 (x)최대값
    float leftMax; //우로 이동가능한 (x)최대값
    float currentPositionX; //현재 위치(x) 저장
    float currentPositionY; //현재 위치(y) 저장
    float direction; //이동속도+방향
    
    void Start()
    {
        rightMax = 1.1f;
        leftMax = 0.6f;
        currentPositionX = transform.position.x;
        currentPositionY = transform.position.y;
        direction = 0.1f;
    }
    
    void Update()
    {
        currentPositionX += Time.deltaTime * direction;

        if (currentPositionX >= rightMax)
        {
            direction *= -1;
            currentPositionX = rightMax;
        }
        //현재 위치(x)가 우로 이동가능한 (x)최대값보다 크거나 같다면
        //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 우로 이동가능한 (x)최대값으로 설정
        else if (currentPositionX <= leftMax)
        {
            direction *= -1;
            currentPositionX = leftMax;
        }

        //현재 위치(x)가 좌로 이동가능한 (x)최대값보다 크거나 같다면
        //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 좌로 이동가능한 (x)최대값으로 설정
        transform.position = new Vector3(currentPositionX, currentPositionY, 0);
    }
}
