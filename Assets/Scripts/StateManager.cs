using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        Players = new List<Player>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {

    }

    //---------------------------------------------------------------------------------------------

    public void NewTurn()
    {
        CurrentPhase = TurnPhase.WAITING_FOR_ACTION;
        CurrentPlayerId = CurrentPlayerId < NumberOfPlayers - 1 ? CurrentPlayerId + 1 : 0;

        if (CurrentPlayer().LostTurn)
        {
            CurrentPlayer().LostTurn = false;
            NewTurn();
        }

        turnView.SetActive(true);
    }

    //---------------------------------------------------------------------------------------------

    public Player CurrentPlayer()
    {
        return Players[CurrentPlayerId];
    }

    //---------------------------------------------------------------------------------------------

    public void PlayingCard()
    {
        CurrentPhase = TurnPhase.CARD_PLAYING;
        ShowCardsView(false);
    }

    //---------------------------------------------------------------------------------------------

    public void DonePlayingCard()
    {
        CurrentPhase = TurnPhase.WAITING_FOR_ACTION;
        ShowTurnView(true);
    }

    //---------------------------------------------------------------------------------------------

    public void DiscardingCard()
    {
        CurrentPhase = TurnPhase.CARD_DISCARDING;
        ShowCardsView(true);
        cardsView.GetComponent<CardViewScript>().ShowDiscardingControls(true);
    }

    //---------------------------------------------------------------------------------------------

    public void DoneDiscardingCard()
    {
        CurrentPhase = TurnPhase.WAITING_FOR_ANIMATION;
        cardsView.GetComponent<CardViewScript>().ShowDiscardingControls(false);
        ShowCardsView(false);
    }

    //---------------------------------------------------------------------------------------------

    public void OpenCardsMenu(bool show)
    {
        cardsView.GetComponent<CardViewScript>().ShowCardsView(show);
        ShowTurnView(!show);

        CurrentPhase = (show) ? TurnPhase.CARD_VIEWING : TurnPhase.WAITING_FOR_ACTION;
    }

    //---------------------------------------------------------------------------------------------

    public void ShowCardsView(bool show)
    {
        cardsView.GetComponent<CardViewScript>().ShowCardsView(show);
    }

    //---------------------------------------------------------------------------------------------

    public void ShowTurnView(bool show)
    {
        turnView.SetActive(show);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public enum TurnPhase 
    {
        WAITING_FOR_ROLL, 
        WAITING_FOR_ACTION, 
        WAITING_FOR_CLICK, 
        WAITING_FOR_ANIMATION,
        CARD_VIEWING,
        CARD_SELLING,
        CARD_PLAYING,
        CARD_DISCARDING
    }
    public TurnPhase CurrentPhase = TurnPhase.WAITING_FOR_ACTION;
    public int LastRollResult;

    public Tile InitialTile;

    // Player stuff
    public int CurrentPlayerId = 0;
    public static int NumberOfPlayers;
    public List<Player> Players;
    public static Color[] PlayerColors = { Color.red, Color.blue, Color.green, Color.yellow };

    // UI layers
    public GameObject turnView;
    public GameObject cardsView;
}
