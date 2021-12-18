using System.Collections;
using System.Collections.Generic;
using Cats;
using Character;
using UnityEngine;

public class CatGenCorpe : MonoBehaviour
{
    [SerializeField] GameObject _normalCorpse;
    [SerializeField] GameObject _rageCorpe;

    void Awake()
    {
        EventCenter.Subcribe(EventId.CHARACTER_ELIMINATED, (object pubData) =>
        {
            GameObject dieObject = pubData as GameObject;
            CharacterSide side = dieObject.GetComponent<CharacterStats>().CharacterSide;
            if (side == CharacterSide.CATS)
            {
                this.GenCatCorpse(dieObject);
            }
        });
    }

    void GenCatCorpse(GameObject dieObject)
    {
        bool isWeakened = dieObject.GetComponent<CatHealthEngine>().IsDying;
        Vector3 curPos = dieObject.transform.position;
        Quaternion curRot = dieObject.transform.rotation;
        GameObject corpse;
        if (!isWeakened) corpse = Instantiate(_normalCorpse, curPos, curRot);
        else corpse = Instantiate(_rageCorpe, curPos, curRot);
    }
}
