using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipStats : MonoBehaviour
{
    public string ChipType;
    public int ChipCount;
    public List<GameObject> Chips;
    public GameObject chipType;

    /// <summary>
    /// 골드칩은 5개 그외 종류의칩은 7개 스폰
    /// </summary>
    private void Awake()
    {
        Chips = new List<GameObject>();
        ChipType = gameObject.name;

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
            ChipCount = 7;
            for (int i = 0; i < 7; i++)
            {
                Chips.Add(Instantiate(chipType, transform.position, transform.rotation));
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
      
    }
}

