using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardViewing : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static CardViewing GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new CardViewing();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void InventoryCardClick(StateManager stateManager, CardManager cardManager, CardInventoryButton inventoryCard)
    {
        cardManager.cardPlayed = inventoryCard.RepresentedCard;

        stateManager.PlayingCard();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static CardViewing StateInstance;
}
