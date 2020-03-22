using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirarDado : BaseCard, ICard
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

    public TirarDado(int pgs) : base(pgs) { }
}
