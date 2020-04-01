using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public Movement(Vector3 position)
    {
        finalPosition = position;
    }

    public Movement(Vector3 position, Quaternion rotation)
    {
        finalPosition = position;
        finalRotation = rotation;
        hasFinalRotation = true;
    }

    public Vector3 finalPosition;
    public Quaternion finalRotation;
    public bool hasFinalRotation = false;
}
