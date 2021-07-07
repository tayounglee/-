using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    bool isOnTurn = false;

    public CardManager CardManager;
    public NobleCardStats NobleCardStats;
    ChipManager ChipManager;
    List<ChipStats> ChipStats;

    public Transform[] nobleCardPlacingPoints;

    public Transform[] DiamondCardPlacingPoints;
    public Transform[] SapphireCardPlacingPoints;
    public Transform[] EmeraldCardPlacingPoints;
    public Transform[] RubyCardPlacingPoints;
    public Transform[] OnyxCardPlacingPoints;

    public Transform[] bookedCardPlacingPoints;
    public List<bool> freeBookedPlaces;

    public Transform[] DiamondChipPlacingPoints;
    public Transform[] SapphireChipPlacingPoints;
    public Transform[] EmeraldChipPlacingPoints;
    public Transform[] RubyChipPlacingPoints;
    public Transform[] OnyxChipPlacingPoints;
    public Transform[] GoldChipPlacingPoints;

    public List<string> chipsTaken;

    public int TotalPoint = 0;

    public int TakeChips = 3;

    public int DiamondChipNumber = 0;
    public int SapphireChipNumber = 0;
    public int EmeraldChipNumber = 0;
    public int RubyChipNumber = 0;
    public int OnyxChipNumber = 0;
    public int GoldChipNumber = 0;

    private int missingChipNumber = 0;

    public List<GameObject> GoldChips;
    public List<GameObject> DiamondChips;
    public List<GameObject> SapphireChips;
    public List<GameObject> EmeraldChips;
    public List<GameObject> RubyChips;
    public List<GameObject> OnyxChips;

    public int DiamondCardNumber = 0;
    public int SapphireCardNumber = 0;
    public int EmeraldCardNumber = 0;
    public int RubyCardNumber = 0;
    public int OnyxCardNumber = 0;

    public int bookedCardsNumber = 0;

    public int nobleCards = 0;

    

    private void Awake()
    {
        ChipManager = FindObjectOfType<ChipManager>();
        NobleCardStats = (NobleCardStats)FindObjectOfType(typeof(NobleCardStats));
        ChipStats = new List<ChipStats>((ChipStats[])FindObjectsOfType(typeof(ChipStats)));

        GoldChips = new List<GameObject>();
        DiamondChips = new List<GameObject>();
        SapphireChips = new List<GameObject>();
        EmeraldChips = new List<GameObject>();
        RubyChips = new List<GameObject>();
        OnyxChips = new List<GameObject>();

        DiamondChipNumber = 0;
        SapphireChipNumber = 0;
        EmeraldChipNumber = 0;
        RubyChipNumber = 0;
        OnyxChipNumber = 0;
        GoldChipNumber = 0;

        ChipStats = new List<ChipStats>((ChipStats[])FindObjectsOfType(typeof(ChipStats)));
    }
}
