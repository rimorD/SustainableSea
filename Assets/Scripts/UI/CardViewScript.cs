using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewScript : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        cardManager = GameObject.FindObjectOfType<CardManager>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        // Only update controls if were seeing them
        if(gameObject.activeSelf)
        {
            PlayerMoney.text = stateManager.CurrentPlayer().Money + " CTS";
            buyButton.interactable = stateManager.CurrentPlayer().Money >= CardManager.PRECIO_COMPRA
                                        && stateManager.CurrentPlayer().cards.Count < 3;
            sellButton.interactable = stateManager.CurrentPlayer().cards.Count > 0;
        }
    }

    //---------------------------------------------------------------------------------------------

    public void ShowCardsView(bool show)
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        gameObject.SetActive(show);
        if (show)
            LoadPlayerCards();
        else
            CleanCardsPanel();
    }

    //---------------------------------------------------------------------------------------------

    public void ShowSellingControls(bool show)
    {
        cardsViewSellingControls.SetActive(show);
        cardsViewGeneralControls.SetActive(!show);

        stateManager.CurrentPhase = (show) ? StateManager.TurnPhase.CARD_SELLING : StateManager.TurnPhase.CARD_VIEWING;
    }

    //---------------------------------------------------------------------------------------------

    public void ShowDiscardingControls(bool show)
    {
        cardsViewDiscardingControls.SetActive(show);
        cardsViewGeneralControls.SetActive(!show);
    }

    //---------------------------------------------------------------------------------------------

    public void BuyCard()
    {
        ICard drawnCard = cardManager.DrawCardFromDeck();
        stateManager.CurrentPlayer().AddCard(drawnCard);
        stateManager.CurrentPlayer().Money -= CardManager.PRECIO_COMPRA;

        LoadPlayerCards();
    }

    //---------------------------------------------------------------------------------------------

    public void LoadPlayerCards()
    {
        CleanCardsPanel();

        GameObject cardButtonPrefab = Resources.Load<GameObject>("Prefabs/CardInventoryButton");
        for(int i = 0; i < stateManager.CurrentPlayer().cards.Count; i++)
        {
            // Create a new button with card image
            GameObject newCardButton = GameObject.Instantiate(cardButtonPrefab, CardsPanel.transform);
            newCardButton.GetComponent<CardInventoryButton>().RepresentedCard = stateManager.CurrentPlayer().cards[i];
            Sprite cardImage = Resources.Load<Sprite>("Cartas/" + stateManager.CurrentPlayer().cards[i].CardName());
            newCardButton.GetComponent<Button>().GetComponent<Image>().sprite = cardImage;
        }
    }

    public void CleanCardsPanel()
    {
        for (int i = 0; i < CardsPanel.transform.childCount; ++i) 
        {
            Destroy(CardsPanel.transform.GetChild(i).gameObject); 
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    private StateManager stateManager;
    // Cards
    private CardManager cardManager;

    // UI layers
    public GameObject cardsViewGeneralControls;
    public GameObject cardsViewSellingControls;
    public GameObject cardsViewDiscardingControls;

    // UI controls
    public Button buyButton;
    public Button sellButton;
    public Text PlayerMoney;
    public GameObject CardsPanel;
}
