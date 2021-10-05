using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMakeRoom : MonoBehaviour
{
    const string BTN_CREATE = "BtnCreate";
    const string BTN_JOIN = "BtnJoin";
    const string BTN_SOLO = "BtnSolo";
    const string TOOGLE_PUBLIC = "TogglePublic";
    const string TOOGLE_PRIVATE = "TogglePrivate";
    const string TXT_ROOM_CODE = "TxtRoomCode";

    Button btnCreate = null;
    Button btnJoin = null;
    Button btnSolo = null;
    Toggle tooglePublic = null;
    Toggle tooglePrivate = null;
    InputField txtRoomCode = null;

    RoomMode? currentRoomMode = null;

    // Start is called before the first frame update
    void Start()
    {
        this.btnCreate = GameObject.Find(BTN_CREATE).GetComponent<Button>();
        this.btnJoin = GameObject.Find(BTN_JOIN).GetComponent<Button>();
        this.btnSolo = GameObject.Find(BTN_SOLO).GetComponent<Button>();
        this.tooglePublic = GameObject.Find(TOOGLE_PUBLIC).GetComponent<Toggle>();
        this.tooglePrivate = GameObject.Find(TOOGLE_PRIVATE).GetComponent<Toggle>();
        this.txtRoomCode = GameObject.Find(TXT_ROOM_CODE).GetComponent<InputField>();

        this.btnCreate.onClick.AddListener(this.OnCreateRoom);
        this.btnJoin.onClick.AddListener(this.OnJoinRoom);
        this.btnSolo.onClick.AddListener(this.OnSolo);

        this.SetRoomMode(RoomMode.PUBLIC);

        this.tooglePublic.onValueChanged.AddListener(delegate { this.SetRoomMode(RoomMode.PUBLIC); });
        this.tooglePrivate.onValueChanged.AddListener(delegate { this.SetRoomMode(RoomMode.PRIVATE); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCreateRoom()
    {
        Debug.Log("Request create room " + this.currentRoomMode);
    }

    void OnJoinRoom()
    {
        string roomCode = this.txtRoomCode.text;
        if (true)
        {
            Debug.Log("Request join room: " + roomCode);
        }
    }

    void OnSolo()
    {
        Debug.Log("Request solo");
    }

    void SetRoomMode(RoomMode roomMode)
    {
        this.currentRoomMode = roomMode;
        Debug.Log("Set room mode: " + roomMode);
        this.tooglePublic.SetIsOnWithoutNotify(roomMode == RoomMode.PUBLIC ? true : false);
        this.tooglePrivate.SetIsOnWithoutNotify(roomMode == RoomMode.PRIVATE ? true : false);
    }
}

enum RoomMode
{
    PUBLIC,
    PRIVATE
}
