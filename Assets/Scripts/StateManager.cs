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
        CurrentState = WaitingForAction.GetInstance();
        CurrentPlayerId = CurrentPlayerId < NumberOfPlayers - 1 ? CurrentPlayerId + 1 : 0;

        if (CurrentPlayer().LostTurn)
        {
            CurrentPlayer().LostTurn = false;
            NewTurn();
        }

        turnView.SetActive(true);
        boardView.SetActive(false);
    }

    //---------------------------------------------------------------------------------------------

    public Player CurrentPlayer()
    {
        return Players[CurrentPlayerId];
    }

    //---------------------------------------------------------------------------------------------

    public void PlayingCard()
    {
        CurrentState = CardPlaying.GetInstance();
        ShowCardsView(false);
        ShowBoardView(true);
    }

    //---------------------------------------------------------------------------------------------

    public void DonePlayingCard()
    {
        CurrentState = WaitingForAction.GetInstance();
        ShowTurnView(true);
        ShowBoardView(false);
    }

    //---------------------------------------------------------------------------------------------

    public void DiscardingCard()
    {
        CurrentState = CardDiscarding.GetInstance();
        ShowCardsView(true);
        cardsView.GetComponent<CardMenu>().ShowDiscardingControls(true);
    }

    //---------------------------------------------------------------------------------------------

    public void DoneDiscardingCard()
    {
        CurrentState = WaitingForAnimation.GetInstance();
        cardsView.GetComponent<CardMenu>().ShowDiscardingControls(false);
        ShowCardsView(false);
    }

    //---------------------------------------------------------------------------------------------

    public void OpenCardsMenu(bool show)
    {
        cardsView.GetComponent<CardMenu>().ShowCardsView(show);
        ShowTurnView(!show);

        if (show)
        {
            CurrentState = CardViewing.GetInstance();
        }
        else
        {
            CurrentState = WaitingForAction.GetInstance();
        }
    }

    //---------------------------------------------------------------------------------------------

    public void ViewBoard(bool show)
    {
        ShowBoardView(show);
        ShowTurnView(!show);

        if (show)
        {
            CurrentState = BoardViewing.GetInstance();
        }
        else
        {
            CurrentState = WaitingForAction.GetInstance();
        }
    }

    //---------------------------------------------------------------------------------------------

    public void ShowCardsView(bool show)
    {
        cardsView.GetComponent<CardMenu>().ShowCardsView(show);
    }

    //---------------------------------------------------------------------------------------------

    public void ShowTurnView(bool show)
    {
        turnView.SetActive(show);
    }

    //---------------------------------------------------------------------------------------------

    public void ShowBoardView(bool show, bool withGoBack = true)
    {
        boardView.SetActive(show);
        boardView.GetComponent<BoardView>().ConfigureUI(withGoBack);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public IGameState CurrentState = WaitingForAction.GetInstance();
    public int LastRollResult;

    public Tile InitialTile;
    public FurtiveBoat Furtives;

    // Player stuff
    public int CurrentPlayerId = 0;
    public static int NumberOfPlayers;
    public List<Player> Players;
    public static Color[] PlayerColors = { Color.red, Color.blue, Color.green, Color.yellow };

    // UI layers
    public GameObject turnView;
    public GameObject cardsView;
    public GameObject boardView;
}
