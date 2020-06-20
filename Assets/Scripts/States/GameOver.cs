using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static GameOver GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new GameOver();
        }
        return StateInstance;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static GameOver StateInstance;
}
