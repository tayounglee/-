using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public DeckStats deckStats;
    private void Awake()
    {
        
    }

    public void Card(GameObject Card)
    {
        int Lv = Card.GetComponent<CardStats>().Lv;

        switch (Lv)
        {
            case 1:
                if (deckStats.CardDeck1.Count >= 1)
                {
                    deckStats.CardDeck1[0].GetComponent<CardStats>().MoveCardToTable(Card.GetComponent<Transform>());
                    deckStats.CardDeck1.RemoveAt(0);
                }

                break;
            case 2:
                if (deckStats.CardDeck2.Count >= 1)
                {
                    deckStats.CardDeck2[0].GetComponent<CardStats>().MoveCardToTable(Card.GetComponent<Transform>());
                    deckStats.CardDeck2.RemoveAt(0);
                }
                break;
            case 3:
                if (deckStats.CardDeck3.Count >= 1)
                {
                    deckStats.CardDeck3[0].GetComponent<CardStats>().MoveCardToTable(Card.GetComponent<Transform>());
                    deckStats.CardDeck3.RemoveAt(0);
                }
                break;
            default:
                break;
        }
    }

}