using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
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

    //---------------------------------------------------------------------------------------------

    public void AddCard(ICard card)
    {
        if (this.cards == null)
            cards = new List<ICard>();

        this.cards.Add(card);

        if(cards.Count > 3)
        {
            stateManager.DiscardingCard();
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public string PlayerName;
    public Color PlayerColor;
    public string PlayerAvatar;
    public int PlayerId;
    public int Money = 15000;
    public int PGS;
    private List<Boat> boats;
    public List<ICard> cards;

    private StateManager stateManager;
}
