using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    float rightMax; //�·� �̵������� (x)�ִ밪
    float leftMax; //��� �̵������� (x)�ִ밪
    float downMax; //�Ʒ��� �̵������� (y)�ִ밪
    float currentPositionX; //���� ��ġ(x) ����
    float currentPositionY; //���� ��ġ(y) ����
    float direction; //�̵��ӵ�+����

    public static ObjectManager instance;
    public bool isWatchout;
    bool isShake;
    
    //ObjectManager Title;

    void Awake()
    {
        rightMax = 1.1f;
        downMax = -0.1f;
        leftMax = 0.6f;
        currentPositionX = transform.position.x;
        currentPositionY = transform.position.y;
        direction = 0.1f;

        instance = this;
        isWatchout = false;
        isShake = true;
        //Title = FindObjectOfType<ObjectManager>();
    }

    void Update()
    {
        if(isShake == true)
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

        if (isWatchout == true)
        {
            isShake = false;
            currentPositionX = transform.position.x;
            currentPositionY = transform.position.y;
            //Title.enabled = false;
            currentPositionY += Time.deltaTime * direction;
            if (currentPositionY >= downMax)
            {
                direction *= -15f;
                currentPositionY = downMax;
            }
            transform.position = new Vector3(currentPositionX, currentPositionY, 0);
            
            if(currentPositionY <= -1)
            {
                isWatchout = false;
                isShake = true;
                direction = 0.1f;
                CameraShake.camera.enabled = true;
                foreach(ObjectShake obj in ObjectShake.obj)
                {
                    obj.enabled = true;
                }
            }
        }
    }

}
