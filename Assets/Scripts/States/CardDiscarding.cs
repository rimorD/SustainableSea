using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardDiscarding : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static CardDiscarding GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new CardDiscarding();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void InventoryCardClick(StateManager stateManager, CardManager cardManager, CardInventoryButton inventoryCard)
    {
        inventoryCard.MarkCard();
        if (stateManager.CurrentPlayer().cards.Count - stateManager.CurrentPlayer().cards.Where(card => card.MarkedForSelling()).Count() <= 3)
        {
            stateManager.CurrentPlayer().ConfirmCardSell();
            stateManager.DoneDiscardingCard();
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static CardDiscarding StateInstance;
}
