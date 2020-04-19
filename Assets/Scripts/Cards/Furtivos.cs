using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furtivos : BaseCard, ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);

        GameObject.FindObjectOfType<StateManager>().Furtives.Move(tile);
    }

    //---------------------------------------------------------------------------------------------

    public override bool PlayableInTile(Tile targetTile)
    {
        return !targetTile.furtives;
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public Furtivos(int pgs, string name) : base(pgs, name) { }
}
