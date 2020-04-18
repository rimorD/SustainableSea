﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTurnDisplay : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        panelBackground = GetComponent<Image>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        playerName.text = stateManager.CurrentPlayer().PlayerName;
        playerMoney.text = stateManager.CurrentPlayer().Money + " CTS";
        playerPGS.text = stateManager.CurrentPlayer().PGS + " PGS";
        panelBackground.color = new Color
        (
            stateManager.CurrentPlayer().PlayerColor.r,
            stateManager.CurrentPlayer().PlayerColor.g,
            stateManager.CurrentPlayer().PlayerColor.b,
            Definitions.NEWTURN_PANEL_BACKGROUND_OPACITY
        );
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    StateManager stateManager;

    public Text playerName;
    public Text playerMoney;
    public Text playerPGS;
    public Image playerAvatar;

    Image panelBackground;
}
