﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furtivos : BaseCard, ICard
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

    public Furtivos(int pgs) : base(pgs) { }
}