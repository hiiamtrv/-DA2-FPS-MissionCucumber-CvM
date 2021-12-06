using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDot : MonoBehaviour
{
    [SerializeField] GameObject _sprite;

    GameObject _follower;
    public GameObject Follower { get => _follower; set => _follower = value; }

    // Update is called once per frame
    void Update()
    {
        if (this._follower != null)
        {
            float playerX = this._follower.transform.position.x;
            float playerZ = this._follower.transform.position.z;
            this.transform.position = new Vector3(playerX, 1, playerZ);
        }
    }

    public void SetVisible(bool isVisible)
    {
        this._sprite.GetComponent<SpriteRenderer>().enabled = isVisible;
    }
}
