using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public int GetResources()
    {
        int multiplier = 1;
        if (passiveCard != null)
        {
            multiplier *= passiveCard.resourceMultiplier;
        }
        if (nextTile.passiveCard != null && nextTile.passiveCard.affectsAdyacent)
        {
            multiplier *= nextTile.passiveCard.adyacentMultiplier;
        }
        if (previousTile.passiveCard != null && previousTile.passiveCard.affectsAdyacent)
        {
            multiplier *= previousTile.passiveCard.adyacentMultiplier;
        }
        if (furtives)
        {
            multiplier = 0;
        }
        return resources * multiplier;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Tile nextTile;
    public Tile previousTile;
    public int resources;
    public bool furtives = false;
    public bool isInitialTile = false;
    public bool isCorner = false;
    public PassiveCard passiveCard;

}
