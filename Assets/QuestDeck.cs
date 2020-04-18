using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDeck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DrawCard();
    }

    public enum TARGET_TYPE { MIN, MAX, EXACT };

    public Transform QuestBox;
    public GameObject QuestCardPrefab;

    [System.Serializable]
    public class QuestCardData
    {
        public string Title;

        public QuestDiceSlot[] QuestDiceSlots;

        public int ValueTarget;

        public QuestDiceSlot BonusDiceSlot; // This die will gain a pip

    }

    [System.Serializable]
    public class QuestDiceSlot
    {
        public int TargetPipValue;
        public TARGET_TYPE TargetType;
    }

    public QuestCardData[] QuestCards;



    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawCard()
    {
        Debug.Log("DrawCard");
        QuestCardData card = QuestCards[0];

        GameObject go = Instantiate(QuestCardPrefab, Vector3.zero, Quaternion.identity, QuestBox);
        go.GetComponent<QuestCard>().SetCardData(card);
    }
}

