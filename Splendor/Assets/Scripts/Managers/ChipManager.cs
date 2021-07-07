using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipManager : MonoBehaviour
{
    public Text P1Diamond;
    public Text P1Sapphire;
    public Text P1Emerald;
    public Text P1Ruby;
    public Text P1Onyx;
    public Text P1Gold;

    public Text P2Diamond;
    public Text P2Sapphire;
    public Text P2Emerald;
    public Text P2Ruby;
    public Text P2Onyx;
    public Text P2Gold;

    public Text P3Diamond;
    public Text P3Sapphire;
    public Text P3Emerald;
    public Text P3Ruby;
    public Text P3Onyx;
    public Text P3Gold;

    public Text P4Diamond;
    public Text P4Sapphire;
    public Text P4Emerald;
    public Text P4Ruby;
    public Text P4Onyx;
    public Text P4Gold;

    void UpdatePlayer1()
    {
    }

    void UpdatePlayer2()
    {
    }

    void UpdatePlayer3()
    {
    }

    void UpdatePlayer4()
    {
    }

    public void UpdatePlayer(string Name)
    {

        switch (Name)
        {
            case "Player1":
                UpdatePlayer1();
                break;
            case "Player2":
                UpdatePlayer2();
                break;
            case "Player3":
                UpdatePlayer3();
                break;
            case "Player4":
                UpdatePlayer4();
                break;
            default:
                break;
        }
    }
}
