using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : Equipment, IWeapon
    {
        protected MeleeWeaponModel _model;
        public MeleeWeaponModel Model => this._model;

        protected bool _canAttack;

        [SerializeField] Camera _eye;
        [SerializeField] AudioClip _soundAttack;
        [SerializeField] AudioClip _soundHit;
        [SerializeField] AudioClip _soundEquip;

        protected override void Start()
        {
            this._model = this.GetComponent<MeleeWeaponStats>().Model;
            base.Start();
        }

        void Update()
        {
            if (this.IsReady && this._canAttack && InputMgr.StartShoot(this._owner))
            {
                this.Attack();
            }
        }

        public override void OnEquiped()
        {
            base.OnEquiped();
            this.gameObject.PlaySound(_soundEquip);
        }

        protected void Attack()
        {
            var targets = this.Target;
            this.gameObject.PlaySound(_soundAttack);
            if (targets.Length > 0)
            {
                this.gameObject.PlaySound(_soundHit);
                foreach (GameObject target in targets)
                {
                    this.DoHitEffect(target);
                }

                this._canAttack = false;
                LeanTween.delayedCall(
                    1 / this.Model.AttackSpeed,
                    () => { if (this != null) _canAttack = true; }
                );
            }
        }

        protected override void ActiveEquipment()
        {
            this._canAttack = true;
            base.ActiveEquipment();
        }

        public override void OnUnequiped()
        {
            base.OnUnequiped();
            EventCenter.Publish(
                EventId.WEAPON_UNEQUIP,
                new PubData.WeaponUnequip(this.Owner)
            );
            this.gameObject.PlaySound(_soundEquip);
        }

        protected virtual void DoHitEffect(GameObject target)
        {
            //TODO: override to inflict effects other than damage

            HealthEngine health = target.GetComponent<HealthEngine>();
            if (health)
            {
                health.InflictDamage(this.Model.Damage);
            }
        }

        public virtual GameObject[] Target
        {
            get
            {
                List<GameObject> targets = new List<GameObject>();

                float screenX = Screen.width * 0.5f;
                float screenY = Screen.height * 0.5f;
                RaycastHit hit;
                Ray ray = this._eye.ScreenPointToRay(new Vector2(screenX, screenY));
                float weaponRange = this.Model.AttackRange;

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    targets.Add(hit.collider.gameObject);
                }

                return targets.ToArray();
            }
        }

        public virtual void TriggerAttack()
        {
            this.Attack();
        }

        public virtual void TriggerReload()
        {
            //do nothing
        }

        public bool NeedReload()
        {
            return false;
        }
    }
}
