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
        boat.Move
        (
            new Movement
            (
                boat.GetPositionForCurrentTile() + Vector3.up * Definitions.SMOOTH_HEIGHT
            )
        );
        boat.LeaveTile();

        // Move
        int tilesToMove = stateManager.LastRollResult;
        for (int i = 0; i < tilesToMove; i++)
        {
            boat.currentTile = boat.currentTile.nextTile;
            if (boat.currentTile.isCorner && i + 1 < tilesToMove)
            {
                // Move to corner
                boat.Move
                (
                    new Movement
                    (
                        boat.GetPositionForCurrentTile() + Vector3.up * Definitions.SMOOTH_HEIGHT,
                        Quaternion.Euler(boat.gameObject.transform.eulerAngles.x, boat.gameObject.transform.eulerAngles.y + 90, boat.gameObject.transform.eulerAngles.z)
                    )
                );
            }

            if (boat.currentTile.isInitialTile)
            {
                stateManager.CurrentPlayer().Money += Definitions.CANTIDAD_A_RECIBIR_SALIDA;
                stateManager.CurrentPlayer().CrossedInitialTile = true;
            }
        }

        // Final destination
        boat.ArriveAtTile();
        boat.currentTile.UpdateBoatPositions(boat);
        Vector3 finalPosition = boat.GetPositionForCurrentTile();
        boat.Move
        (
            new Movement
            (
                finalPosition + Vector3.up * Definitions.SMOOTH_HEIGHT
            )
        );


        // Move into the tile
        boat.Move
        (
            new Movement(finalPosition)
        );
        boat.isAnimating = true;
        WaitingForAnimation.PendingAnimations.Add(boat.gameObject);
        stateManager.CurrentState = WaitingForAnimation.GetInstance();

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

            stateManager.ShowCardReceived(drawnCard.CardName());
        }
        // Move furtives if we rolled 1
        if (stateManager.LastRollResult == 1)
        {
            stateManager.Furtives.Move(boat.currentTile);
        }

        // Resolve resources in our destination
        boat.CollectResources();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForClick StateInstance;
}
