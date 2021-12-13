using System.Collections;
using System.Collections.Generic;
using Character;
using Photon.Pun;
using UnityEngine;
using Utilities;

namespace HawkDrone
{
    public class UtilityHawk : EquipUtility
    {
        [SerializeField] GameObject _drone;

        protected override void ActiveUtil()
        {
            base.ActiveUtil();

            Vector3 ownerPos = this._owner.transform.position;
            Quaternion rotation = this._owner.transform.rotation;

            GameObject drone = PhotonNetwork.Instantiate(_drone.name, ownerPos, rotation);
            drone.GetComponent<HawkDroneEngine>().SetOwner(this._owner);
        }
    }
}
