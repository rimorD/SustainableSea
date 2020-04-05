using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        cardManager = GameObject.FindObjectOfType<CardManager>();
        Players = new List<Player>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {

    }

    //---------------------------------------------------------------------------------------------

    public void NewTurn()
    {
        // Realmente sería WAITING_FOR_ACTION, pero por ahora solamente tiramos dados
        CurrentPhase = TurnPhase.WAITING_FOR_ROLL;
        CurrentPlayerId = CurrentPlayerId < NumberOfPlayers - 1 ? CurrentPlayerId + 1 : 0;
        turnView.SetActive(true);
    }

    //---------------------------------------------------------------------------------------------

    public Player CurrentPlayer()
    {
        return Players[CurrentPlayerId];
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public enum TurnPhase { WAITING_FOR_ROLL, WAITING_FOR_ACTION, WAITING_FOR_CLICK, WAITING_FOR_ANIMATION }
    public TurnPhase CurrentPhase = TurnPhase.WAITING_FOR_ACTION;
    public int LastRollResult;

    // Player stuff
    public int CurrentPlayerId = 0;
    public static int NumberOfPlayers = 4;
    public List<Player> Players;
    public static Color[] PlayerColors = { Color.red, Color.blue, Color.green, Color.yellow };

    // Cards
    private CardManager cardManager;

    // UI layers
    public GameObject turnView;
    public GameObject cardsView;
}
