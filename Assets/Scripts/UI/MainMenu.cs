using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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

    public void Quit()
    {
        Application.Quit();
    }

    //---------------------------------------------------------------------------------------------

    public void StartGame(int numberOfPlayers)
    {
        StateManager.NumberOfPlayers = numberOfPlayers;
        SceneManager.LoadScene(1);
    }

    //---------------------------------------------------------------------------------------------

    public void ShowNewGameButtons()
    {
        mainButtons.SetActive(!mainButtons.activeSelf);
        newGameButtons.SetActive(!newGameButtons.activeSelf);
    }

    //---------------------------------------------------------------------------------------------
    public GameObject mainButtons;
    public GameObject newGameButtons;
}
