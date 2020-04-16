﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderDinero : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);
        player.SubtractMoney += CANTIDAD_A_PERDER;
    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {

    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PerderDinero(int pgs, string name) : base(pgs, name) { }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public const int CANTIDAD_A_PERDER = 2000;
}
