using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSubcriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Subcribe(
            EventId.MOUSE_DOWN,
            delegate (object data) { NotifyMouseClick(data); }
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NotifyMouseClick(object data)
    {
        UnityEngine.Debug.Log("CATCH EVENT MOUSE CLICK:" + ((Vector3)data).ToString());
    }
}
