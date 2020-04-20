using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingForRoll : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static WaitingForRoll GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new WaitingForRoll();
        }
        return StateInstance;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForRoll StateInstance;
}
