using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipControl : MonoBehaviour
{
    public string Type;
    private float Timer = 0;
    private float TimerEnd = 2f;
    private bool EnableMove = false;

    private Rigidbody Rigidbody;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private void Awake()
    {
        Type = gameObject.name.Substring(0, 1);
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;
    }

    public void MoveToPlayer(PlayerControl Player)
    {
        switch (Type)
        {
            case "D":
                MoveTo(Player.DiamondChipPoints[Player.DiamondChipNumber]);
                break;
            case "S":
                MoveTo(Player.SapphireChipPoints[Player.SapphireChipNumber]);
                break;
            case "E":
                MoveTo(Player.EmeraldChipPoints[Player.EmeraldChipNumber]);
                break;
            case "R":
                MoveTo(Player.RubyChipPoints[Player.RubyChipNumber]);
                break;
            case "O":
                MoveTo(Player.OnyxChipPoints[Player.OnyxChipNumber]);
                break;
            case "G":
                MoveTo(Player.GoldChipPoints[Player.GoldChipNumber]);
                break;
            default:
                break;
        }

        EnableMove = true;
    }

    public void MoveTo(Transform destination)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Physics.IgnoreLayerCollision(10, 10, true);
        ChangeTransform(destination);
        EnableMove = true;
    }

    public void ChangeTransform(Transform Target)
    {
        TargetPos = Target.position;
        TargetRot = Target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableMove && Timer < TimerEnd)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, 3f * Time.deltaTime);
            if (Timer > TimerEnd)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                EnableMove = false;
                Timer = 0;
            }
        }
    }
}
