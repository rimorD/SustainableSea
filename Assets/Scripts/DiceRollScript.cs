using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollScript : MonoBehaviour
{

    // Methods ////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        d6 = gameObject;
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        speed = d6.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed < 0.1)
        {
            d6.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Debug.Log("Stopped");
        }
    }

    //---------------------------------------------------------------------------------------------

    public void Roll()
    {
        // Show die
        d6.GetComponent<Renderer>().enabled = true;
        // Throw die, apply movement, rotation...
        d6.GetComponent<Rigidbody>().useGravity = true;
        d6.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10,100), 5, Random.Range(10,100)), ForceMode.Impulse);
        d6.GetComponent<Rigidbody>().AddForce((new Vector3(Random.Range(10, 100), 5, Random.Range(10, 100)) * d6.GetComponent<Rigidbody>().mass));
        d6.transform.rotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));

        // Disable walls and collisions

        // Read die value
        while(speed > 0.5)
        { }
        // Reenable walls and collisions

        // Reset die
        d6.transform.position = new Vector3(4.5f, 5, 5.5f);
        d6.transform.rotation = Quaternion.Euler(0,0,0);
        d6.GetComponent<Renderer>().enabled = false;
        d6.GetComponent<Rigidbody>().useGravity = false;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    GameObject d6;
    float speed;

}
