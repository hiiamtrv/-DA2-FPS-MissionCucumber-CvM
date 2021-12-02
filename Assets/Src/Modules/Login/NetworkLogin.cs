using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkLogin : BaseNetwork
{
    static NetworkLogin _ins;
    public static NetworkLogin Ins => _ins;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _ins = this;
    }

    public void Login(string userName)
    {
        Gm.PlayerProfile.SetUsername(userName);
        string nickName = Gm.PlayerProfile.GetUsername();
        PhotonNetwork.NickName = nickName;
        this.LogInfo();

        if (PhotonNetwork.IsConnectedAndReady)
        {
            SceneManager.LoadScene(SceneId.LOBBY);
        }
    }
}