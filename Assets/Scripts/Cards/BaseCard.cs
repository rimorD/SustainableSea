using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCard : ICard
{
    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public BaseCard(int pgs, string name)
    {
        PGS = pgs;
        cardName = name;
    }

    //---------------------------------------------------------------------------------------------

    public virtual void PlayCard(Player player, Tile tile) 
    {
        player.PGS += this.PGS;
    }

    //---------------------------------------------------------------------------------------------

    public virtual bool PlayableInTile(Tile targetTile)
    {
        return false;
    }

    //---------------------------------------------------------------------------------------------

    public virtual bool PlayableInPlayer(Player targetPlayer)
    {
        return false;
    }

    //---------------------------------------------------------------------------------------------

    public string CardName()
    {
        return cardName;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int PGS;
    public string cardName;
}
