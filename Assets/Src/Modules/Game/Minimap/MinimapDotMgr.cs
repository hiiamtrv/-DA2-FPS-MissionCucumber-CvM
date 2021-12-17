using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class MinimapDotMgr : MonoBehaviour
{
    [SerializeField] bool _revealAll;
    [SerializeField] GameObject _catDot;
    [SerializeField] GameObject _miceDot;

    [SerializeField] GameObject _selfDot;

    bool _isMainCharDead = false;

    List<GameObject> _listDot = new List<GameObject>();
    List<GameObject> _listPlayer = new List<GameObject>();
    List<bool> _isVisible = new List<bool>();

    void Awake()
    {
        EventCenter.Subcribe(EventId.MINIMAP_CREATE_DOT, this.AddDot);
    }

    void Start()
    {
        GameObject selfDot = Instantiate(_selfDot, this.transform.position, Quaternion.identity, this.gameObject.transform);
    }

    void LateUpdate()
    {
        if (GameVar.Ins.Player != null && (!_isMainCharDead || _revealAll))
        {
            this._isVisible = new List<bool>();
            for (var i = 0; i < this._listPlayer.Count; i++) this._isVisible.Add(this._revealAll);

            CharacterSide mainPlayerSide = GameVar.Ins.Player.GetComponent<CharacterStats>().CharacterSide;
            for (var i = 0; i < this._listPlayer.Count; i++)
            {
                GameObject player = this._listPlayer[i];
                if (player.GetComponent<CharacterStats>().CharacterSide == mainPlayerSide)
                {
                    Camera eye = player.GetComponent<Eye>().MainCamera;
                    for (var j = 0; j < this._listPlayer.Count; j++)
                    {
                        GameObject otherPlayer = this._listPlayer[j];

                        bool isSelf = otherPlayer == player;
                        bool isOnSameTeam = otherPlayer.GetComponent<CharacterStats>().CharacterSide == mainPlayerSide;
                        bool isVisible = eye.IsObjectVisible(otherPlayer.transform.position);

                        this._isVisible[j] = this._isVisible[j] || isSelf || isOnSameTeam || isVisible;
                    }
                }
            }

            for (var i = 0; i < this._isVisible.Count; i++)
            {
                GameObject dot = this._listDot[i];
                bool isVisible = this._isVisible[i];
                dot.GetComponent<MinimapDot>().SetVisible(isVisible);
            }
        }
        else
        {
            this._isVisible.ForEach(item => item = false);for (var i = 0; i < this._isVisible.Count; i++)
            {
                GameObject dot = this._listDot[i];
                bool isVisible = this._isVisible[i];
                dot.GetComponent<MinimapDot>().SetVisible(isVisible);
            }
        }
    }

    void AddDot(object data)
    {
        GameObject player = data as GameObject;
        CharacterSide side = player.GetComponent<CharacterStats>().CharacterSide;
        Debug.Log("Create dot", player, side);
        GameObject dot = null;

        if (side == GameVar.StartSide) dot = Instantiate(_catDot, this.transform.position, Quaternion.identity, this.gameObject.transform);
        else dot = Instantiate(_miceDot, this.transform.position, Quaternion.identity, this.gameObject.transform);

        if (dot != null)
        {
            dot.GetComponent<MinimapDot>().Follower = player;
            this._listPlayer.Add(player);
            this._listDot.Add(dot);
        }
    }
}
