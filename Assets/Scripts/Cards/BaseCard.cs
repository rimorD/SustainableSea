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
        Debug.Log(string.Format("Playing card {0}", this.cardName));
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

    public bool MarkedForSelling()
    {
        return markedForSelling;
    }

    //---------------------------------------------------------------------------------------------

    public void MarkForSell(bool mark)
    {
        markedForSelling = mark;
    }

    //---------------------------------------------------------------------------------------------

    public string CardName()
    {
        return cardName;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int PGS;
    public string cardName;
    public bool markedForSelling;
}
