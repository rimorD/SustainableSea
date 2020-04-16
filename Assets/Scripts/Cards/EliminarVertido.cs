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

    public override void DrawCard()
    {
        
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public EliminarVertido(int pgs, string name) : base(pgs, name) { }
}
