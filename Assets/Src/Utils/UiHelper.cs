using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiHelper
{
    public Dictionary<string, GameObject> ui = new Dictionary<string, GameObject>();

    HashSet<string> duplicated = new HashSet<string>();

    public UiHelper(GameObject canvas)
    {
        this.SearchAt(canvas);
        // foreach (GameObject i in ui.Values) {
        //     Debug.Log(i.ToString());
        // }
    }

    void SearchAt(GameObject gameObject)
    {
        string name = gameObject.name;
        if (!duplicated.Contains(name))
        {
            if (ui.ContainsKey(name))
            {
                duplicated.Add(name);
                ui.Remove(name);
            }
            else
            {
                ui.Add(name, gameObject);
            }
        }

        foreach (Transform child in gameObject.transform)
        {
            this.SearchAt(child.gameObject);
        }
    }
}