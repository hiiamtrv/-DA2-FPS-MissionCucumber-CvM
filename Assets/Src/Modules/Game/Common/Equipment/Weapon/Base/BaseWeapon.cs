using System.Collections;
using System.Collections.Generic;
using Equipments;
using UnityEngine;

namespace Weapons
{
    public class BaseWeapon : Equipment
    {
        [SerializeField] WeaponName _name;
        public WeaponName Name => this._name;

        protected WeaponModel _model;
        public WeaponModel Model => this._model;  

        void Start()
        {
            
        }

        void Update()
        {
            
        }
    }
}
