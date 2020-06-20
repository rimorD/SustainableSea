using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardPlaying : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static CardPlaying GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new CardPlaying();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void TileOnClick(StateManager stateManager, CardManager cardManager, Tile targetTile)
    {
        if (cardManager.cardPlayed == null
        || !cardManager.cardPlayed.PlayableInTile(targetTile))
            return;

        cardManager.cardPlayed.PlayCard(stateManager.CurrentPlayer(), targetTile);
        stateManager.DonePlayingCard();
        cardManager.cardPlayed = null;
    }

    //---------------------------------------------------------------------------------------------

    public override void PlayerClick(StateManager stateManager, CardManager cardManager, int playerId)
    {
        if (cardManager.cardPlayed == null
            || !cardManager.cardPlayed.PlayableInPlayer(stateManager.Players[playerId]))
            return;

        cardManager.cardPlayed.PlayCard(stateManager.Players[playerId], null);
        stateManager.DonePlayingCard();
        cardManager.cardPlayed = null;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static CardPlaying StateInstance;
}
