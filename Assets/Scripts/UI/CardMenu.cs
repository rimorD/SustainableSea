using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMenu : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        cardManager = GameObject.FindObjectOfType<CardManager>();
        BuyingTip.GetComponentInChildren<Text>().text = LangManager.GetTranslation(string.Format(LangManager.GetTranslation("currency_abreviation"), Definitions.PRECIO_COMPRA_CARTAS));
        SellingTip.GetComponentInChildren<Text>().text = LangManager.GetTranslation(string.Format(LangManager.GetTranslation("currency_abreviation"), Definitions.PRECIO_VENTA_CARTAS));
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        PlayerMoney.text = string.Format(LangManager.GetTranslation("currency_abreviation"), stateManager.CurrentPlayer().Money);
        buyButton.interactable = stateManager.CurrentPlayer().Money >= Definitions.PRECIO_COMPRA_CARTAS
                                    && stateManager.CurrentPlayer().cards.Count < 3;
        sellButton.interactable = stateManager.CurrentPlayer().cards.Count > 0;
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

        if (show)
        {
            stateManager.CurrentState = CardSelling.GetInstance();
        }
        else
        {
            stateManager.CurrentState = CardViewing.GetInstance();
            stateManager.CurrentPlayer().ConfirmCardSell();
        }

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
        string dialogText = string.Format(LangManager.GetTranslation("comprar_carta"), Definitions.PRECIO_COMPRA_CARTAS);
        Action onConfirm = delegate ()
        {
            if(cardManager == null)
            {
                cardManager = GameObject.FindObjectOfType<CardManager>();
            }
            ICard drawnCard = cardManager.DrawCardFromDeck();
            stateManager.CurrentPlayer().AddCard(drawnCard);
            stateManager.CurrentPlayer().Money -= Definitions.PRECIO_COMPRA_CARTAS;

            LoadPlayerCards();
        };
        GameObject.FindObjectOfType<ConfirmDialog>().ShowDialog(dialogText, onConfirm);
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
            newCardButton.GetComponentInChildren<Button>().GetComponent<Image>().sprite = cardImage;
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
    public GameObject BuyingTip;
    public GameObject SellingTip;
}
