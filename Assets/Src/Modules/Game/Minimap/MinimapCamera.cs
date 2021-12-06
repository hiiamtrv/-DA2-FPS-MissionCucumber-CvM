using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    GameObject _player;

    void Awake()
    {
        EventCenter.Subcribe(EventId.CREATE_PLAYER, (data) =>
        {
            this._player = GameVar.Ins.Player;
        });
    }

    void LateUpdate()
    {
        if (this._player != null)
        {
            float playerX = this._player.transform.position.x;
            float playerZ = this._player.transform.position.z;
            this.transform.position = new Vector3(playerX, 2, playerZ);

            float playerRotY = this._player.transform.rotation.eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(90, 0, -playerRotY);
        }
    }
}
