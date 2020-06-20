using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatShop : MonoBehaviour
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

    public void ShowDialog(string text)
    {
        canvas.interactable = true;
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;

        TrailButton.interactable = stateManager.CurrentPlayer().Money > Definitions.PRECIO_COMPRA_ARRASTRE;
        ArtisanalButton.interactable = stateManager.CurrentPlayer().Money > Definitions.PRECIO_COMPRA_ARTESANAL;

        dialogText.text = text;
    }

    //---------------------------------------------------------------------------------------------

    public void HideDialog()
    {
        canvas.interactable = false;
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
    }

    //---------------------------------------------------------------------------------------------

    public void BuyArtisanalBoat()
    {
        HideDialog();

        string dialogText = string.Format(LangManager.GetTranslation("comprar_barco_artesanal_confirm"), Definitions.PRECIO_COMPRA_ARTESANAL);
        Action onConfirm = delegate ()
        {
            GameObject newBoatGameObject = GameObject.Instantiate(ArtisanalBoatPrefab, stateManager.InitialTile.transform.position, Quaternion.identity, BoatContainer.transform);
            Boat newBoat = newBoatGameObject.GetComponent<Boat>();
            stateManager.CurrentPlayer().AddBoat(newBoat);
            stateManager.CurrentPlayer().Money -= Definitions.PRECIO_COMPRA_ARTESANAL;
            newBoat.SetCurrentTile(stateManager.InitialTile.GetComponent<Tile>());
            newBoat.ArriveAtTile();
            newBoat.currentTile.UpdateBoatPositions(newBoat);
            newBoatGameObject.transform.position = newBoat.GetPositionForCurrentTile();

            stateManager.currentState = WaitingForAnimation.GetInstance();
        };
        Action onReject = delegate ()
        {
            ShowDialog
            (
                String.Format
                (
                    LangManager.GetTranslation("boat_shop_text"),
                    Definitions.CANTIDAD_A_RECIBIR_SALIDA
                )
            );
        };
        GameObject.FindObjectOfType<ConfirmDialog>().ShowDialog(dialogText, onConfirm, onReject);
    }

    //---------------------------------------------------------------------------------------------

    public void BuyTrailBoat()
    {
        HideDialog();
        string dialogText = string.Format(LangManager.GetTranslation("comprar_barco_arrastre_confirm"), Definitions.PRECIO_COMPRA_ARRASTRE);
        Action onConfirm = delegate ()
        {
            GameObject newBoatGameObject = GameObject.Instantiate(TrailBoatPrefab, stateManager.InitialTile.transform.position, Quaternion.identity, BoatContainer.transform);
            Boat newBoat = newBoatGameObject.GetComponent<Boat>();
            stateManager.CurrentPlayer().AddBoat(newBoat);
            stateManager.CurrentPlayer().Money -= Definitions.PRECIO_COMPRA_ARRASTRE;
            newBoat.SetCurrentTile(stateManager.InitialTile.GetComponent<Tile>());
            newBoat.ArriveAtTile();
            newBoat.currentTile.UpdateBoatPositions(newBoat);
            newBoatGameObject.transform.position = newBoat.GetPositionForCurrentTile();

            stateManager.currentState = WaitingForAnimation.GetInstance();
        };
        Action onReject = delegate ()
        {
            ShowDialog
            (
                String.Format
                (
                    LangManager.GetTranslation("boat_shop_text"),
                    Definitions.CANTIDAD_A_RECIBIR_SALIDA
                )
            );
        };
        GameObject.FindObjectOfType<ConfirmDialog>().ShowDialog(dialogText, onConfirm, onReject);
    }

    //---------------------------------------------------------------------------------------------

    public void DoNothing()
    {
        HideDialog();
        stateManager.currentState = WaitingForAnimation.GetInstance();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public GameObject ArtisanalBoatPrefab;
    public GameObject BoatContainer;
    public GameObject TrailBoatPrefab;
    public StateManager stateManager;

    // UI items
    public Text dialogText;
    public CanvasGroup canvas;
    public Button ArtisanalButton;
    public Button TrailButton;
}
