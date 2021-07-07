using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardStats : MonoBehaviour
{
    public int Lv;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private Rigidbody Rigidbody;
    private bool EnableMove;
    private float Timer = 0;
    private float TimerEnd = 3f;

    private bool NeedSlerp = false;
    public bool IsBooked = false;

    /// <summary>
    /// Rotation x,y,z축 제한
    /// </summary>
    private void Awake()
    {
        Lv = int.Parse(name.Substring(0, 1));

        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;
    }

    /// <summary>
    /// 테이블로 카드를 이동
    /// </summary>
    /// <param name="target"></param>
    public void MoveCardToTable(Transform target)
    {
        GetComponent<Rigidbody>().useGravity = false;

        Rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 270, 0)));

        ChangeTransform(target);

        EnableMove = true;
    }

    public void ChangeTransform(Transform target)
    {
        TargetPos = target.position;
        TargetRot = target.rotation;
    }


    /// <summary>
    /// 구형보간을 이용한 이동
    /// </summary>
    void Update()
    {
        if (EnableMove && Timer < TimerEnd)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, TargetPos, 2f * Time.deltaTime);
            if (NeedSlerp)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, 2f * Time.deltaTime);
            }
            if (Timer > TimerEnd)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                EnableMove = false;
                NeedSlerp = false;
                Timer = 0;
            }
        }
    }
}
