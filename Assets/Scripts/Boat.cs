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
        if (isAnimating && stateManager.CurrentPhase == StateManager.TurnPhase.WAITING_FOR_ANIMATION)
        {
            if (currentMovement != null && Vector3.Distance(transform.position, currentMovement.finalPosition) > SMOOTH_DISTANCE)
            {
                this.transform.position = Vector3.SmoothDamp(transform.position, currentMovement.finalPosition, ref velocity, SMOOTH_TIME);
            }
            else if(currentMovement != null && currentMovement.hasFinalRotation && Quaternion.Angle(transform.rotation, currentMovement.finalRotation) > MAX_DEGREES_DELTA)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, currentMovement.finalRotation, MAX_DEGREES_DELTA);
            }
            else
            {
                if (pendingMovements.Count > 0)
                    currentMovement = pendingMovements.Dequeue();
                else
                {
                    currentMovement = null;
                    isAnimating = false;
                    stateManager.NewTurn();
                }
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
        pendingMovements = new Queue<Movement>();
        // Move out of the tile
        pendingMovements.Enqueue
        (
            new Movement
            (
                new Vector3
                (
                    currentTile.transform.position.x, 
                    currentTile.transform.position.y + SMOOTH_HEIGHT, 
                    currentTile.transform.position.z
                )
            )
        );

        // Move
        int tilesToMove = stateManager.LastRollResult;
        for(int i = 0; i < tilesToMove; i++)
        {
            currentTile = currentTile.nextTile;
            if (currentTile.isCorner)
            {
                // Move to corner
                pendingMovements.Enqueue
                (
                    new Movement
                    (
                        new Vector3(currentTile.transform.position.x, currentTile.transform.position.y + SMOOTH_HEIGHT, currentTile.transform.position.z),
                        Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z)
                    )
                );
            }

            if (currentTile.isInitialTile)
            {
                // Do whatever, get money, etc...
            }
        }

        // Final destination
        pendingMovements.Enqueue
        (
            new Movement
            (
                new Vector3
                (
                    currentTile.transform.position.x, 
                    currentTile.transform.position.y + SMOOTH_HEIGHT, 
                    currentTile.transform.position.z
                )
            )
        );


        // Move into the tile
        pendingMovements.Enqueue(new Movement(currentTile.transform.position));
        stateManager.CurrentPhase = StateManager.TurnPhase.WAITING_FOR_ANIMATION;
        isAnimating = true;

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
                Owner.AddCard(drawnCard);
            }
        }
        // Move furtives if we rolled 1

        // Resolve resources in our destination
        this.Owner.AddMoney += currentTile.GetResources();
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

    public enum BoatType { ARTISANAL, TRAIL }
    public BoatType boatType = BoatType.ARTISANAL;

    // Animation stuff
    Movement currentMovement;
    bool isAnimating = false;
    Queue<Movement> pendingMovements = new Queue<Movement>();
    Vector3 velocity;
    const float SMOOTH_TIME = 0.25f;
    const float SMOOTH_DISTANCE = 0.01f;
    const float SMOOTH_HEIGHT = 0.5f;
    const float MAX_DEGREES_DELTA = 2.5f;
}
