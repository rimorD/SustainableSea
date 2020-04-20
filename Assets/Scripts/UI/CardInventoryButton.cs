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
        stateManager.CurrentState.InventoryCardClick(stateManager, cardManager, this);
    }

    public void RemoveCard()
    {
        stateManager.CurrentPlayer().cards.Remove(RepresentedCard);
        cardManager.DiscardPile.Add(RepresentedCard);
        Destroy(gameObject);
        stateManager.cardsView.GetComponent<CardMenu>().LoadPlayerCards();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public ICard RepresentedCard;
    private StateManager stateManager;
    private CardManager cardManager;
}
