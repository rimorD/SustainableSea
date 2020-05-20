using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangObjectManager: MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        textComponent = gameObject.GetComponentInChildren<Text>();
        translationKey = textComponent.text;
        currentLang = LangManager.currentLang;
        textComponent.text = LangManager.GetTranslation(translationKey);
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        if(currentLang != LangManager.currentLang)
        {
            textComponent.text = LangManager.GetTranslation(translationKey);
            currentLang = LangManager.currentLang;
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    Text textComponent;
    string translationKey;
    string currentLang;
}
