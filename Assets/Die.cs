using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Die : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();

        dieSlot = GetComponentInParent<DieSlot>();
        originalDieSlot = dieSlot;
        if(dieSlot == null)
        {
            Debug.LogError("Die isn't in a DieSlot!");
        }
        dieSlot.SetDie(this, true);

        image = GetComponentInChildren<Image>();

        Roll();
    }

    RectTransform rectTransform;
    GraphicRaycaster graphicRaycaster;
    DieSlot dieSlot;
    DieSlot originalDieSlot;

    public CanvasGroup StatBlock;

    public Sprite[] SideImages;
    public int[] PipsOnSides;
    int upSide;

    Image image;

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetValue()
    {
        return PipsOnSides[upSide];
    }

    public void Roll()
    {
        SetUpSide( Random.Range(0, PipsOnSides.Length) );
    }

    void SetUpSide( int sideNum )
    {
        upSide = sideNum;

        if(upSide >= PipsOnSides.Length)
        {
            Debug.LogError("Dice only have 6 sides! Trying to set index: " + upSide);
            return;
        }

        int numPips = GetValue();

        if(numPips >= SideImages.Length)
        {
            Debug.LogError("Trying to assign too many pips to a die side: " + numPips);
            return;
        }

        image.sprite = SideImages[numPips];

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Parent ourselves directly to the canvas, and put us at the end so we render on top of everything.
        this.transform.SetParent( GetComponentInParent<Canvas>().transform );
        this.transform.SetAsLastSibling();

        StatBlock.alpha = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move with the mouse

        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if we're on a valid DieSlot, if not return to our old DieSlot

        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast( eventData, results );

        foreach(RaycastResult result in results)
        {
            DieSlot ds = result.gameObject.GetComponentInParent<DieSlot>();
            if(ds != null)
            {
                // We're in a DieSlot!
                //Debug.Log("Hit!");
                if(IsLegalForSlot(ds))
                {
                    AttachToSlot(ds);
                    return;
                }

            }
        }

        // If we get here, we weren't dropped on a slot. Re-attach to the old spot.
        //Debug.Log("Missed!");
        AttachToSlot(originalDieSlot);
        StatBlock.alpha = 1;

    }

    bool IsLegalForSlot( DieSlot ds )
    {
        if(ds.CanOccupy(this) == false)
            return false;

        // Make sure we are legal for this slot!
        if(ds.QuestDiceSlot.TargetType == QuestDeck.TARGET_TYPE.EXACT && GetValue() != ds.QuestDiceSlot.TargetPipValue)
            return false;
        if(ds.QuestDiceSlot.TargetType == QuestDeck.TARGET_TYPE.MIN && GetValue() < ds.QuestDiceSlot.TargetPipValue)
            return false;
        if(ds.QuestDiceSlot.TargetType == QuestDeck.TARGET_TYPE.MAX && GetValue() > ds.QuestDiceSlot.TargetPipValue)
            return false;

        return true;
    }

    void AttachToSlot( DieSlot ds )
    {
        dieSlot.SetDie( null );

        dieSlot = ds;
        dieSlot.SetDie( this );

        this.transform.SetParent(dieSlot.transform);
        this.transform.localPosition = Vector3.zero;
    }
}
