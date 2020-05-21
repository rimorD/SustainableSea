using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BoardViewing : BaseGameState, IGameState
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static BoardViewing GetInstance()
    {
        if(StateInstance == null)
        {
            StateInstance = new BoardViewing();
        }
        return StateInstance;
    }

    //---------------------------------------------------------------------------------------------

    public override void TileOnClick(StateManager stateManager, CardManager cardManager, Tile targetTile)
    {
        CameraManager gameCamera = Camera.main.GetComponent<CameraManager>();
        if(gameCamera.currentMovement == null)
        {
            gameCamera.currentMovement =
            new Movement
            (
                targetTile.transform.Find("CameraPlaceholder").transform.position,
                Quaternion.Euler
                (
                    Camera.main.transform.rotation.eulerAngles.x,
                    targetTile.transform.rotation.eulerAngles.y - 180,
                    Camera.main.transform.rotation.eulerAngles.z
                )
            );
        }
    }

    //---------------------------------------------------------------------------------------------

    public override void CameraUpdate(CameraManager camera)
    {
        if (camera.currentMovement != null && Vector3.Distance(camera.gameObject.transform.position, camera.currentMovement.finalPosition) > Definitions.SMOOTH_DISTANCE)
        {
            camera.gameObject.transform.position = Vector3.SmoothDamp(camera.gameObject.transform.position, camera.currentMovement.finalPosition, ref camera.velocity, Definitions.SMOOTH_TIME);
        }
        else if (camera.currentMovement != null && camera.currentMovement.hasFinalRotation && Quaternion.Angle(camera.gameObject.transform.rotation, camera.currentMovement.finalRotation) > Definitions.MAX_DEGREES_DELTA)
        {
            camera.gameObject.transform.rotation = Quaternion.RotateTowards(camera.gameObject.transform.rotation, camera.currentMovement.finalRotation, Definitions.MAX_DEGREES_DELTA);
        }
        else
        {
            camera.currentMovement = null;
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public static BoardViewing StateInstance;
}
