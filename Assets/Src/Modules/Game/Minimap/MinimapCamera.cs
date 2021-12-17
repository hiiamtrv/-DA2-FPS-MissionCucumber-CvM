using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    void LateUpdate()
    {
        GameObject player = CameraExtension.GetCurrentCamera().gameObject;
        if (player != null)
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            this.transform.position = new Vector3(playerX, 2, playerZ);

            float playerRotY = player.transform.rotation.eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(90, 0, -playerRotY);
        }
    }
}
