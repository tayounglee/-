using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipStats : MonoBehaviour
{
    public string ChipType;
    public int ChipCount;
    public List<GameObject> Chips;
    public GameObject chipType;

    private float Timer = 0;
    private float TimerEnd = 4f;
    private bool EnabledToMove = false;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private Rigidbody Rigidbody;

    public Transform ChipDeck;

    private void Awake()
    {
        Chips = new List<GameObject>();
        ChipType = gameObject.name;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;

        if (ChipType == "Gold Chips")
        {
            for (int i = 0; i < 5; i++)
            {
                Chips.Add(Instantiate(chipType, transform.position, transform.rotation));
            }
            ChipCount = 5;
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                Chips.Add(Instantiate(chipType, transform.position, transform.rotation));
            }
            ChipCount = 7;
        }
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

