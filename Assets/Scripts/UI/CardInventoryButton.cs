using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        cardManager = GameObject.FindObjectOfType<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        switch (stateManager.CurrentPhase) 
        {
            case StateManager.TurnPhase.CARD_DISCARDING:
                stateManager.CurrentPlayer().AddMoney += Definitions.PRECIO_VENTA_CARTAS;
                RemoveCard();
                if(stateManager.CurrentPlayer().cards.Count <= 3)
                    stateManager.DoneDiscardingCard();
                break;
            case StateManager.TurnPhase.CARD_SELLING:
                stateManager.CurrentPlayer().AddMoney += Definitions.PRECIO_VENTA_CARTAS;

                RemoveCard();
                break;
            case StateManager.TurnPhase.CARD_VIEWING:
                cardManager.cardPlayed = RepresentedCard;

                stateManager.PlayingCard();

                RemoveCard();
                break;
        }
    }

    private void RemoveCard()
    {
        stateManager.CurrentPlayer().cards.Remove(RepresentedCard);
        cardManager.DiscardPile.Add(RepresentedCard);
        Destroy(gameObject);
        stateManager.cardsView.GetComponent<CardViewScript>().LoadPlayerCards();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public ICard RepresentedCard;
    private StateManager stateManager;
    private CardManager cardManager;
}
