using System.Collections;
using System.Linq;
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

        boat.GetComponentInChildren<MeshRenderer>().material.color = this.PlayerColor;
    }

    //---------------------------------------------------------------------------------------------

    public void ConfirmCardSell()
    {
        for(int i=cards.Count - 1; i>=0; i--)
        {
            if (cards[i].MarkedForSelling())
            {
                GameObject.FindObjectOfType<CardManager>().DiscardPile.Add(cards[i]);
                cards.RemoveAt(i);
                Money += Definitions.PRECIO_VENTA_CARTAS;
            }
        }
        stateManager.cardsView.GetComponent<CardMenu>().LoadPlayerCards();
    }

    //---------------------------------------------------------------------------------------------

    public void RemoveCard(ICard card)
    {
        cards.Remove(card);
        stateManager.cardManager.DiscardPile.Add(card);
    }

    //---------------------------------------------------------------------------------------------

    public void AddCard(ICard card)
    {
        if (this.cards == null)
            cards = new List<ICard>();

        this.cards.Add(card);
    }

    //---------------------------------------------------------------------------------------------

    public override string ToString()
    {
        return string.Format("{0}_id{1}", PlayerName, PlayerId);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public string PlayerName;
    public Color PlayerColor;
    public string PlayerAvatar;
    public int PlayerId;
    public int Money = 0;
    public int PGS;
    public bool LostTurn = false;
    public bool CrossedInitialTile = false;
    public List<Boat> boats;
    public List<ICard> cards;

    private StateManager stateManager;
}
