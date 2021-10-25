using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkLogin : BaseNetwork
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        EventCenter.Subcribe(EventId.LOGIN_SUCCESS, data =>
        {
            string nickName = Gm.PlayerProfile.GetUsername();
            PhotonNetwork.NickName = nickName;
            this.LogInfo();
        });
    }
}