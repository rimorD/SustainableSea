using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LangManager
{

    // Methods ////////////////////////////////////////////////////////////////////////////////////

    public static void InitializeLangManager()
    {
        currentLang = Definitions.DEFAULT_LANGUAGE;
        LoadTranslationFromFile();
    }

    public static void ChangeLanguage(string newLang)
    {
        if (Definitions.ACCEPTED_LANGUAGES.Contains(newLang))
            currentLang = newLang;
        else
            currentLang = Definitions.DEFAULT_LANGUAGE;
        
        LoadTranslationFromFile();
    }

    //---------------------------------------------------------------------------------------------

    public static string GetTranslation(string key)
    {
        if(translations == null)
        {
            InitializeLangManager();
        }
        if (!translations.ContainsKey(key))
        {
            return key;
        }
        return translations[key];
    }

    // Private Methods ////////////////////////////////////////////////////////////////////////////

    private static void LoadTranslationFromFile()
    {
        translations = new Dictionary<string, string>();
        string resourcesFileName = string.Format("{0}.{1}", Definitions.TRANSLATIONS_FILENAME, currentLang.ToString());
        TextAsset textAsset = (TextAsset)Resources.Load<TextAsset>(resourcesFileName);
        string[] lines = textAsset.text.Split('\n');

        foreach(string line in lines)
        { 
            string[] values = line.Split('=');
            translations[values[0]] = values[1];
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////

    public static Dictionary<string, string> translations;
    public static string currentLang;
}
