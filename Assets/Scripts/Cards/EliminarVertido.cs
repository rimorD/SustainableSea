using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarVertido : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);
        PassiveCard.RemoveCard(tile);
    }


    //---------------------------------------------------------------------------------------------

    public override bool PlayableInTile(Tile targetTile)
    {
        return targetTile.passiveCard != null 
            && targetTile.passiveCard.CardName() == "OilSpill"
            && !targetTile.passiveCard.isCloneFromAdyacent;
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public EliminarVertido(int pgs, string name) : base(pgs, name) { }
}
