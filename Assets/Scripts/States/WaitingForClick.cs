using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingForClick : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static WaitingForClick GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new WaitingForClick();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void BoatOnClick(StateManager stateManager, CardManager cardManager, Boat boat)
    {
        if (stateManager.CurrentPlayerId != boat.Owner.PlayerId)
            return;

        // Initialize movement queue
        boat.pendingMovements = new Queue<Movement>();
        // Move out of the tile
        boat.pendingMovements.Enqueue
        (
            new Movement
            (
                new Vector3
                (
                    boat.currentTile.transform.position.x,
                    boat.currentTile.transform.position.y + Definitions.SMOOTH_HEIGHT,
                    boat.currentTile.transform.position.z
                )
            )
        );

        // Move
        int tilesToMove = stateManager.LastRollResult;
        for (int i = 0; i < tilesToMove; i++)
        {
            boat.currentTile = boat.currentTile.nextTile;
            if (boat.currentTile.isCorner)
            {
                // Move to corner
                boat.pendingMovements.Enqueue
                (
                    new Movement
                    (
                        new Vector3(boat.currentTile.transform.position.x, boat.currentTile.transform.position.y + Definitions.SMOOTH_HEIGHT, boat.currentTile.transform.position.z),
                        Quaternion.Euler(boat.gameObject.transform.eulerAngles.x, boat.gameObject.transform.eulerAngles.y + 90, boat.gameObject.transform.eulerAngles.z)
                    )
                );
            }

            if (boat.currentTile.isInitialTile)
            {
                // Do whatever, get money, etc...
            }
        }

        // Final destination
        boat.pendingMovements.Enqueue
        (
            new Movement
            (
                new Vector3
                (
                    boat.currentTile.transform.position.x,
                    boat.currentTile.transform.position.y + Definitions.SMOOTH_HEIGHT,
                    boat.currentTile.transform.position.z
                )
            )
        );


        // Move into the tile
        boat.pendingMovements.Enqueue(new Movement(boat.currentTile.transform.position));
        stateManager.CurrentState = WaitingForAnimation.GetInstance();
        boat.isAnimating = true;

        // Get a card if we rolled 6
        if (stateManager.LastRollResult == 6)
        {
            // Get a card
            ICard drawnCard = cardManager.DrawCardFromDeck();
            if (drawnCard is PassiveCard)
            {
                drawnCard.PlayCard(boat.Owner, boat.currentTile);
            }
            else
            {
                boat.Owner.AddCard(drawnCard);
            }
        }
        // Move furtives if we rolled 1
        if (stateManager.LastRollResult == 1)
        {
            stateManager.Furtives.Move(boat.currentTile);
        }

        // Resolve resources in our destination
        boat.Owner.Money += boat.currentTile.GetResources();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForClick StateInstance;
}
