using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        cardManager = GameObject.FindObjectOfType<CardManager>();
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
        if (furtives || overexploited)
        {
            multiplier = 0;
        }
        return resources * multiplier;
    }

    //---------------------------------------------------------------------------------------------

    public void OnMouseUp()
    {
        stateManager.CurrentState.TileOnClick(stateManager, cardManager, this);
    }

    //---------------------------------------------------------------------------------------------

    public Vector3 GetBoatPosition(Boat boat)
    {
        string placeholder = string.Empty;
        if (BoatsOfBothTypesOnTile())
        {
            string boatType = (boat.boatType == Boat.BoatType.ARTISANAL) ? "ART" : "TRA";
            placeholder = string.Format("BoatPlaceholder_{0}_{1}_{2}", StateManager.NumberOfPlayers, boat.Owner.PlayerId, boatType);
        }
        else
        {
            placeholder = string.Format("BoatPlaceholder_{0}_{1}", StateManager.NumberOfPlayers, boat.Owner.PlayerId);
        }
        return gameObject.transform.Find(placeholder).transform.position;
    }

    //---------------------------------------------------------------------------------------------

    public bool BoatsOfBothTypesOnTile()
    {
        return artisanalBoatsOnTile > 0 && trailBoatsOnTile > 0;
    }

    //---------------------------------------------------------------------------------------------

    public void MarkAsOverexploited()
    {
        overexploited = true;
        gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Materiales/TileMaterials/Overexploited");
    }

    //---------------------------------------------------------------------------------------------

    public void UpdateBoatPositions(Boat newBoat)
    {
        foreach(Boat boat in boatsInTile)
        {
            if (boat != newBoat)
            {
                Vector3 newPosition = boat.GetPositionForCurrentTile();
                if (boat.gameObject.transform.position != newPosition)
                {
                    boat.Move(new Movement(newPosition));
                    boat.isAnimating = true;
                    WaitingForAnimation.PendingAnimations.Add(boat.gameObject);
                }
            }
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Tile nextTile;
    public Tile previousTile;
    public int resources;
    public bool furtives = false;
    public bool overexploited = false;
    public bool isInitialTile = false;
    public bool isCorner = false;
    public PassiveCard passiveCard;

    private StateManager stateManager;
    private CardManager cardManager;

    public int artisanalBoatsOnTile = 0;
    public int trailBoatsOnTile = 0;
    public List<Boat> boatsInTile = new List<Boat>();
}
