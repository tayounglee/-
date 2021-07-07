using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardStats : MonoBehaviour
{
    public int Lv;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private Rigidbody rigidbody;
    private bool EnableMove;
    private float Timer = 0;
    private float TimerEnd = 3f;

    private bool needSlerp = false;
    public bool isBooked = false;

    private void Awake()
    {
        Lv = int.Parse(name.Substring(0, 1));

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    public void MoveToTable(Transform target)
    {
        GetComponent<Rigidbody>().useGravity = false;

        rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 270, 0)));

        ChangeTransform(target);

        EnableMove = true;
    }

    public void ChangeTransform(Transform target)
    {
        TargetPos = target.position;
        TargetRot = target.rotation;
    }

    void Update()
    {
        if (EnableMove && Timer < TimerEnd)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, TargetPos, 2f * Time.deltaTime);
            if (needSlerp)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, 2f * Time.deltaTime);
            }
            if (Timer > TimerEnd)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                EnableMove = false;
                needSlerp = false;
                Timer = 0;
            }
        }
    }
}
