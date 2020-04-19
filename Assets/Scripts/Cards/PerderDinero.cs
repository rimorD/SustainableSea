using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderDinero : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);
        player.Money -= (player.Money > Definitions.CANTIDAD_A_PERDER_MULTA) ? Definitions.CANTIDAD_A_PERDER_MULTA : player.Money;
    }

    //---------------------------------------------------------------------------------------------

    public override bool PlayableInPlayer(Player targetPlayer)
    {
        return targetPlayer.Money > 0;
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PerderDinero(int pgs, string name) : base(pgs, name) { }
}
