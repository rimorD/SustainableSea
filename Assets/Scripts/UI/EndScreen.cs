using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public void ConfigureView(string playerName, Color playerColor)
    {
        this.background.color = playerColor;
        this.playerName.text = playerName;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Image background;
    public Text playerName;
}
