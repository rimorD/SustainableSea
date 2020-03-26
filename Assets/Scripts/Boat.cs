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
        if (stateManager.CurrentPhase == StateManager.TurnPhase.WAITING_FOR_ANIMATION)
        {
            if (transform.position != targetPosition)
            {
                this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SMOOTH_TIME);
            }
            else
            {
                if (pendingTargetPositions.Count > 0)
                    targetPosition = pendingTargetPositions.Dequeue();
                else
                    stateManager.NewTurn();
            }
        }
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

        // Initialize movement queue
        pendingTargetPositions = new Queue<Vector3>();
        // Move out of the tile
        pendingTargetPositions.Enqueue(currentTile.transform.Find("BoatPlaceholder").position);

        // Move
        int tilesToMove = stateManager.LastRollResult;
        for(int i = 0; i < tilesToMove; i++)
        {
            currentTile = currentTile.nextTile;
            if (currentTile.isCorner)
            {
                // Rotate
                this.transform.Rotate(0, 90, 0);
                // Move to corner
                pendingTargetPositions.Enqueue(currentTile.transform.Find("BoatPlaceholder").position);
            }

            if (currentTile.isInitialTile)
            {
                // Do whatever, get money, etc...
            }
        }

        // Final destination
        pendingTargetPositions.Enqueue(currentTile.transform.Find("BoatPlaceholder").position);
        // Move into the tile
        pendingTargetPositions.Enqueue(currentTile.transform.position);

        stateManager.CurrentPhase = StateManager.TurnPhase.WAITING_FOR_ANIMATION;

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

    // Animation stuff
    Vector3 targetPosition;
    Quaternion targetRotation;
    Queue<Vector3> pendingTargetPositions = new Queue<Vector3>();
    Vector3 velocity;
    const float SMOOTH_TIME = 0.25f;
}
