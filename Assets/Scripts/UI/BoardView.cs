using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
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

    public void PlayerAvatarClick(int playerId)
    {
        StateManager stateManager = stateManagerGO.GetComponent<StateManager>();
        CardManager cardManager = cardManagerGO.GetComponent<CardManager>();

        stateManager.CurrentState.PlayerClick(stateManager, cardManager, playerId);
    }

    //---------------------------------------------------------------------------------------------

    public void ConfigureUI(bool showGoBack)
    {
        StateManager stateManager = stateManagerGO.GetComponent<StateManager>();

        Player1UI.SetActive(true);
        Text player1Name = Player1UI.transform.Find("PlayerName").GetComponent<Text>();
        player1Name.color = stateManager.Players[0].PlayerColor;
        player1Name.text = stateManager.Players[0].PlayerName;
        Player1UI.transform.Find("PlayerMoney").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("currency_abreviation"), stateManager.Players[0].Money);
        Player1UI.transform.Find("PlayerPGS").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("points_abreviation"), stateManager.Players[0].PGS);

        Player2UI.SetActive(true);
        Text player2Name = Player2UI.transform.Find("PlayerName").GetComponent<Text>();
        player2Name.color = stateManager.Players[1].PlayerColor;
        player2Name.text = stateManager.Players[1].PlayerName;
        Player2UI.transform.Find("PlayerMoney").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("currency_abreviation"), stateManager.Players[1].Money);
        Player2UI.transform.Find("PlayerPGS").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("points_abreviation"), stateManager.Players[1].PGS);

        if (StateManager.NumberOfPlayers > 2)
        {
            Player3UI.SetActive(true);
            Text player3Name = Player3UI.transform.Find("PlayerName").GetComponent<Text>();
            player3Name.color = stateManager.Players[2].PlayerColor;
            player3Name.text = stateManager.Players[2].PlayerName;
            Player3UI.transform.Find("PlayerMoney").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("currency_abreviation"), stateManager.Players[2].Money);
            Player3UI.transform.Find("PlayerPGS").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("points_abreviation"), stateManager.Players[2].PGS);

            if (StateManager.NumberOfPlayers > 3)
            {
                Player4UI.SetActive(true);
                Text player4Name = Player4UI.transform.Find("PlayerName").GetComponent<Text>();
                player4Name.color = stateManager.Players[3].PlayerColor;
                player4Name.text = stateManager.Players[3].PlayerName;
                Player4UI.transform.Find("PlayerMoney").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("currency_abreviation"), stateManager.Players[3].Money);
                Player4UI.transform.Find("PlayerPGS").GetComponent<Text>().text = string.Format(LangManager.GetTranslation("points_abreviation"), stateManager.Players[3].PGS);
            }
        }

        GoBackButton.SetActive(showGoBack);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public GameObject Player1UI;
    public GameObject Player2UI;
    public GameObject Player3UI;
    public GameObject Player4UI;

    public GameObject GoBackButton;

    public GameObject stateManagerGO;
    public GameObject cardManagerGO;
}
