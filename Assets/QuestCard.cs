    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TextMeshProUGUI TitleText;

    public GameObject DieSlotPrefab;
    public Transform DieSlotContainer;

    public void SetCardData(QuestDeck.QuestCardData cardData)
    {
        Debug.Log("SetCardData");
        TitleText.text = cardData.Title;

        foreach( QuestDeck.QuestDiceSlot qds in cardData.QuestDiceSlots )
        {
            GameObject slot = Instantiate( DieSlotPrefab, Vector3.zero, Quaternion.identity, DieSlotContainer );
            slot.GetComponent<DieSlot>().QuestDiceSlot = qds;

            if(qds.TargetPipValue > 0 || qds.TargetType == QuestDeck.TARGET_TYPE.EXACT )
            {
                TextMeshProUGUI t = slot.GetComponentInChildren<TextMeshProUGUI>();

                if(qds.TargetType == QuestDeck.TARGET_TYPE.EXACT)
                {
                    t.text = "=";
                }

                t.text += qds.TargetPipValue.ToString();

                if(qds.TargetType == QuestDeck.TARGET_TYPE.MIN)
                {
                    t.text += "▲";
                }
                else if(qds.TargetType == QuestDeck.TARGET_TYPE.MAX)
                {
                    t.text += "▼";
                }


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
