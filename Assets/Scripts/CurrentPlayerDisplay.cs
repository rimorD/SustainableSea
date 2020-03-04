using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        displayText = this.GetComponent<Text>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        // TODO: Player name
        displayText.text = "Turno del jugador " + (stateManager.CurrentPlayerId + 1);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    StateManager stateManager;
    Text displayText;
}
