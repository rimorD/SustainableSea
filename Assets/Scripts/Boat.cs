using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        diceRoller = GameObject.FindObjectOfType<DiceRoller>();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {

    }

    //---------------------------------------------------------------------------------------------

    void OnMouseUp()
    {
        // TODO: Check if UI element is in the way
        Debug.Log("Click");

        // TODO: Check if is our turn

        // Check if weve rolled the dice
        if (!diceRoller.IsDoneRolling())
            return;
        
        // Move
        int tilesToMove = diceRoller.LastRollResult();
        for(int i = 0; i < tilesToMove; i++)
        {
            currentTile = currentTile.GetNextTile();
            if (currentTile.IsCorner())
            {
                // Rotate
                this.transform.Rotate(0, 90, 0);
            }

            if (currentTile.IsInitialTile())
            {
                // Do whatever, get money, etc...
            }
        }

        // Teleport to final position
        this.transform.position = currentTile.transform.position;

        // End turn
        //The die is ready to roll again
        diceRoller.SetDoneRolling(false);
    }

    // Attributes /////////////////////////////////////////////////////////////////////////////////

    public void SetCurrentTile (Tile tile)
    {
        currentTile = tile;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    DiceRoller diceRoller;
    Tile currentTile;
}
