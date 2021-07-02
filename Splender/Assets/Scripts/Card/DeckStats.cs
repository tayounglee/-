using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckStats : MonoBehaviour
{
    private float Time = 1f;

    public string Lv;

    public List<GameObject> CardPrefab1;
    public List<GameObject> CardPrefab2;
    public List<GameObject> CardPrefab3;

    public List<GameObject> CardDeck1;
    public List<GameObject> CardDeck2;
    public List<GameObject> CardDeck3;

    public Transform Lv1CardSpawnPoint;
    public Transform Lv2CardSpawnPoint;
    public Transform Lv3CardSpawnPoint;

    public Transform DeckPoint1;
    public Transform DeckPoint2;
    public Transform DeckPoint3;

    Vector3 current;
    Vector3 current2;
    Vector3 current3;

    private void Awake()
    {
        current = Lv1CardSpawnPoint.transform.position;
        current2 = Lv2CardSpawnPoint.transform.position;
        current3 = Lv3CardSpawnPoint.transform.position;

        Lv = name.Substring(3, 1);

        var notRandomizedCards = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Lv1 Cards Prefabs"));
        CardPrefab1 = new List<GameObject>(ShuffleCards(notRandomizedCards));

        var notRandomizedCards2 = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Lv2 Cards Prefabs"));
        CardPrefab2 = new List<GameObject>(ShuffleCards(notRandomizedCards2));

        var notRandomizedCards3 = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Lv3 Cards Prefabs"));
        CardPrefab3 = new List<GameObject>(ShuffleCards(notRandomizedCards3));

        DeckOnTable();

        for (int i = 0; i < 4; i++)
        {
            Invoke("StartingCardsPlace", Time);
            Time += 1.5f;
        }
    }

    public List<GameObject> ShuffleCards(List<GameObject> notRandomizedCards)
    {
        var shuffledcards = notRandomizedCards.OrderBy(a => Random.Range(0, notRandomizedCards.Count + 1)).ToList();
        return shuffledcards;
    }

    public void DeckOnTable()
    {
        foreach (var card in CardPrefab1)
        {
            CardDeck1.Add(Instantiate(card, DeckPoint1.position, Quaternion.Euler(new Vector3(0, 270, 180))));
        }

        foreach (var card in CardPrefab2)
        {
            CardDeck2.Add(Instantiate(card, DeckPoint2.position, Quaternion.Euler(new Vector3(0, 270, 180))));
        }

        foreach (var card in CardPrefab3)
        {
            CardDeck3.Add(Instantiate(card, DeckPoint3.position, Quaternion.Euler(new Vector3(0, 270, 180))));
        }
    }

    public void StartingCardsPlace()
    {
        Lv1CardSpawnPoint.position = current + new Vector3(0, 0, 0.4f);
        Lv2CardSpawnPoint.position = current2 + new Vector3(0, 0, 0.4f);
        Lv3CardSpawnPoint.position = current3 + new Vector3(0, 0, 0.4f);

        CardDeck1[0].GetComponent<CardStats>().MoveToTable(Lv1CardSpawnPoint);
        current.z += 0.4f;
        CardDeck1.RemoveAt(0);

        CardDeck2[0].GetComponent<CardStats>().MoveToTable(Lv2CardSpawnPoint);
        current2.z += 0.4f;
        CardDeck2.RemoveAt(0);

        CardDeck3[0].GetComponent<CardStats>().MoveToTable(Lv3CardSpawnPoint);
        current3.z += 0.4f;
        CardDeck3.RemoveAt(0);
    }
}
