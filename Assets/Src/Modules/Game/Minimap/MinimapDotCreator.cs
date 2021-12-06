using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDotCreator : MonoBehaviour
{
    [SerializeField] GameObject _catDot;
    [SerializeField] GameObject _miceDot;

    void Awake()
    {
        EventCenter.Subcribe(EventId.MINIMAP_CREATE_DOT, (data) =>
        {
            GameObject player = data as GameObject;
            CharacterSide side = player.GetComponent<Character.CharacterStats>().CharacterSide;
            Debug.Log("Create dot", player, side);
            GameObject dot = null;
            switch (side)
            {
                case CharacterSide.CATS:
                    dot = Instantiate(_catDot, this.transform.position, Quaternion.identity, this.gameObject.transform);
                    break;
                case CharacterSide.MICE:
                    dot = Instantiate(_miceDot, this.transform.position, Quaternion.identity, this.gameObject.transform);
                    break;
            }

            if (dot != null)
            {
                dot.GetComponent<MinimapDot>().Follower = player;
            }
        });
    }
}
