using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public QuestDeck.QuestDiceSlot QuestDiceSlot;  // Hang on to this so we can filter drops


    Die starterDie;
    Die die;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanOccupy( Die d )
    {
        if(starterDie != null && d != starterDie)
            return false;

        return die == null;
    }

    public void SetDie( Die d, bool setStarter = false )
    {
        die = d;

        if(setStarter)
        {
            starterDie = d;
        }
        
    }
}
