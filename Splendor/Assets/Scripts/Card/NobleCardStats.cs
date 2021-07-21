using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NobleCardStats : MonoBehaviour
{
    public int PointValue;

    public int DiamondCardValue;
    public int SapphireCardValue;
    public int EmeraldCardValue;
    public int RubyCardValue;
    public int OnyxCardValue;

    public bool IsOwned = false;

    Vector3 TargetPlayerPos;
    Quaternion TargetPlayerRot;

    protected float Timer = 0;

    bool EnableMove = false;

    private void Awake()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        PointValue = int.Parse(name.Substring(1, 1));
        DiamondCardValue = int.Parse(name.Substring(2, 1));
        SapphireCardValue = int.Parse(name.Substring(3, 1));
        EmeraldCardValue = int.Parse(name.Substring(4, 1));
        RubyCardValue = int.Parse(name.Substring(5, 1));
        OnyxCardValue = int.Parse(name.Substring(6, 1));
    }

    public bool CheckPlayer(PlayerControl Player)
    {
        if (Player.DiamondCardNumber >= DiamondCardValue &&
            Player.SapphireCardNumber >= SapphireCardValue &&
            Player.EmeraldCardNumber >= EmeraldCardValue &&
            Player.RubyCardNumber >= RubyCardValue &&
            Player.OnyxCardNumber >= OnyxCardValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveToPlayer(PlayerControl Player)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        TargetPlayerPos = Player.NobleCardPoints[Player.NobleCards].position;
        TargetPlayerRot = Player.NobleCardPoints[Player.NobleCards].rotation;
        gameObject.GetComponent<AudioSource>().Play();
        EnableMove = true;
    }

    private void Update()
    {
        if (EnableMove)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, TargetPlayerPos, 1.5f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetPlayerRot, 1.5f * Time.deltaTime);
            if (Timer > 6f)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                EnableMove = false;
                Timer = 0;
            }
        }
    }
}