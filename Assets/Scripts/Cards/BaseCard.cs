using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCard : ICard
{
    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public BaseCard(int pgs)
    {
        PGS = pgs;
    }

    public virtual void PlayCard(Player player, Tile tile) 
    {
        player.PGS += this.PGS;
    }
    public abstract void DrawCard();

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public const int PRECIO_COMPRA = 3000;
    public const int PRECIO_VENTA = 1500;
    public int PGS;
}
