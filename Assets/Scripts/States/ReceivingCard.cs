using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReceivingCard : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static ReceivingCard GetInstance()
    {
        if(StateInstance == null)
        {
            StateInstance = new ReceivingCard();
        }
        return StateInstance;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static ReceivingCard StateInstance;
}
