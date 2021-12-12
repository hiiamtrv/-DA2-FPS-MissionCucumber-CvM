using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace HawkDrone
{
    public class HawkDroneEngine : StateMachine
    {
        [SerializeField] GameObject _markBullet;

        Camera _ownerEye = null;

        GameObject _eye;
        MoveModel _moveModel;
        CharacterController _charCtrl;

        public override BaseState GetDefaultState() => new Fly(this);

        protected override void Start()
        {
            base.Start();
            _moveModel = this.GetComponent<CharacterStats>().MoveModel;
            _charCtrl = this.GetComponent<CharacterController>();
            _charCtrl.enabled = true;
            _eye = this.GetComponent<Eye>().MainCamera.gameObject;
            this.GetComponent<Eye>().MainCamera.enabled = true;
        }

        protected override void Update()
        {
            base.Update();
            this.GetComponent<Eye>().MainCamera.enabled = (this._ownerEye != null);
            if (InputMgr.StartShoot(this.gameObject))
            {
                if (this.MarkTarget())
                {
                    this.Destroy();
                }
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Debug.Log("check enable", this._charCtrl.enabled);
            float x = (this._currentState as Fly).X;
            float z = (this._currentState as Fly).Z;
            Vector3 localMove = (Vector3.forward * z + Vector3.right * x) * this._moveModel.Speed * Time.fixedDeltaTime;
            Vector3 worldMove = this._eye.transform.TransformDirection(localMove);
            this._charCtrl.Move(worldMove);
        }

        public void SetOwnerEye(Camera ownerEye)
        {
            this._ownerEye = ownerEye;
            this._ownerEye.enabled = false;
            this.GetComponent<Eye>().MainCamera.enabled = true;
        }

        public override void Destroy()
        {
            this._ownerEye.enabled = true;
            base.Destroy();
        }

        protected bool MarkTarget()
        {
            RaycastHit hit;
            Ray ray = new Ray(this._eye.transform.position, this._eye.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.collider.gameObject;
                if (target.GetComponent<CharacterStats>())
                {
                    GameObject newMarker = Instantiate(this._markBullet, target.transform.position, Quaternion.identity);
                    newMarker.GetComponent<HawkBullet>().SetTarget(target);
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
