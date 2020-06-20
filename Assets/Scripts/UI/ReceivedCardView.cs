using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceivedCardView : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {

    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {

    }

    //---------------------------------------------------------------------------------------------

    public void Show(string CardName)
    {
        gameObject.SetActive(true);
        Sprite cardImage = Resources.Load<Sprite>("Cartas/" + CardName);
        RepresentedCard.sprite = cardImage;
    }

    //---------------------------------------------------------------------------------------------

    public void Hide()
    {
        gameObject.SetActive(false);
        if (stateManager.CurrentPlayer().cards.Count > 3)
        {
            stateManager.DiscardingCard();
        }
        else
        {
            stateManager.CurrentState = WaitingForAnimation.GetInstance();
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Image RepresentedCard;
    public StateManager stateManager;
}
