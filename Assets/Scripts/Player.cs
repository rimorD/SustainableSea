using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        cards = new List<ICard>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public void AddBoat(Boat boat)
    {
        if(this.boats == null)
            boats = new List<Boat>();

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
    public int PGS;
    private List<Boat> boats;
    public List<ICard> cards;
}
