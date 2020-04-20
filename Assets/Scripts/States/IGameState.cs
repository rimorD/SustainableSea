using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IGameState
{
    void BoatUpdate(StateManager stateManager, Boat boat);
    void BoatOnClick(StateManager stateManager, CardManager cardManager, Boat boat);
    void TileOnClick(StateManager stateManager, CardManager cardManager, Tile targetTile);
    void PlayerClick(StateManager stateManager, CardManager cardManager, int playerId);
    void InventoryCardClick(StateManager stateManager, CardManager cardManager, CardInventoryButton inventoryCard);
}
