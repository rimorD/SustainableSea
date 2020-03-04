using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        boats = new List<Boat>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public void AddBoat(Boat boat)
    {
        this.boats.Add(boat);
        boat.Owner = this;

        // Bastante mejorable...
        boat.GetComponentsInChildren<MeshRenderer>()[0].material.color = this.PlayerColor;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public string PlayerName;
    public Color PlayerColor;
    public string PlayerAvatar;
    public int PlayerId;
    public int Money;
    private List<Boat> boats;
    //private Card[] cards;
}
