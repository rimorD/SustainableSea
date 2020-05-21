using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        stateManager.CurrentState.CameraUpdate(this);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public StateManager stateManager;

    public Movement currentMovement;
    public Vector3 velocity;
}
