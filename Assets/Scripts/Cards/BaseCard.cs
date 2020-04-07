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

    public abstract void DrawCard();

    //---------------------------------------------------------------------------------------------

    public string CardName()
    {
        return cardName;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int PGS;
    public string cardName;
}
