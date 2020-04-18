using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurtiveBoat : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        if (isAnimating)
        {
            if (currentMovement != null && currentMovement.hasFinalRotation && Quaternion.Angle(transform.rotation, currentMovement.finalRotation) > Definitions.MAX_DEGREES_DELTA)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, currentMovement.finalRotation, Definitions.MAX_DEGREES_DELTA);
            }
            else if (currentMovement != null && Vector3.Distance(transform.position, currentMovement.finalPosition) > Definitions.SMOOTH_DISTANCE)
            {
                this.transform.position = Vector3.SmoothDamp(transform.position, currentMovement.finalPosition, ref velocity, Definitions.SMOOTH_TIME);
            }
            else
            {
                currentMovement = null;
                isAnimating = false;
            }
        }
    }

    //---------------------------------------------------------------------------------------------

    public void Move(Tile targetTile)
    {
        if(CurrentTile != null)
        {
            CurrentTile.furtives = false;
        }
        CurrentTile = targetTile;
        CurrentTile.furtives = true;

        currentMovement = 
            new Movement
            (
                new Vector3(CurrentTile.transform.Find("FurtivesPlaceholder").position.x, 0, CurrentTile.transform.Find("FurtivesPlaceholder").position.z),
                Quaternion.LookRotation
                (
                    (CurrentTile.transform.position - transform.position).normalized
                )
            );
        isAnimating = true;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    Tile CurrentTile;

    // Animation stuff
    Movement currentMovement;
    bool isAnimating = false;
    Vector3 velocity;
}
