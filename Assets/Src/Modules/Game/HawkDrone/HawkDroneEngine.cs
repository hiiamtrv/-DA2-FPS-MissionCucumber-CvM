using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using Photon.Pun;
using UnityEngine;

namespace HawkDrone
{
    public class HawkDroneEngine : StateMachine
    {
        [SerializeField] float _remainTime;
        [SerializeField] GameObject _markBullet;
        [SerializeField] GameObject _model;
        [SerializeField] Canvas _hud;

        GameObject _owner = null;

        GameObject _eye;
        MoveModel _moveModel;
        CharacterController _charCtrl;
        PhotonView _view;

        UtilityHawk _utility;

        public override BaseState GetDefaultState() => new Fly(this);

        protected override void Start()
        {
            base.Start();

            _view = this.GetComponent<PhotonView>();
            _moveModel = this.GetComponent<CharacterStats>().MoveModel;
            _charCtrl = this.GetComponent<CharacterController>();
            _charCtrl.enabled = true;
            _eye = this.GetComponent<Eye>().MainCamera.gameObject;
            this.GetComponent<Eye>().MainCamera.enabled = true;

            float delay = Mathf.Max(_remainTime, Time.deltaTime);
            LeanTween.delayedCall(delay, () =>
            {
                this.Destroy();
            });

            this.transform.Translate(Vector3.forward + Vector3.up, Space.Self);

            if (!this._view.IsMine)
            {
                this._eye.SetActive(false);
                this.enabled = false;
                this._hud.gameObject.SetActive(false);
            }
            else
            {
                this._model.gameObject.SetActive(false);
                Crosshair.Ins.Hide();
            }

            EventCenter.Subcribe(EventId.MATCH_END, (pubData) =>
            {
                if (_view.IsMine)
                {
                    Crosshair.Ins.Show();
                }
                this.Destroy();
            });

            EventCenter.Subcribe(EventId.HEALTH_CHANGE, (pubData) =>
            {
                PubData.HealthChange data = pubData as PubData.HealthChange;
                if (data.IsDamageEvent
                    && (data.Dispatcher == this.gameObject || data.Dispatcher == this._owner)
                )
                {
                    this.Destroy();
                }
            });

            EventCenter.Subcribe(EventId.SHILED_CHANGE, (pubData) =>
            {
                PubData.ShieldChange data = pubData as PubData.ShieldChange;
                if (data.Reason == ShieldReason.DAMAGE
                    && (data.Dispatcher == this.gameObject || data.Dispatcher == this._owner)
                )
                {
                    this.Destroy();
                }
            });
        }

        protected override void Update()
        {
            base.Update();
            if (InputMgr.StartShoot(this.gameObject))
            {
                if (this.MarkTarget())
                {
                    if (_view.IsMine)
                    {
                        Crosshair.Ins.Show();
                    }
                    this.Destroy();
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (this._view.IsMine && this != null)
            {
                base.FixedUpdate();

                Debug.Log("check enable", this._charCtrl.enabled);
                float x = (this._currentState as Fly).X;
                float z = Mathf.Min(1.5f, (this._currentState as Fly).Z + 0.5f);
                Vector3 localMove = (Vector3.forward * z + Vector3.right * x) * this._moveModel.Speed * Time.fixedDeltaTime;
                Vector3 worldMove = this._eye.transform.TransformDirection(localMove);
                this._charCtrl.Move(worldMove);
            }
        }

        public void SetOwner(GameObject owner)
        {
            this._owner = owner;
            this._owner.GetComponent<Eye>().enabled = false;
            this._owner.GetComponent<MoveEngine>().enabled = false;
            this._owner.GetComponent<CharacterController>().enabled = false;
            this._owner.transform.Find("EyePoint").gameObject.SetActive(false);

            this.GetComponent<Eye>().MainCamera.enabled = true;
        }

        public override void Destroy()
        {
            this.GetComponent<Eye>().MainCamera.enabled = false;

            this._owner.GetComponent<Eye>().enabled = true;
            this._owner.GetComponent<MoveEngine>().enabled = true;
            this._owner.GetComponent<CharacterController>().enabled = true;
            this._owner.transform.Find("EyePoint").gameObject.SetActive(true);

            this._utility.FinishUtilAction();

            base.Destroy();
        }

        protected bool MarkTarget()
        {
            RaycastHit hit;
            Ray ray = new Ray(this._eye.transform.position, this._eye.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.collider.gameObject;
                if (target != this._owner && target.GetComponent<CharacterStats>())
                {
                    GameObject newMarker = PhotonNetwork.Instantiate(this._markBullet.name, target.transform.position, Quaternion.identity);
                    newMarker.GetComponent<HawkBullet>().SetTarget(target);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void SetBindingUtility(UtilityHawk utility)
        {
            this._utility = utility;
        }
    }
}
