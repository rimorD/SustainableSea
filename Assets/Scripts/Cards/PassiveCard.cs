using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCard : BaseCard, ICard
{

    // Methods ////////////////////////////////////////////////////////////////////////////////////
    public override void PlayCard(Player player, Tile tile)
    {
        tile.passiveCard = this;
        tileAttachedTo = tile;
        DrawCard();
    }

    //---------------------------------------------------------------------------------------------

    public override void DrawCard()
    {
        // Load specific card prefab
        GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Cards/" + cardName);
        // Draw it in the tile placeholder
        GameObject.Instantiate(cardPrefab, tileAttachedTo.transform.Find("CardPlaceholder").transform.position, tileAttachedTo.transform.rotation);
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
}
