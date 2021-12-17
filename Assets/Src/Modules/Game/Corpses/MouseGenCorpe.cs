using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class MouseGenCorpe : MonoBehaviour
{
    [SerializeField] GameObject _normalCorpse;

    void Awake()
    {
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (object pubData) =>
        {
            GameObject dieObject = pubData as GameObject;
            CharacterSide side = dieObject.GetComponent<CharacterStats>().CharacterSide;
            if (side == CharacterSide.MICE)
            {
                this.GenMouseCorpse(dieObject);
            }
        });
    }

    void GenMouseCorpse(GameObject dieObject)
    {
        Vector3 curPos = dieObject.transform.position;
        Quaternion curRot = dieObject.transform.rotation;
        curPos = new Vector3(curPos.x, 0, curPos.z);
        GameObject corpse = Instantiate(_normalCorpse, curPos, curRot);
    }
}
