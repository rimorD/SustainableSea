using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderTurno : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);
        player.LostTurn = true;
    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {

    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PerderTurno(int pgs, string name) : base(pgs, name) { }
}
