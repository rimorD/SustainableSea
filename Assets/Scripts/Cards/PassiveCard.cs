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
            GameObject.Destroy(tile.passiveCard.prefabInstance);
        }

        tile.passiveCard = this;
        tileAttachedTo = tile;
        DrawCard();

        if (this.affectsAdyacent)
        {
            PassiveCard adyacentCard = new PassiveCard(0, adyacentMultiplier, cardName);
            adyacentCard.PlayCard(player, tile.nextTile);
            adyacentCard.PlayCard(player, tile.previousTile);
        }
    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {
        // Load specific card prefab
        GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Cards/" + cardName);
        // Draw it in the tile placeholder
        prefabInstance = GameObject.Instantiate(cardPrefab, tileAttachedTo.transform.Find("CardPlaceholder").transform.position, tileAttachedTo.transform.rotation, tileAttachedTo.transform.parent);
    }

    // Ctor ///////////////////////////////////////////////////////////////////////////////////////

    public PassiveCard(int pgs, int multiplier, string name) : base(pgs)
    {
        resourceMultiplier = multiplier;
        cardName = name;
        affectsAdyacent = false;
        adyacentMultiplier = 1;
    }

    //---------------------------------------------------------------------------------------------

    public PassiveCard(int pgs, int multiplier, string name, int adyacentTileMultiplier) : base(pgs)
    {
        resourceMultiplier = multiplier;
        cardName = name;
        affectsAdyacent = true;
        adyacentMultiplier = adyacentTileMultiplier;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int resourceMultiplier;
    public string cardName;
    public bool affectsAdyacent;
    public int adyacentMultiplier;
    Tile tileAttachedTo;
    GameObject prefabInstance;
}
