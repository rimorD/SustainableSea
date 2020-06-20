using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BoatShopping : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static BoatShopping GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new BoatShopping();
        }
        return StateInstance;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static BoatShopping StateInstance;
}
