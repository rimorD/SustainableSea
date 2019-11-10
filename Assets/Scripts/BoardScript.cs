using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        GameObject board = gameObject;
        for (int i = 0; i <= boardLength; i++)
        {
            // Lateral izquierdo
            Vector3 pos = new Vector3(0, 0, i);
            GameObject go = Instantiate(this.GetMediumTile(), pos, Quaternion.identity);
            go.transform.parent = board.transform;

            // Lateral derecho
            pos = new Vector3(boardLength, 0, i);
            go = Instantiate(this.GetMediumTile(), pos, Quaternion.identity);
            go.transform.parent = board.transform;

            if (i != boardLength && i != 0)
            {
                // Parte inferior
                pos = new Vector3(i, 0, 0);
                go = Instantiate(this.GetCoastTile(), pos, Quaternion.identity);
                go.transform.parent = board.transform;

                // Parte superior
                pos = new Vector3(i, 0, boardLength);
                go = Instantiate(this.GetInteriorTile(), pos, Quaternion.identity);
                go.transform.parent = board.transform;
            }
        }
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetCoastTile()
    {
        //TODO
        return prefab;
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetInteriorTile()
    {
        //TODO
        return prefab;
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetMediumTile()
    {
        //TODO
        return prefab;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    public float boardLength;

    // Prefabs ////////////////////////////////////////////////////////////////////////////////////
    public GameObject prefab;

}
