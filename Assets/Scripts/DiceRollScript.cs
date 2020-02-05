using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
            
    }

    //---------------------------------------------------------------------------------------------

    public void Roll()
    {
        StartCoroutine(DiceCoroutine());
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator DiceCoroutine()
    {
        // Show die
        d6.GetComponent<Renderer>().enabled = true;
        // Throw die, apply movement, rotation...
        d6.GetComponent<Rigidbody>().useGravity = true;
        d6.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-180, 10), 5, Random.Range(-180, 180)), ForceMode.Impulse);
        d6.GetComponent<Rigidbody>().AddForce((new Vector3(Random.Range(-180, 10), 5, Random.Range(-180, 180)) * d6.GetComponent<Rigidbody>().mass));
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        yield return new WaitForSeconds(0.5f);

        // Wait until die stops

        while (d6.GetComponent<Rigidbody>().velocity.magnitude > 0.1)
        {
            yield return new WaitForSeconds(4);
        }
        d6.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        // Read die value
        GetDieValue();

        // Reset die
        transform.position = new Vector3(4.5f, 5, 5.5f);
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        d6.GetComponent<Renderer>().enabled = false;
        d6.GetComponent<Rigidbody>().useGravity = false;
    }

    //---------------------------------------------------------------------------------------------

    void GetDieValue()
    {
        if (Vector3.Dot(transform.forward, Vector3.up) > 0.6f)
            Debug.Log("1");
        if (Vector3.Dot(-transform.forward, Vector3.up) > 0.6f)
            Debug.Log("6");
        if (Vector3.Dot(transform.up, Vector3.up) > 0.6f)
            Debug.Log("2");
        if (Vector3.Dot(-transform.up, Vector3.up) > 0.6f)
            Debug.Log("5");
        if (Vector3.Dot(transform.right, Vector3.up) > 0.6f)
            Debug.Log("3");
        if (Vector3.Dot(-transform.right, Vector3.up) > 0.6f)
            Debug.Log("4");

    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    GameObject d6;
}
