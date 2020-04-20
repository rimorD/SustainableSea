using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingForAnimation : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static WaitingForAnimation GetInstance()
    {
        if (StateInstance == null)
        {
            StateInstance = new WaitingForAnimation();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void BoatUpdate(StateManager stateManager, Boat boat)
    {
        if (boat.isAnimating)
        {
            if (boat.currentMovement != null && Vector3.Distance(boat.gameObject.transform.position, boat.currentMovement.finalPosition) > Definitions.SMOOTH_DISTANCE)
            {
                boat.gameObject.transform.position = Vector3.SmoothDamp(boat.gameObject.transform.position, boat.currentMovement.finalPosition, ref boat.velocity, Definitions.SMOOTH_TIME);
            }
            else if (boat.currentMovement != null && boat.currentMovement.hasFinalRotation && Quaternion.Angle(boat.gameObject.transform.rotation, boat.currentMovement.finalRotation) > Definitions.MAX_DEGREES_DELTA)
            {
                boat.gameObject.transform.rotation = Quaternion.RotateTowards(boat.gameObject.transform.rotation, boat.currentMovement.finalRotation, Definitions.MAX_DEGREES_DELTA);
            }
            else
            {
                if (boat.pendingMovements.Count > 0)
                    boat.currentMovement = boat.pendingMovements.Dequeue();
                else
                {
                    boat.currentMovement = null;
                    boat.isAnimating = false;
                    stateManager.NewTurn();
                }
            }
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static WaitingForAnimation StateInstance;
}
