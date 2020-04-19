using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCard : BaseCard, ICard
{

    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        base.PlayCard(player, tile);

        if(tile.passiveCard != null)
        {
            RemoveCard(tile);
        }

        tile.passiveCard = this;
        tileAttachedTo = tile;
        DrawCard();

        if (this.affectsAdyacent)
        {
            // We cant create a single card, otherwise there are errors when trying to remove them
            PassiveCard nextTileCard = new PassiveCard(0, adyacentMultiplier, cardName);
            nextTileCard.isCloneFromAdyacent = true;
            nextTileCard.PlayCard(player, tile.nextTile);

            PassiveCard previousTileCard = new PassiveCard(0, adyacentMultiplier, cardName);
            previousTileCard.isCloneFromAdyacent = true;
            previousTileCard.PlayCard(player, tile.previousTile);
        }
    }

    //---------------------------------------------------------------------------------------------

    public void DrawCard()
    {
        // Load specific card prefab
        GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Cards/" + cardName);
        // Draw it in the tile placeholder
        prefabInstance = GameObject.Instantiate(cardPrefab, tileAttachedTo.transform.Find("CardPlaceholder").transform.position, tileAttachedTo.transform.rotation, tileAttachedTo.transform.parent);
    }

    //---------------------------------------------------------------------------------------------

    public static void RemoveCard(Tile tile)
    {   
        if (tile.passiveCard.affectsAdyacent)
        {
            RemoveCard(tile.nextTile);
            RemoveCard(tile.previousTile);
        }

        GameObject.Destroy(tile.passiveCard.prefabInstance);
        tile.passiveCard.prefabInstance = null;

        tile.passiveCard = null;
    }

    //---------------------------------------------------------------------------------------------

    public override bool PlayableInTile(Tile targetTile)
    {
        return true;
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PassiveCard(int pgs, int multiplier, string name) : base(pgs, name)
    {
        resourceMultiplier = multiplier;
        affectsAdyacent = false;
        adyacentMultiplier = 1;
    }

    //---------------------------------------------------------------------------------------------

    public PassiveCard(int pgs, int multiplier, string name, int adyacentTileMultiplier) : base(pgs, name)
    {
        resourceMultiplier = multiplier;
        affectsAdyacent = true;
        adyacentMultiplier = adyacentTileMultiplier;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int resourceMultiplier;
    public bool affectsAdyacent;
    public bool isCloneFromAdyacent;
    public int adyacentMultiplier;
    Tile tileAttachedTo;
    GameObject prefabInstance;
}
