using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingForAction : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static WaitingForAction GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new WaitingForAction();
        }
        return StateInstance;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForAction StateInstance;
}
