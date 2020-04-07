using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        cardManager = GameObject.FindObjectOfType<CardManager>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public int GetResources()
    {
        int multiplier = 1;
        if (passiveCard != null)
        {
            multiplier *= passiveCard.resourceMultiplier;
        }
        if (furtives)
        {
            multiplier = 0;
        }
        return resources * multiplier;
    }

    //---------------------------------------------------------------------------------------------

    public void OnMouseUp()
    {
        if (stateManager.CurrentPhase != StateManager.TurnPhase.CARD_PLAYING
            || cardManager.cardPlayed == null
            || !(cardManager.cardPlayed is PassiveCard))
            return;

        cardManager.cardPlayed.PlayCard(stateManager.CurrentPlayer(), this);
        cardManager.cardPlayed = null;
        stateManager.DonePlayingCard();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Tile nextTile;
    public Tile previousTile;
    public int resources;
    public bool furtives = false;
    public bool isInitialTile = false;
    public bool isCorner = false;
    public PassiveCard passiveCard;

    private StateManager stateManager;
    private CardManager cardManager;

}
