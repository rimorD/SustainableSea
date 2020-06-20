using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingForTurnEndAction : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static WaitingForTurnEndAction GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new WaitingForTurnEndAction();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void StateUpdate(StateManager stateManager)
    {
        if (stateManager.CurrentPlayer().CrossedInitialTile)
        {
            stateManager.currentState = BoatShopping.GetInstance();
            stateManager.boatShop.GetComponent<BoatShop>().ShowDialog
            (
                String.Format
                (
                    LangManager.GetTranslation("boat_shop_text"),
                    Definitions.CANTIDAD_A_RECIBIR_SALIDA
                )
            );
            stateManager.CurrentPlayer().CrossedInitialTile = false;
        }
        else
        {
            stateManager.NewTurn();
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForTurnEndAction StateInstance;
}
