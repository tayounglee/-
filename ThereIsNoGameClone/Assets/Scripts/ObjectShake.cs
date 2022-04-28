using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    float waitTime;
    float rightMax; //�·� �̵������� (x)�ִ밪
    float leftMax; //��� �̵������� (x)�ִ밪
    float currentPositionX; //���� ��ġ(x) ����
    float currentPositionY; //���� ��ġ(y) ����
    float direction; //�̵��ӵ�+����
    
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
        //���� ��ġ(x)�� ��� �̵������� (x)�ִ밪���� ũ�ų� ���ٸ�
        //�̵��ӵ�+���⿡ -1�� ���� ������ ���ְ� ������ġ�� ��� �̵������� (x)�ִ밪���� ����
        else if (currentPositionX <= leftMax)
        {
            direction *= -1;
            currentPositionX = leftMax;
        }

        //���� ��ġ(x)�� �·� �̵������� (x)�ִ밪���� ũ�ų� ���ٸ�
        //�̵��ӵ�+���⿡ -1�� ���� ������ ���ְ� ������ġ�� �·� �̵������� (x)�ִ밪���� ����
        transform.position = new Vector3(currentPositionX, currentPositionY, 0);
    }
}
