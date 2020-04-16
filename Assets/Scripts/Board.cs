﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        this.lineLength = boardLength / 4;
        this.stateManager = GameObject.FindObjectOfType<StateManager>();

        InitializeTileLists();

        CreateTiles();

        CreateBoardBackground();

        CreateBoardLimitWalls();
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

        // Load resources, materials and tile basic prefab (no skin)
        Material[] tileMaterials = Resources.LoadAll<Material>("Materiales/TileMaterials");
        GameObject tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");

        // Read tile proportion file
        string[] lines = System.Text.RegularExpressions.Regex.Split(tileCountFile.text.Trim(), "[\n|\r]+");
        foreach(string line in lines)
        {
            string[] values = line.Split(';');

            int numberOfCopies = (int)(double.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture) * lineLength);
            for (int i=0; i < numberOfCopies; i++)
            {
                // Generate tiles
                GameObject tile = Instantiate(tilePrefab);
                tile.GetComponent<Tile>().resources = Int32.Parse(values[2]);
                tile.GetComponentInChildren<MeshRenderer>().material = tileMaterials.First(material => material.name.Equals(values[0]));

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
        GameObject initialTile = null;
        GameObject lastTile = null;
        for (int i = lineLength - 1; i >= 0; i--)
        {
            // Parte inferior
            GameObject coastTile = GetCoastTile();
            coastTile.transform.position = new Vector3(i, 0, 0);
            coastTile.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1));
            coastTile.transform.parent = gameObject.transform;

            // Si es la primera casilla, guardarla para asignarla como siguiente a la última 
            // y marcarla como casilla de salida
            if (i == lineLength - 1)
            {
                initialTile = coastTile;
                coastTile.GetComponent<Tile>().isInitialTile = true;
            }

            // Si no, enlazar las casillas
            else
            {
                lastTile.GetComponent<Tile>().nextTile = coastTile.GetComponent<Tile>();
                coastTile.GetComponent<Tile>().previousTile = lastTile.GetComponent<Tile>();
            }

            // Si es la primera o la última, es una esquina
            if (i == 0 || i == lineLength - 1)
            {
                coastTile.GetComponent<Tile>().isCorner = true;

                // Move furtives placeholder since its a corner
                // Only the last one since the first one doesnt have a previous one yet
                if (i == 0)
                {
                    coastTile.transform.Find("FurtivesPlaceholder").transform.position =
                        lastTile.transform.Find("FurtivesPlaceholder").transform.position;
                }
            }

            lastTile = coastTile;
        }

        for (int i = 0; i < lineLength; i++)
        {
            // Lateral izquierdo
            GameObject mediumTileLeft = GetMediumTile();
            mediumTileLeft.transform.position = new Vector3(0, 0, i + 1);
            mediumTileLeft.transform.rotation = Quaternion.AngleAxis(270, new Vector3(0, 1));
            mediumTileLeft.transform.parent = gameObject.transform;

            // Enlazar las casillas
            lastTile.GetComponent<Tile>().nextTile = mediumTileLeft.GetComponent<Tile>();
            mediumTileLeft.GetComponent<Tile>().previousTile = lastTile.GetComponent<Tile>();
            lastTile = mediumTileLeft;
        }

        for (int i = 0; i < lineLength; i++)
        {
            // Parte superior
            GameObject deepTile = GetInteriorTile();
            deepTile.transform.position = new Vector3(i, 0, lineLength + 1);
            deepTile.transform.rotation = Quaternion.identity;
            deepTile.transform.parent = gameObject.transform;

            // Enlazar las casillas
            lastTile.GetComponent<Tile>().nextTile = deepTile.GetComponent<Tile>();
            deepTile.GetComponent<Tile>().previousTile = lastTile.GetComponent<Tile>();

            // Si es la primera o la última, es una esquina
            if (i == 0 || i == lineLength - 1)
            {
                deepTile.GetComponent<Tile>().isCorner = true;

                // Move furtives placeholder since its a corner
                deepTile.transform.Find("FurtivesPlaceholder").transform.position =
                    lastTile.transform.Find("FurtivesPlaceholder").transform.position;
            }

            lastTile = deepTile;
        }

        for (int i = lineLength - 1; i >= 0; i--)
        {
            // Lateral derecho
            GameObject mediumTileRight = GetMediumTile();
            mediumTileRight.transform.position = new Vector3(lineLength - 1, 0, i + 1);
            mediumTileRight.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 1));
            mediumTileRight.transform.parent = gameObject.transform;

            // Enlazar las casillas
            lastTile.GetComponent<Tile>().nextTile = mediumTileRight.GetComponent<Tile>();
            mediumTileRight.GetComponent<Tile>().previousTile = lastTile.GetComponent<Tile>();
            lastTile = mediumTileRight;

            // Si es la última, asignarle la casilla inicial como siguiente
            if (i == 0)
            {
                mediumTileRight.GetComponent<Tile>().nextTile = initialTile.GetComponent<Tile>();
                initialTile.GetComponent<Tile>().previousTile = mediumTileRight.GetComponent<Tile>();

                // Move furtives placeholder since its a corner
                initialTile.transform.Find("FurtivesPlaceholder").transform.position =
                    lastTile.transform.Find("FurtivesPlaceholder").transform.position;
            }
        }

        // Creación de jugadores

        GameObject PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        GameObject PlayerContainer = GameObject.Find("Players");

        GameObject BoatPrefab = Resources.Load<GameObject>("Prefabs/Barco");
        GameObject BoatContainer = GameObject.Find("Barcos-Jugador");

        for(int i = 0; i < StateManager.NumberOfPlayers; i++)
        {
            GameObject newPlayerGameObject = GameObject.Instantiate(PlayerPrefab, PlayerContainer.transform);
            Player newPlayer = newPlayerGameObject.GetComponent<Player>();
            newPlayer.PlayerName = "Jugador " + (i+1);
            newPlayer.PlayerId = i;
            newPlayer.PlayerColor = StateManager.PlayerColors[i];

            // Crear un barco para el jugador
            GameObject boatGameObject = GameObject.Instantiate(BoatPrefab, initialTile.transform.position, Quaternion.identity, BoatContainer.transform);
            Boat newBoat = boatGameObject.GetComponent<Boat>();
            newBoat.SetCurrentTile(initialTile.GetComponent<Tile>());
            newPlayer.AddBoat(newBoat);

            stateManager.Players.Add(newPlayer);
        }

        // We need to know which one is the initialTile later when we buy boats
        stateManager.InitialTile = initialTile.GetComponent<Tile>();
    }

    //---------------------------------------------------------------------------------------------

    private void CreateBoardBackground()
    {
        Vector3 position = new Vector3((lineLength / 2) - 0.5f, 0, (lineLength / 2) + 0.5f);
        GameObject background = Resources.Load<GameObject>("Prefabs/TableroBackground");
        GameObject boardBackground = Instantiate(background, position, Quaternion.AngleAxis(90, new Vector3(0, 1)));
        boardBackground.transform.parent = gameObject.transform;
        boardBackground.transform.localScale = new Vector3(lineLength, boardBackground.transform.localScale.y, lineLength - 2);
    }

    // Create walls around the board so that our dice wont fall over.

    private void CreateBoardLimitWalls()
    {
        GameObject borderWall = Resources.Load<GameObject>("Prefabs/BorderWall");

        Vector3 position = new Vector3(0.5f, this.lineLength / 2, this.lineLength / 2 + 0.5f);
        wallInnerTop = Instantiate(borderWall, position, Quaternion.identity, gameObject.transform);
        wallInnerTop.name = "wallInnerTop";
        wallInnerTop.transform.localScale = new Vector3(0, this.lineLength, this.lineLength);

        position = new Vector3(this.lineLength / 2 - 0.5f, this.lineLength / 2, this.lineLength + 0.5f);
        wallInnerRight = Instantiate(borderWall, position, Quaternion.Euler(0, 90, 0), gameObject.transform);
        wallInnerRight.name = "wallInnerRight";
        wallInnerRight.transform.localScale = new Vector3(0, this.lineLength, this.lineLength - 2);

        position = new Vector3(this.lineLength - 1.5f, this.lineLength / 2, this.lineLength / 2 + 0.5f);
        wallInnerBottom = Instantiate(borderWall, position, Quaternion.identity, gameObject.transform);
        wallInnerBottom.name = "wallInnerBottom";
        wallInnerBottom.transform.localScale = new Vector3(0, this.lineLength, this.lineLength);

        position = new Vector3(this.lineLength / 2 - 0.5f, this.lineLength / 2, 0.5f);
        wallInnerLeft = Instantiate(borderWall, position, Quaternion.Euler(0, 90, 0), gameObject.transform);
        wallInnerLeft.name = "wallInnerLeft";
        wallInnerLeft.transform.localScale = new Vector3(0, this.lineLength, this.lineLength - 2);
    }

    //---------------------------------------------------------------------------------------------

    public void SetWallColliders(bool enable)
    {
        wallInnerTop.GetComponent<Collider>().enabled = enable;
        wallInnerRight.GetComponent<Collider>().enabled = enable;
        wallInnerBottom.GetComponent<Collider>().enabled = enable;
        wallInnerLeft.GetComponent<Collider>().enabled = enable;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    public int boardLength;
    private int lineLength;

    protected List<GameObject> medium_tiles;
    protected List<GameObject> interior_tiles;
    protected List<GameObject> coast_tiles;

    GameObject wallInnerTop;
    GameObject wallInnerRight;
    GameObject wallInnerBottom;
    GameObject wallInnerLeft;

    StateManager stateManager;

    public TextAsset tileCountFile;
}
