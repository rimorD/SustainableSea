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
        this.lineLength = boardLength / 4;

        InitializeTileLists();

        CreateTiles();

        CreateBoardBackground();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetCoastTile()
    {
        GameObject result = coast_tiles[0];
        coast_tiles.RemoveAt(0);
        return result;
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetInteriorTile()
    {
        GameObject result = interior_tiles[0];
        interior_tiles.RemoveAt(0);
        return result;
    }

    //---------------------------------------------------------------------------------------------

    private GameObject GetMediumTile()
    {
        GameObject result = medium_tiles[0];
        medium_tiles.RemoveAt(0);
        return result;
    }

    //---------------------------------------------------------------------------------------------

    private void InitializeTileLists()
    {
        medium_tiles = new List<GameObject>(); 
        interior_tiles = new List<GameObject>(); 
        coast_tiles = new List<GameObject>();
        GameObject[] tile_prefabs = Resources.LoadAll<GameObject>("Prefabs/Tiles");

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
                        coast_tiles.Add(tile);
                        break;
                    case "Int":
                        interior_tiles.Add(tile);
                        break;
                    case "Med":
                        medium_tiles.Add(tile);
                        break;
                }
            }
        }

        ShuffleTileLists();

    }

    //---------------------------------------------------------------------------------------------

    private void ShuffleTileLists()
    {
        System.Random rng = new System.Random();

        // Hay el doble de casillas medio que costa/interior, por lo que está separado
        int n = medium_tiles.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = medium_tiles[k];
            medium_tiles[k] = medium_tiles[n];
            medium_tiles[n] = value;
        }

        // Hay el mismo número de costa e interior, por lo que pueden ir juntos
        n = coast_tiles.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = coast_tiles[k];
            coast_tiles[k] = coast_tiles[n];
            coast_tiles[n] = value;

            k = rng.Next(n + 1);
            value = interior_tiles[k];
            interior_tiles[k] = interior_tiles[n];
            interior_tiles[n] = value;
        }
    }

    //---------------------------------------------------------------------------------------------

    private void CreateTiles() 
    {
        for (int i = 0; i < lineLength; i++)
        {
            // Lateral izquierdo
            Vector3 position = new Vector3(0, 0, i + 1);
            GameObject tile = Instantiate(GetMediumTile(), position, Quaternion.AngleAxis(270, new Vector3(0, 1)));
            tile.transform.parent = gameObject.transform;

            // Lateral derecho
            position = new Vector3(lineLength - 1, 0, i + 1);
            tile = Instantiate(GetMediumTile(), position, Quaternion.AngleAxis(90, new Vector3(0, 1)));
            tile.transform.parent = gameObject.transform;

            // Parte inferior
            position = new Vector3(i, 0, 0);
            tile = Instantiate(GetCoastTile(), position, Quaternion.AngleAxis(180, new Vector3(0, 1)));
            tile.transform.parent = gameObject.transform;

            // Parte superior
            position = new Vector3(i, 0, lineLength + 1);
            tile = Instantiate(GetInteriorTile(), position, Quaternion.identity);
            tile.transform.parent = gameObject.transform;
        }
    }

    //---------------------------------------------------------------------------------------------

    private void CreateBoardBackground()
    {
        Vector3 position = new Vector3((lineLength / 2) - 0.5f, 0, (lineLength / 2) + 0.5f);
        GameObject background = Resources.Load<GameObject>("Prefabs/Tablero");
        GameObject boardBackground = Instantiate(background, position, Quaternion.AngleAxis(90, new Vector3(0, 1)));
        boardBackground.transform.parent = gameObject.transform;
        boardBackground.transform.localScale = new Vector3(lineLength, boardBackground.transform.localScale.y, lineLength - 2);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    public int boardLength;
    private int lineLength;

    protected List<GameObject> medium_tiles;
    protected List<GameObject> interior_tiles;
    protected List<GameObject> coast_tiles;

    public TextAsset tileCountFile;


}
