using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardScript : MonoBehaviour
{
    
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        GameObject board = gameObject;
        lineLength = boardLength / 4;

        this.InitializeTileStacks();

        for (int i = 0; i < lineLength; i++)
        {
            // Lateral izquierdo
            Vector3 position = new Vector3(0, 0, i+1);
            GameObject tile = Instantiate(this.GetMediumTile(), position, Quaternion.AngleAxis(270, new Vector3(0, 1)));
            tile.transform.parent = board.transform;

            // Lateral derecho
            position = new Vector3(lineLength - 1, 0, i+1);
            tile = Instantiate(this.GetMediumTile(), position, Quaternion.AngleAxis(90, new Vector3(0, 1)));
            tile.transform.parent = board.transform;

            // Parte inferior
            position = new Vector3(i, 0, 0);
            tile = Instantiate(this.GetCoastTile(), position, Quaternion.AngleAxis(180, new Vector3(0,1)));
            tile.transform.parent = board.transform;

            // Parte superior
            position = new Vector3(i, 0, lineLength + 1);
            tile = Instantiate(this.GetInteriorTile(), position, Quaternion.identity);
            tile.transform.parent = board.transform;
        }
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetCoastTile()
    {
        return coast_tiles.Pop();
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetInteriorTile()
    {
        return interior_tiles.Pop();
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetMediumTile()
    {
        return medium_tiles.Pop();
    }

    //---------------------------------------------------------------------------------------------

    private void InitializeTileStacks()
    {
        medium_tiles = new Stack<GameObject>(); 
        interior_tiles = new Stack<GameObject>(); 
        coast_tiles = new Stack<GameObject>();
        GameObject[] tile_prefabs = Resources.LoadAll<GameObject>("Prefabs");

        string[] lines = System.Text.RegularExpressions.Regex.Split(tileCountFile.text.Trim(), "[\n|\r]+");
        foreach(string line in lines)
        {
            string[] values = line.Split(';');
            GameObject tile = tile_prefabs.First(prefab => prefab.name.Equals(values[0]));

            int numberOfCopies = (int)(double.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture) * lineLength);
            for (int i=0; i < numberOfCopies; i++)
            {
                switch (values[0].Substring(0, 3))
                {
                    case "Cos":
                        coast_tiles.Push(tile);
                        break;
                    case "Int":
                        interior_tiles.Push(tile);
                        break;
                    case "Med":
                        medium_tiles.Push(tile);
                        break;
                }
            }
        }

        /*medium_tiles = medium_tiles.OrderBy(stack => UnityEngine.Random.value);
        interior_tiles = interior_tiles.OrderBy(stack => UnityEngine.Random.value);
        coast_tiles = coast_tiles.OrderBy(stack => UnityEngine.Random.value);*/

    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    public int boardLength;
    private int lineLength;

    private Stack<GameObject> medium_tiles;
    private Stack<GameObject> interior_tiles;
    private Stack<GameObject> coast_tiles;

    public TextAsset tileCountFile;


}
