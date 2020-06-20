using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour
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

    public void ShowDialog(string text, Action onConfirm, Action onReject = null)
    {
        canvas.interactable = true;
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;

        dialogText.text = text;
        OnConfirm = onConfirm;
        OnReject = onReject;
    }

    //---------------------------------------------------------------------------------------------

    public void Accept()
    {
        OnConfirm?.Invoke();

        canvas.interactable = false;
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
    }

    //---------------------------------------------------------------------------------------------

    public void Reject()
    {
        OnReject?.Invoke();

        canvas.interactable = false;
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public Text dialogText;
    public CanvasGroup canvas;
    static Action OnConfirm;
    static Action OnReject;
}
