using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderDinero : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {

    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {

    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PerderDinero(int pgs) : base(pgs) { }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public const int CANTIDAD_A_PERDER = 2000;
}
