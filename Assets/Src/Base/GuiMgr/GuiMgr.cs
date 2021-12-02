using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMgr : MonoBehaviour
{
    const float MOVE_TIME = 0.5f;
    const float DELAY = 0;

    [SerializeField] GameObject[] guis;
    [SerializeField] GameObject startGui;

    Dictionary<string, GameObject> mapGui = new Dictionary<string, GameObject>();

    string curGuiName = "";

    //Implement when screen have "Goback"
    // List<string> guiStack = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gui in guis)
        {
            mapGui.Add(gui.name, gui);
        }

        this.ResetGuis();
        this.ChangeGui(startGui.name);

        EventCenter.Subcribe(EventId.CHANGE_GUI, data => this.ChangeGui((string)data));
    }

    void ResetGuis()
    {
        foreach (GameObject gui in guis)
        {
            gui.SetActive(false);
        }
    }

    void ChangeGui(string newGuiName)
    {
        if (this.curGuiName != "")
        {
            if (curGuiName == newGuiName) return;

            GameObject oldGui = mapGui[curGuiName];
            this.MoveOutOfScreen(oldGui);
            oldGui.GetComponent<BaseGui>().OnExit();
        }

        GameObject newGui = mapGui[newGuiName];
        newGui.GetComponent<BaseGui>().OnEnter();

        this.MoveIntoScreen(newGui);
        curGuiName = newGuiName;
    }

    void MoveOutOfScreen(GameObject canvas)
    {
        // int screenHeight = Screen.height;
        if (canvas != null) canvas.SetActive(false);

    }

    void MoveIntoScreen(GameObject canvas)
    {
        // int screenHeight = Screen.height;
        // Vector3 startPoint = new Vector3(0, screenHeight * 1.5f);
        // canvas.transform.position = startPoint;
        if (canvas != null) canvas.SetActive(true);
    }

    public static GameObject GetGui(string guiName)
    {
        GuiMgr mgr = GameObject.FindObjectOfType<GuiMgr>();
        return mgr.mapGui[guiName];
    }
}
