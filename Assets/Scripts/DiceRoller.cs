using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{

    // Methods ////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        dieRenderer = gameObject.GetComponentInChildren<Renderer>();
        dieRigidBody = gameObject.GetComponentInChildren<Rigidbody>();

        board = FindObjectOfType<Board>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
            
    }

    //---------------------------------------------------------------------------------------------

    public void Roll()
    {
        StartCoroutine(DiceCoroutine());
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator DiceCoroutine()
    {
        ThrowDie();

        // Wait for the die to start moving before checking if its stopped
        yield return new WaitForSeconds(0.5f);

        // Wait until die stops

        while (dieRigidBody.velocity.magnitude > 0.1)
        {
            yield return new WaitForSeconds(4);
        }
        dieRigidBody.velocity = new Vector3(0, 0, 0);

        // Read die value
        GetDieValue();

        // Reset die
        ResetDie();
    }

    //---------------------------------------------------------------------------------------------

    private void ThrowDie()
    {
        // Show die
        dieRenderer.enabled = true;
        // Enable wall colliders
        board.SetWallColliders(true);
        // Throw die, apply movement, rotation...
        dieRigidBody.useGravity = true;
        dieRigidBody.AddTorque(new Vector3(Random.Range(-180, 10), 5, Random.Range(-180, 180)), ForceMode.Impulse);
        dieRigidBody.AddForce((new Vector3(Random.Range(-180, 10), 5, Random.Range(-180, 180)) * dieRigidBody.mass));
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    //---------------------------------------------------------------------------------------------

    private void ResetDie()
    {
        transform.position = new Vector3(4.5f, 5, 5.5f);
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        dieRenderer.enabled = false;
        dieRigidBody.useGravity = false;

        board.SetWallColliders(false);
    }

    //---------------------------------------------------------------------------------------------

    void GetDieValue()
    {
        double dotProduct;
        if ((dotProduct = Vector3.Dot(transform.forward, Vector3.up)) > 0.6f)
        {
            lastRollResult = 1;
        }
        else if ((dotProduct = Vector3.Dot(-transform.forward, Vector3.up)) > 0.6f)
        {
            lastRollResult = 6;
        }
        else if ((dotProduct = Vector3.Dot(transform.up, Vector3.up)) > 0.6f)
        {
            lastRollResult = 2;
        }
        else if ((dotProduct = Vector3.Dot(-transform.up, Vector3.up)) > 0.6f)
        {
            lastRollResult = 5;
        }
        else if ((dotProduct = Vector3.Dot(transform.right, Vector3.up)) > 0.6f)
        {
            lastRollResult = 3;
        }
        else
        {
            lastRollResult = 4;
        }

        Debug.Log(dotProduct);
        Debug.Log(lastRollResult);

        SetDoneRolling(true);
    }

    // Attributes /////////////////////////////////////////////////////////////////////////////////

    public int LastRollResult()
    {
        return lastRollResult;
    }

    //---------------------------------------------------------------------------------------------

    public bool IsDoneRolling()
    {
        return isDoneRolling;
    }

    //---------------------------------------------------------------------------------------------

    public void SetDoneRolling(bool value)
    {
        isDoneRolling = value;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    Renderer dieRenderer;
    Rigidbody dieRigidBody;
    int lastRollResult = 0;
    bool isDoneRolling = false;

    Board board;
}
