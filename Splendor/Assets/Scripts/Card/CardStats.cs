using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardStats : MonoBehaviour
{
    public int DiamondChipsValue;
    public int SapphireChipsValue;
    public int EmeraldChipsValue;
    public int RubyChipsValue;
    public int OnyxChipsValue;

    int GoldChipsToUse = 0;

    public string Type;
    public int PointValue;
    public int Lv;
    public int Booking;

    Vector3 TargetPos;
    Quaternion TargetRot;

    private Rigidbody Rigidbody;
    private bool EnableMove;
    private float Timer = 0;
    private float TimerEnd = 3f;

    private bool NeedSlerp = false;
    public bool IsBooked = false;

    public PlayerControl Keep;

    /// <summary>
    /// Rotation x,y,z축 제한
    /// </summary>
    private void Awake()
    {
        Lv = int.Parse(name.Substring(0, 1));
        Type = name.Substring(1, 1);
        PointValue = int.Parse(name.Substring(2, 1));
        DiamondChipsValue = int.Parse(name.Substring(3, 1));
        SapphireChipsValue = int.Parse(name.Substring(4, 1));
        EmeraldChipsValue = int.Parse(name.Substring(5, 1));
        RubyChipsValue = int.Parse(name.Substring(6, 1));
        OnyxChipsValue = int.Parse(name.Substring(7, 1));

        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;

    }

    public void MouseLeftClick(PlayerControl Player)
    {
        if (Player.SelectChips)
        {
            print("Can't buy card and pick chips in the same turn, continue looting!");
            return;
        }

        if (!isPlayerBookedMe(Player) && IsBooked)
        {
            print("Someone else has booked this card!");
            return;
        }

        if (!CheckPlayer(Player))
        {
            print("Not enough chips to buy this card!");
            return;
        }

        Player.GetCard(GetComponent<CardStats>());
    }

    public void OnRightClick(PlayerControl Player)
    {
        if (IsBooked)
        {
            print("Someone has already booked this card!");
            return;
        }
        IsBooked = true;
        Keep = Player;
        Player.BookCard(GetComponent<CardStats>(), GetComponent<ChipStats>());
    }

    public bool CheckPlayer(PlayerControl Player)
    {
        int remainingGoldChips = Player.GoldChipNumber;


        if (Player.DiamondChipNumber + Player.DiamondCardNumber < DiamondChipsValue)
        {
            if (remainingGoldChips + Player.DiamondChipNumber + Player.DiamondCardNumber < DiamondChipsValue)
            {
                return false;
            }
            else
            {
                remainingGoldChips -= DiamondChipsValue - Player.DiamondCardNumber - Player.DiamondChipNumber;
            }
        }
        if (Player.SapphireChipNumber + Player.SapphireCardNumber < SapphireChipsValue)
        {
            if (remainingGoldChips + Player.SapphireChipNumber + Player.SapphireCardNumber < SapphireChipsValue)
            {
                return false;
            }
            else
            {
                remainingGoldChips -= SapphireChipsValue - Player.SapphireCardNumber - Player.SapphireChipNumber;
            }
        }
        if (Player.EmeraldChipNumber + Player.EmeraldCardNumber < EmeraldChipsValue)
        {
            if (remainingGoldChips + Player.EmeraldChipNumber + Player.EmeraldCardNumber < EmeraldChipsValue)
            {
                return false;
            }
            else
            {
                remainingGoldChips -= EmeraldChipsValue - Player.EmeraldCardNumber - Player.EmeraldChipNumber;
            }
        }
        if (Player.RubyChipNumber + Player.RubyCardNumber < RubyChipsValue)
        {
            if (remainingGoldChips + Player.RubyChipNumber + Player.RubyCardNumber < RubyChipsValue)
            {
                return false;
            }
            else
            {
                remainingGoldChips -= RubyChipsValue - Player.RubyCardNumber - Player.RubyChipNumber;
            }
        }
        if (Player.OnyxChipNumber + Player.OnyxCardNumber < OnyxChipsValue)
        {
            if (remainingGoldChips + Player.OnyxChipNumber + Player.OnyxCardNumber < OnyxChipsValue)
            {
                return false;
            }
            else
            {
                remainingGoldChips -= OnyxChipsValue - Player.OnyxCardNumber - Player.OnyxChipNumber;
            }
        }

        return true;
    }

    public void MoveToPlayerBooks(PlayerControl Player)
    {
        Booking = Player.freeBookedPlaces.IndexOf(Player.freeBookedPlaces.Find(f => f == true));
        MoveTo(Player.KeepCards[Booking]);
        Player.freeBookedPlaces[Booking] = false;
    }

    public void MoveTo(Transform destination)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        ChangeTransform(destination);
        EnableMove = true;
        NeedSlerp = true;
    }

    public bool isPlayerBookedMe(PlayerControl Player)
    {
        if (Keep == Player)
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
        switch (Type)
        {
            case "D":
                MoveTo(Player.DiamondCardPoints[Player.DiamondCardNumber]);
                break;
            case "S":
                MoveTo(Player.SapphireCardPoints[Player.SapphireCardNumber]);
                break;
            case "E":
                MoveTo(Player.EmeraldCardPoints[Player.EmeraldCardNumber]);
                break;
            case "R":
                MoveTo(Player.RubyCardPoints[Player.RubyCardNumber]);
                break;
            case "O":
                MoveTo(Player.OnyxCardPoints[Player.OnyxCardNumber]);
                break;
            default:
                break;
        }
        EnableMove = true;
    }

    /// <summary>
    /// 테이블로 카드를 이동
    /// </summary>
    /// <param name="Target"></param>
    public void MoveCardToTable(Transform Target)
    {
        GetComponent<Rigidbody>().useGravity = false;
        Rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 270, 0)));
        ChangeTransform(Target);
        EnableMove = true;
    }

    public void ChangeTransform(Transform Target)
    {
        TargetPos = Target.position;
        TargetRot = Target.rotation;
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
