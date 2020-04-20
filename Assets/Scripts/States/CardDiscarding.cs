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
        stateManager.CurrentPlayer().Money += Definitions.PRECIO_VENTA_CARTAS;
        inventoryCard.RemoveCard();
        if (stateManager.CurrentPlayer().cards.Count <= 3)
            stateManager.DoneDiscardingCard();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static CardDiscarding StateInstance;
}
