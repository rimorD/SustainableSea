using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderTurno : BaseCard, ICard
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

    public PerderTurno(int pgs) : base(pgs) { }
}
