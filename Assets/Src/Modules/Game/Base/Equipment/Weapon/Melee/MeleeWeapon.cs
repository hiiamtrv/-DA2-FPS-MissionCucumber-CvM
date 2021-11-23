using System.Collections;
using System.Collections.Generic;
using Character;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : Equipment
    {
        protected MeleeWeaponModel _model;
        public MeleeWeaponModel Model => this._model;

        protected bool _canAttack;

        [SerializeField] Camera _eye;
        [SerializeField] AudioClip _soundAttack;
        [SerializeField] AudioClip _soundEquip;
        AudioSource _audio = new AudioSource();

        protected override void Start()
        {
            this._model = this.GetComponent<MeleeWeaponStats>().Model;
            base.Start();
        }

        void Update()
        {
            if (this.IsReady && this._canAttack && InputMgr.StartShoot)
            {
                this.Attack();
            }
        }

        protected void Attack()
        {
            var targets = this.Target;
            if (targets.Length > 0)
            {
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
    }
}
