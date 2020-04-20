using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseGameState : IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public virtual void BoatOnClick(StateManager stateManager, CardManager cardManager, Boat boat)
    {
        return;
    }

    //---------------------------------------------------------------------------------------------

    public virtual void BoatUpdate(StateManager stateManager, Boat boat)
    {
        return;
    }

    //---------------------------------------------------------------------------------------------

    public virtual void TileOnClick(StateManager stateManager, CardManager cardManager, Tile targetTile)
    {
        return;
    }

    //---------------------------------------------------------------------------------------------

    public virtual void PlayerClick(StateManager stateManager, CardManager cardManager, int playerId)
    {
        return;
    }

    //---------------------------------------------------------------------------------------------

    public virtual void InventoryCardClick(StateManager stateManager, CardManager cardManager, CardInventoryButton inventoryCard)
    {
        return;
    }
}
