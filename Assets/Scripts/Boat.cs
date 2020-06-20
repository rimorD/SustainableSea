using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
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
        stateManager.CurrentState.BoatUpdate(stateManager, this);
    }

    //---------------------------------------------------------------------------------------------

    void OnMouseUp()
    {
        stateManager.CurrentState.BoatOnClick(stateManager, cardManager, this);
    }

    //---------------------------------------------------------------------------------------------

    public Vector3 GetPositionForCurrentTile()
    {
        return currentTile.GetBoatPosition(this);
    }

    //---------------------------------------------------------------------------------------------

    public void Move(Movement move)
    {
        pendingMovements.Enqueue(move);
    }

    //---------------------------------------------------------------------------------------------

    public void LeaveTile()
    {
        currentTile.boatsInTile.Remove(this);
        if(boatType == BoatType.ARTISANAL)
        {
            currentTile.artisanalBoatsOnTile--;
        }
        else
        {
            currentTile.trailBoatsOnTile--;
        }
    }
    
    //---------------------------------------------------------------------------------------------

    public void ArriveAtTile()
    {
        currentTile.boatsInTile.Add(this);
        if (boatType == BoatType.ARTISANAL)
        {
            currentTile.artisanalBoatsOnTile++;
        }
        else
        {
            currentTile.trailBoatsOnTile++;
        }
    }

    //---------------------------------------------------------------------------------------------

    public void CollectResources()
    {
        int resources = currentTile.GetResources();
        if(boatType == BoatType.TRAIL)
        {
            resources *= 5;
            currentTile.MarkAsOverexploited();
        }
        Owner.Money += resources;
    }

    // Attributes /////////////////////////////////////////////////////////////////////////////////

    public void SetCurrentTile (Tile tile)
    {
        currentTile = tile;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    StateManager stateManager;
    public Tile currentTile;
    public Player Owner;
    CardManager cardManager;

    public enum BoatType { ARTISANAL, TRAIL }
    public BoatType boatType = BoatType.ARTISANAL;

    // Animation stuff
    public Movement currentMovement;
    public bool isAnimating = false;
    public Queue<Movement> pendingMovements = new Queue<Movement>();
    public Vector3 velocity;
}
