using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
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

    void OnMouseUp()
    {
        // TODO: Check if UI element is in the way

        // TODO: Check if is our turn
        if (stateManager.CurrentPlayerId != this.Owner.PlayerId)
            return;

        // Check if weve rolled the dice
        if (stateManager.CurrentPhase != StateManager.TurnPhase.WAITING_FOR_CLICK)
            return;
        
        // Move
        int tilesToMove = stateManager.LastRollResult;
        for(int i = 0; i < tilesToMove; i++)
        {
            currentTile = currentTile.nextTile;
            if (currentTile.isCorner)
            {
                // Rotate
                this.transform.Rotate(0, 90, 0);
            }

            if (currentTile.isInitialTile)
            {
                // Do whatever, get money, etc...
            }
        }

        // Teleport to final position
        this.transform.position = currentTile.transform.position;

        // Get a card if we rolled 6
        if (stateManager.LastRollResult == 6)
        {
            // Get a card
            ICard drawnCard = cardManager.DrawCardFromDeck();
            if(drawnCard is PassiveCard)
            {
                drawnCard.PlayCard(Owner, currentTile);
            }
            else
            {
                Owner.cards.Add(drawnCard);
            }
        }
        // Move furtives if we rolled 1

        // Resolve resources in our destination
        this.Owner.Money += currentTile.GetResources();

        // End turn
        //The die is ready to roll again
        // TODO: Animate the boat, and wait for the animation to end
        stateManager.NewTurn();
    }

    // Attributes /////////////////////////////////////////////////////////////////////////////////

    public void SetCurrentTile (Tile tile)
    {
        currentTile = tile;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    StateManager stateManager;
    Tile currentTile;
    public Player Owner;
    CardManager cardManager;
}
