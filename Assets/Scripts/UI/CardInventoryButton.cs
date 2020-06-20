using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void MarkCard() 
    {
        gameObject.GetComponent<Image>().color = (RepresentedCard.MarkedForSelling()) ? Color.white : stateManager.CurrentPlayer().PlayerColor;
        RepresentedCard.MarkForSell(!RepresentedCard.MarkedForSelling());
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public ICard RepresentedCard;
    private StateManager stateManager;
    private CardManager cardManager;
}
