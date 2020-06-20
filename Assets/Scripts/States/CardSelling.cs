using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardSelling : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static CardSelling GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new CardSelling();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void InventoryCardClick(StateManager stateManager, CardManager cardManager, CardInventoryButton inventoryCard)
    {
        inventoryCard.MarkCard();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static CardSelling StateInstance;
}
