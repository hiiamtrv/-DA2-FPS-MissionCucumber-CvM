using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipments
{
    public class Equipment : MonoBehaviour
    {
        protected GameObject _owner;
        public GameObject Owner { get => this._owner; set => this._owner = value; }

        protected float _equipTime;
        public float EquipTime => this._equipTime;

        protected bool _isReady;
        public bool IsReady => this._isReady;

        protected float _drawUnix;
        public float DrawUnix => this._drawUnix;

        protected EquipmentMgr _equipMgr;
        public EquipmentMgr EquipMgr => this._equipMgr;

        const string EQUIPMENT = "Equipment";
        protected GameObject _equipmentObject;
        public GameObject EquipmentObject => this._equipmentObject;

        public bool IsEquiped => this._equipmentObject.activeInHierarchy;

        protected virtual void Awake()
        {
            Transform equipmentTransform = this.transform.Find(EQUIPMENT);
            if (equipmentTransform != null) this._equipmentObject = equipmentTransform.gameObject;
            else Destroy(this);
        }

        protected virtual void Start()
        {
            this._equipMgr = this.GetComponentInParent<EquipmentMgr>();
            if (this._equipMgr == null) Destroy(this.gameObject);
        }

        public virtual void OnEquiped()
        {
            if (this.EquipmentObject != null && !this.IsEquiped)
            {
                this._isReady = false;
                this._drawUnix = Time.time;
                this._equipmentObject.SetActive(true);

                LeanTween.delayedCall(Mathf.Max(Time.deltaTime, this._equipTime), () =>
                {
                    if (this.IsEquiped) this.ActiveEquipment();
                });
            }
        }

        public virtual void OnUnequiped()
        {
            if (this.EquipmentObject != null && this.IsEquiped)
            {
                this._isReady = false;
                this._drawUnix = -1;
                this._equipmentObject.SetActive(false);
            }
        }

        protected virtual void ActiveEquipment()
        {
            this._isReady = true;
        }
    }
}
