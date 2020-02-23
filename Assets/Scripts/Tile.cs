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

    // Setters and getters ////////////////////////////////////////////////////////////////////////
    
    public Tile GetNextTile()
    {
        return nextTile;
    }

    //---------------------------------------------------------------------------------------------

    public void SetNextTile(Tile next)
    {
        nextTile = next;
    }

    //---------------------------------------------------------------------------------------------

    public int GetResources()
    {
        return resources;
    }

    //---------------------------------------------------------------------------------------------

    public void SetResources(int rec)
    {
        resources = rec;
    }

    //---------------------------------------------------------------------------------------------

    public bool AreThereFurtives()
    {
        return furtives;
    }

    //---------------------------------------------------------------------------------------------

    public void SetFurtives(bool furt)
    {
        furtives = furt;
    }

    //---------------------------------------------------------------------------------------------

    public bool IsInitialTile()
    {
        return isInitialTile;
    }

    //---------------------------------------------------------------------------------------------

    public void SetAsInitialTile()
    {
        isInitialTile = true;
    }

    //---------------------------------------------------------------------------------------------

    public bool IsCorner()
    {
        return isCorner;
    }

    //---------------------------------------------------------------------------------------------

    public void SetAsCorner()
    {
        isCorner = true;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    Tile nextTile;
    int resources;
    bool furtives = false;
    bool isInitialTile = false;
    bool isCorner = false;

}
