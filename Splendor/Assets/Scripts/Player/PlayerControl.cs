using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    bool IsOnTurn = false;

    public CardManager CardManager;
    public DeckStats DeckStats;
    ChipManager ChipManager;
    ClickManager ClickManager;
    List<ChipStats> ChipStats;

    public Transform[] NobleCardPoints;
    public Transform[] DiamondCardPoints;
    public Transform[] SapphireCardPoints;
    public Transform[] EmeraldCardPoints;
    public Transform[] RubyCardPoints;
    public Transform[] OnyxCardPoints;

    public Transform[] DiamondChipPoints;
    public Transform[] SapphireChipPoints;
    public Transform[] EmeraldChipPoints;
    public Transform[] RubyChipPoints;
    public Transform[] OnyxChipPoints;
    public Transform[] GoldChipPoints;

    public Transform[] KeepCards;
    public List<bool> freeBookedPlaces;

    public List<string> ChipsTaken;

    public int TotalPoint = 0;
    public int LimitChip = 0;
    public int TakeChips = 3;

    public int DiamondChipNumber = 0;
    public int SapphireChipNumber = 0;
    public int EmeraldChipNumber = 0;
    public int RubyChipNumber = 0;
    public int OnyxChipNumber = 0;
    public int GoldChipNumber = 0;

    public List<GameObject> GoldChips;
    public List<GameObject> DiamondChips;
    public List<GameObject> SapphireChips;
    public List<GameObject> EmeraldChips;
    public List<GameObject> RubyChips;
    public List<GameObject> OnyxChips;

    public bool SelectChips = false;
    public bool LimitAction = false;

    public int DiamondCardNumber = 0;
    public int SapphireCardNumber = 0;
    public int EmeraldCardNumber = 0;
    public int RubyCardNumber = 0;
    public int OnyxCardNumber = 0;
    public int bookedCardsNumber = 0;
    public int NobleCards = 0;

    private void Awake()
    {
        ChipManager = FindObjectOfType<ChipManager>();
        ClickManager = Camera.main.GetComponent<ClickManager>();
        DeckStats = (DeckStats)FindObjectOfType(typeof(DeckStats));
        ChipStats = new List<ChipStats>((ChipStats[])FindObjectsOfType(typeof(ChipStats)));

        GoldChips = new List<GameObject>();
        DiamondChips = new List<GameObject>();
        SapphireChips = new List<GameObject>();
        EmeraldChips = new List<GameObject>();
        RubyChips = new List<GameObject>();
        OnyxChips = new List<GameObject>();

        freeBookedPlaces = new List<bool>();
        for(int i = 0; i < 4; i++)
        {
            freeBookedPlaces.Add(true);
        }

        DiamondChipNumber = 0;
        SapphireChipNumber = 0;
        EmeraldChipNumber = 0;
        RubyChipNumber = 0;
        OnyxChipNumber = 0;
        GoldChipNumber = 0;

        TakeChips = 3;
        LimitChip = 0;

        ChipsTaken = new List<string>();
    }

    public void AddPoint(int point)
    {
        TotalPoint = TotalPoint + point;
    }

    public void EndTurn()
    {
            ChipManager.UpdatePlayer(this.name);
            TakeChips = 3;
            ChipsTaken.Clear();
            SelectChips = false;
            ClickManager.NextPlayer();
            print("다음 턴");
    }

    private void AddCardGem(string CardGemType)
    {
        if (!LimitAction)
        {
            switch (CardGemType)
            {
                case "Diamond":
                    DiamondCardNumber++;
                    break;
                case "Sapphire":
                    SapphireCardNumber++;
                    break;
                case "Emerald":
                    EmeraldCardNumber++;
                    break;
                case "Ruby":
                    RubyCardNumber++;
                    break;
                case "Onyx":
                    OnyxCardNumber++;
                    break;
                default:
                    break;
            }
        }
    }

    public void GetCard(CardStats Card)
    {
        if (!LimitAction)
        {
            if (Card.IsBooked)
            {
                bookedCardsNumber--;
                freeBookedPlaces[Card.Booking] = true;
            }

            PayChips(Card);

            AddPoint(Card.PointValue);

            AddCardGem(Card.Type);

            Card.MoveToPlayer(this);

            DeckStats.CheckPlayerCardValues(this);

            if (!Card.IsBooked)
            {
                CardManager.Card(Card.gameObject);
            }

            EndTurn();
        }
    }

    public void GetTwoChips(ChipStats ChipStats)
    {
        if (!LimitAction)
        {
            if (TakeChips == 3 && ChipStats.ChipType != "G")
            {
                GetChip(ChipStats);
                GetChip(ChipStats);

                EndTurn();
            }
            //else if (TotalChips == 1)
            //{

            //}
            else
            {
                print("이 행동은 할 수 없습니다.");
            }
        }
    }

    public void PayChips(CardStats Card)
    {
        if (!LimitAction)
        {
            for (int i = 0; i < Card.DiamondChipsValue - DiamondCardNumber; i++)
            {
                if (DiamondChips.Count != 0)
                {
                    ChipStats.Find(c => c.ChipType == "D").PayChips(DiamondChips[DiamondChips.Count - 1]);
                    DiamondChips.RemoveAt(DiamondChips.Count - 1);
                    DiamondChipNumber--;
                }
                else
                {
                    ChipStats.Find(c => c.ChipType == "G").PayChips(GoldChips[GoldChips.Count - 1]);
                    GoldChips.RemoveAt(GoldChips.Count - 1);
                    GoldChipNumber--;
                }
            }
            for (int i = 0; i < Card.SapphireChipsValue - SapphireCardNumber; i++)
            {
                if (SapphireChips.Count != 0)
                {
                    ChipStats.Find(c => c.ChipType == "S").PayChips(SapphireChips[SapphireChips.Count - 1]);
                    SapphireChips.RemoveAt(SapphireChips.Count - 1);
                    SapphireChipNumber--;
                }
                else
                {
                    ChipStats.Find(c => c.ChipType == "G").PayChips(GoldChips[GoldChips.Count - 1]);
                    GoldChips.RemoveAt(GoldChips.Count - 1);
                    GoldChipNumber--;
                }
            }
            for (int i = 0; i < Card.EmeraldChipsValue - EmeraldCardNumber; i++)
            {
                if (EmeraldChips.Count != 0)
                {
                    ChipStats.Find(c => c.ChipType == "E").PayChips(EmeraldChips[EmeraldChips.Count - 1]);
                    EmeraldChips.RemoveAt(EmeraldChips.Count - 1);
                    EmeraldChipNumber--;
                }
                else
                {
                    ChipStats.Find(c => c.ChipType == "G").PayChips(GoldChips[GoldChips.Count - 1]);
                    GoldChips.RemoveAt(GoldChips.Count - 1);
                    GoldChipNumber--;
                }
            }
            for (int i = 0; i < Card.RubyChipsValue - RubyCardNumber; i++)
            {
                if (RubyChips.Count != 0)
                {
                    ChipStats.Find(c => c.ChipType == "R").PayChips(RubyChips[RubyChips.Count - 1]);
                    RubyChips.RemoveAt(RubyChips.Count - 1);
                    RubyChipNumber--;
                }
                else
                {
                    ChipStats.Find(c => c.ChipType == "G").PayChips(GoldChips[GoldChips.Count - 1]);
                    GoldChips.RemoveAt(GoldChips.Count - 1);
                    GoldChipNumber--;
                }
            }
            for (int i = 0; i < Card.OnyxChipsValue - OnyxCardNumber; i++)
            {
                if (OnyxChips.Count != 0)
                {
                    ChipStats.Find(c => c.ChipType == "O").PayChips(OnyxChips[OnyxChips.Count - 1]);
                    OnyxChips.RemoveAt(OnyxChips.Count - 1);
                    OnyxChipNumber--;
                }
                else
                {
                    ChipStats.Find(c => c.ChipType == "G").PayChips(GoldChips[GoldChips.Count - 1]);
                    GoldChips.RemoveAt(GoldChips.Count - 1);
                    GoldChipNumber--;
                }
            }
        }
    }

    public void GetChip(ChipStats ChipStats)
    {
        if (!LimitAction)
        {
            switch (ChipStats.ChipType)
            {
                case "D":
                    DiamondChipNumber++;
                    LimitChip++;
                    break;
                case "S":
                    SapphireChipNumber++;
                    LimitChip++;
                    break;
                case "E":
                    EmeraldChipNumber++;
                    LimitChip++;
                    break;
                case "R":
                    RubyChipNumber++;
                    LimitChip++;
                    break;
                case "O":
                    OnyxChipNumber++;
                    LimitChip++;
                    break;
                case "G":
                    print("골드 칩은 킵 행위로만 획득할 수 있습니다.");
                    return;
                default:
                    break;
            }

            ChipStats.GiveChipToPlayer(this, ChipStats);

            ChipsTaken.Add(ChipStats.ChipType);

            TakeChips--;

            SelectChips = true;

            print("칩 한개를 선택하셨습니다.");

            if (LimitChip >= 3)
            {
                LimitAction = true;
                print("칩을 3개 이상 소지할 수 없습니다.");
                print("반환할 칩을 선택해주세요.");
            }
        }
    }

    public void BookCard(CardStats Card, ChipStats chipStats)
    {
        if (!LimitAction)
        {
            Card.MoveToPlayerBooks(this);

            CardManager.Card(Card.gameObject);

            GoldChipNumber++;
            LimitChip++;
            bookedCardsNumber++;

            ChipStats.Find(c => c.ChipType == "G").GiveChipToPlayer(this, chipStats);

            TakeChips = 0;
            EndTurn();
        }
    }

    public void GetNobleCard(NobleCardStats NobleCard)
    {
        if (!LimitAction)
        {
            TotalPoint = TotalPoint + NobleCard.PointValue;
            NobleCard.MoveToPlayer(this);
            NobleCards++;
        }
    }

    public void AddChip(GameObject Chip)
    {
        if (!LimitAction)
        {
            switch (Chip.GetComponent<ChipControl>().Type)
            {
                case "D":
                    DiamondChips.Add(Chip);
                    break;
                case "S":
                    SapphireChips.Add(Chip);
                    break;
                case "E":
                    EmeraldChips.Add(Chip);
                    break;
                case "R":
                    RubyChips.Add(Chip);
                    break;
                case "O":
                    OnyxChips.Add(Chip);
                    break;
                case "G":
                    GoldChips.Add(Chip);
                    break;
                default:
                    break;
            }
        }
    }

    public int CardNumber()
    {
        int CardNumber = DiamondCardNumber + SapphireCardNumber + EmeraldCardNumber + RubyCardNumber + OnyxCardNumber;

        return CardNumber;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LimitChip >= 3)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit ChipHit;
                int layerMask = 1 << LayerMask.NameToLayer("ChipBundleMask");

                if (Physics.Raycast(ray, out ChipHit, ClickManager.CamRayLength, layerMask))
                {
                    ChipHit.transform.GetComponent<ChipStats>().MouseLeftClick(ClickManager.GetOnTurnPlayer());
                }

                else
                {
                    print("아무것도 안맞음");
                }
            }
        }

        if (TakeChips == 0 && LimitChip < 3 && !LimitAction)
        {
            EndTurn();
        }
    }
}