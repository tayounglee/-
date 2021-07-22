using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public bool isLastRound = false;
    public int lastCounter = 4;
    public int OnTurnPlayerId = 0;
    int CardMask;
    public int ChipMask;
    public int ChipBundleMask;
    public float CamRayLength = 100f;
    float Timer;
    ChipManager ChipManager;
    public List<GameObject> Players;
    PlayerControl p;

    private void Awake()
    {
        ChipManager = GetComponent<ChipManager>();
        CardMask = LayerMask.GetMask("CardMask");
        ChipMask = LayerMask.GetMask("ChipMask");
        ChipBundleMask = LayerMask.GetMask("ChipBundleMask");
    }

    public void SetNextPlayerId()
    {
        if (OnTurnPlayerId != 3)
        {
            OnTurnPlayerId++;
        }
        else
        {
            OnTurnPlayerId = 0;
        }
        Timer = 0;

    }

    public void NextPlayer()
    {
        if (GetOnTurnPlayer().TotalPoint >= 15)
        {
            isLastRound = true;
        }

        if (isLastRound && lastCounter != 0)
        {
            lastCounter--;
        }

        if (lastCounter != 0)
        {
            SetNextPlayerId();
        }
    }

    public PlayerControl GetOnTurnPlayer()
    {
        return Players[OnTurnPlayerId].GetComponent<PlayerControl>();
    }

    public void OnCardLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit CardHit;

            if (Physics.Raycast(ray, out CardHit, CamRayLength, CardMask))
            {
                CardHit.transform.GetComponent<CardStats>().MouseLeftClick(GetOnTurnPlayer());
            }
        }
    }

    public void OnCardRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit CardHit;

            if (Physics.Raycast(ray, out CardHit, CamRayLength, CardMask))
            {
                CardHit.transform.GetComponent<CardStats>().MouseRightClick(GetOnTurnPlayer());
            }
        }
    }

    public void OnChipLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit ChipHit;
            int layerMask = 1 << LayerMask.NameToLayer("ChipMask");

            if (Physics.Raycast(ray, out ChipHit, CamRayLength, layerMask))
            {
                if (!GetOnTurnPlayer().ChipsTaken.Contains(ChipHit.transform.GetComponent<ChipStats>().ChipType))
                {
                    ChipHit.transform.GetComponent<ChipStats>().MouseLeftClick(GetOnTurnPlayer(), GetComponent<CardStats>());
                }

                else
                {
                    print("이미 같은타입의 칩을 가져갔습니다. 다른종류의 칩을 선택해주세요.");
                }
            }
        }
    }

    public void OnChipRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit ChipHit;

            if (Physics.Raycast(ray, out ChipHit, CamRayLength, ChipMask))
            {
                if (ChipHit.transform.GetComponent<ChipStats>().ChipType != "Gold Chips")
                {
                    ChipHit.transform.GetComponent<ChipStats>().MouseRightClick(GetOnTurnPlayer(), GetComponent<CardStats>());
                }
                else
                {
                    print("Can't take two from the Gold chips! Book a card with right click to get one!");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lastCounter != 0)
        {
            OnCardLeftClick();

            OnCardRightClick();

            OnChipLeftClick();

            OnChipRightClick();
        }
    }
}
