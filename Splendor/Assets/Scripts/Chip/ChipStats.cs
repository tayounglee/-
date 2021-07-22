using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipStats : MonoBehaviour
{
    public string ChipType;
    public int ChipCount;
    public List<GameObject> Chips;
    public List<GameObject> ChipBox;
    public GameObject chipType;
    //PlayerControl PlayerControl;
    public bool IsBooked = false;

    /// <summary>
    /// 골드칩은 5개 그외 종류의칩은 7개 스폰
    /// </summary>
    private void Awake()
    {
        Chips = new List<GameObject>();
        ChipBox = new List<GameObject>();
        
        ChipType = gameObject.name.Substring(0, 1);

        if (ChipType == "G")
        {
            for (int i = 0; i < 5; i++)
            {
                Chips.Add(Instantiate(chipType, transform.position, transform.rotation));
            }
            ChipCount = 5;
        }
        else
        {
            ChipCount = 7;
            for (int i = 0; i < 7; i++)
            {
                Chips.Add(Instantiate(chipType, transform.position, transform.rotation));
            }
        }
    }

    public void MouseLeftClick(PlayerControl Player, CardStats Card)
    {
        if (ChipCount >= 1)
        {
            Player.GetChip(this, Card);
            if (Player.LimitAction)
            {

            }
        }

        else
        {
            print("칩더미에 칩이 4개 미만입니다.");
        }
    }

    public void MouseRightClick(PlayerControl Player, CardStats Card)
    {
        if (ChipCount - 2 >= 2)
        {
            //ChipCount = ChipCount - 2;
            Player.GetTwoChips(this, Card);
            if (Player.LimitAction)
            {
                
            }
        }
        else
        {
            print("칩더미에 칩이 4개 미만입니다.");
        }
    }

    public void GiveChipToPlayer(PlayerControl Player)
    {
        Chips[ChipCount - 1].GetComponent<ChipControl>().MoveToPlayer(Player);
        Player.AddChip(Chips[ChipCount - 1]);
        //ChipBox.Add(Chips[ChipCount - 1]);
        Chips.RemoveAt(ChipCount - 1);
        ChipCount--;
    }

    public void PayChips(GameObject chip)
    {
        Chips.Add(chip);
        chip.GetComponent<ChipControl>().MoveTo(this.transform);
        ChipCount++;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
    }
}

