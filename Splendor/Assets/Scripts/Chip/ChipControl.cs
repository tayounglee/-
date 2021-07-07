using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipControl : MonoBehaviour
{
    public string Type;
    private float Timer = 0;
    private float TimerEnd = 4f;
    private bool EnabledToMove = false;

    private Rigidbody Rigidbody;
    public Transform chipStash;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private void Awake()
    {
        Type = gameObject.name;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;
    }


    public void ChangeTransform(Transform target)
    {
        TargetPos = target.position;
        TargetRot = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnabledToMove && Timer < TimerEnd)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, 3f * Time.deltaTime);
            if (Timer > TimerEnd)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                EnabledToMove = false;
                Timer = 0;
            }
        }
    }
}
