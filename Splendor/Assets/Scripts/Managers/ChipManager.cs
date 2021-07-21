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

    public PlayerControl Player1;
    public PlayerControl Player2;
    public PlayerControl Player3;
    public PlayerControl Player4;


    void UpdatePlayer1()
    {
        P1Diamond.text = Player1.DiamondChips.Count.ToString();
        P1Sapphire.text = Player1.SapphireChips.Count.ToString();
        P1Emerald.text = Player1.EmeraldChips.Count.ToString();
        P1Ruby.text = Player1.RubyChips.Count.ToString();
        P1Onyx.text = Player1.OnyxChips.Count.ToString();
        P1Gold.text = Player1.GoldChips.Count.ToString();
    }

    void UpdatePlayer2()
    {
        P2Diamond.text = Player2.DiamondChips.Count.ToString();
        P2Sapphire.text = Player2.SapphireChips.Count.ToString();
        P2Emerald.text = Player2.EmeraldChips.Count.ToString();
        P2Ruby.text = Player2.RubyChips.Count.ToString();
        P2Onyx.text = Player2.OnyxChips.Count.ToString();
        P2Gold.text = Player2.GoldChips.Count.ToString();
    }

    void UpdatePlayer3()
    {
        P3Diamond.text = Player3.DiamondChips.Count.ToString();
        P3Sapphire.text = Player3.SapphireChips.Count.ToString();
        P3Emerald.text = Player3.EmeraldChips.Count.ToString();
        P3Ruby.text = Player3.RubyChips.Count.ToString();
        P3Onyx.text = Player3.OnyxChips.Count.ToString();
        P3Gold.text = Player3.GoldChips.Count.ToString();
    }

    void UpdatePlayer4()
    {
        P4Diamond.text = Player4.DiamondChips.Count.ToString();
        P4Sapphire.text = Player4.SapphireChips.Count.ToString();
        P4Emerald.text = Player4.EmeraldChips.Count.ToString();
        P4Ruby.text = Player4.RubyChips.Count.ToString();
        P4Onyx.text = Player4.OnyxChips.Count.ToString();
        P4Gold.text = Player4.GoldChips.Count.ToString();
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
