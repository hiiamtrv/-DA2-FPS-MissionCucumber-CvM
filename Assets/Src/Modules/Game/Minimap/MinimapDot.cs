using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDot : MonoBehaviour
{
    [SerializeField] GameObject _sprite;
    [SerializeField] bool _isMainPlayer;

    GameObject _follower;
    public GameObject Follower { get => _follower; set => _follower = value; }

    void Awake()
    {
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (pubData) =>
        {
            GameObject eliminatedChar = pubData as GameObject;
            if (eliminatedChar == this._follower)
            {
                Destroy(this._sprite);
                this.gameObject.SetActive(false);
            }
        });
    }

    void Update()
    {
        if (this._isMainPlayer) this._follower = CameraExtension.GetCurrentCamera().gameObject;

        if (this._follower != null && this._follower.activeInHierarchy)
        {
            float playerX = this._follower.transform.position.x;
            float playerZ = this._follower.transform.position.z;
            this.transform.position = new Vector3(playerX, 1, playerZ);
        }
        else
        {
            if (this._follower != null && !this._follower.activeInHierarchy)
            {
                Destroy(this._sprite);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SetVisible(bool isVisible)
    {
        if (this._sprite == null) return;
        this._sprite.GetComponent<SpriteRenderer>().enabled = isVisible;
    }
}
