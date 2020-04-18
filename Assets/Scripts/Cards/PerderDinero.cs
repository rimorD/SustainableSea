using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderDinero : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);
        player.SubtractMoney += Definitions.CANTIDAD_A_PERDER_MULTA;
    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {

    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PerderDinero(int pgs, string name) : base(pgs, name) { }
}
