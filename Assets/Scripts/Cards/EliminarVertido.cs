using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarVertido : BaseCard, ICard
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

    public EliminarVertido(int pgs) : base(pgs) { }
}
