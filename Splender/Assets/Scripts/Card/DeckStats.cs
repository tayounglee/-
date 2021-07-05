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
    public List<GameObject> NobleCardPrefab;

    public List<GameObject> CardDeck1;
    public List<GameObject> CardDeck2;
    public List<GameObject> CardDeck3;

    public Transform Lv1CardSpawnPoint;
    public Transform Lv2CardSpawnPoint;
    public Transform Lv3CardSpawnPoint;
    public Transform NobleCardSpawnPoint;

    public Transform DeckPoint1;
    public Transform DeckPoint2;
    public Transform DeckPoint3;

    Vector3 current;
    Vector3 current2;
    Vector3 current3;

    public int nobleCardNumber;
    

    /// <summary>
    /// 카드들 리소스 로드 및 카드 스폰
    /// </summary>
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

        var notRandomizedCards4 = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Noble Cards Prefabs"));
        NobleCardPrefab = new List<GameObject>(ShuffleCards(notRandomizedCards4).GetRange(0, 5));

        DeckOnTable();

        for (int i = 0; i < 4; i++)
        {
            Invoke("CardSpawnPoint", Time);
            Time += 1.5f;
        }
    }

    /// <summary>
    /// 섞이지 않은 덱을 랜덤하게 섞는 함수.
    /// </summary>
    /// <param name="notRandomizedCards"> 아직 섞이지 않은 덱 </param>
    /// <returns></returns>
    public List<GameObject> ShuffleCards(List<GameObject> notRandomizedCards)
    {
        var shuffledcards = notRandomizedCards.OrderBy(a => Random.Range(0, notRandomizedCards.Count + 1)).ToList();
        return shuffledcards;
    }

    /// <summary>
    /// 테이블 위의 레벨별 카드덱과 귀족 카드덱의 방향을 조정
    /// </summary>
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
        
        Vector3 current;
        for (int i = 0; i < 5; i++)
        {
            current = NobleCardSpawnPoint.position;
            current.z += i * 0.35f;
            Instantiate(NobleCardPrefab[i], current, Quaternion.Euler(new Vector3(0, 270, 0)));
        }       
    }


    /// <summary>
    /// 카드가 스폰될 장소를 지정하며 한장 뽑을시 요소제거
    /// </summary>
    public void CardSpawnPoint()
    {
        Lv1CardSpawnPoint.position = current;
        Lv2CardSpawnPoint.position = current2;
        Lv3CardSpawnPoint.position = current3;

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
