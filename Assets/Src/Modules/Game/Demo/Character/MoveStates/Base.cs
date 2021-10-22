using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMoveState
{
    public class Base : BaseState
    {
        protected CharacterController _characterControler;
        protected float MoveX
        {
            get => ((CharacterEngine)this._stateMachine).GetMoveX();
            set => ((CharacterEngine)this._stateMachine).SetMoveX(value);
        }
        protected float MoveY
        {
            get => ((CharacterEngine)this._stateMachine).GetMoveY();
            set => ((CharacterEngine)this._stateMachine).SetMoveY(value);
        }
        protected float MoveZ
        {
            get => ((CharacterEngine)this._stateMachine).GetMoveZ();
            set => ((CharacterEngine)this._stateMachine).SetMoveZ(value);
        }
        bool _addedGravity = false;

        public Base(StateMachine stateMachine) : base(stateMachine)
        {
            this._characterControler = _gameObject.GetComponent<CharacterController>();
        }

        public override void PhysicUpdate()
        {
            this.HandleGravity();
            this.ApplyMove();
        }

        #region ACTIONS
        protected void HandleGravity()
        {
            if (!CharacterUtils.IsTouchFoot(this._characterControler))
            {
                Vector3 vtGravity = Physics.gravity * Time.fixedDeltaTime * 0.5f;
                this.MoveX += vtGravity.x;
                this.MoveY += vtGravity.y;
                this.MoveZ += vtGravity.z;
            }
            else
            {
                this.MoveY = 0;
            }
        }

        protected void Walk(WalkMode walkMove = WalkMode.BOTH)
        {
            if (walkMove != WalkMode.X_ONLY) this.MoveZ = InputMgr.GetZMove();
            if (walkMove != WalkMode.Z_ONLY) this.MoveX = InputMgr.GetXMove();
        }

        protected void Jump(float? height = null)
        {
            float jumpHeight = (height != null ? (float)height : ((CharacterEngine)this._stateMachine).GetJumpHeight());
            this.MoveY = jumpHeight;
        }

        protected void Crouch()
        {

        }
        #endregion

        #region VECTOR MOVE
        protected void ResetMove()
        {
            this.MoveX = 0;
            this.MoveY = 0;
            this.MoveZ = 0;
        }

        protected void ApplyMove()
        {
            this.AdjustXZ();
            float speed = ((CharacterEngine)this._stateMachine).GetSpeed();
            Vector3 vtXZ = new Vector3(this.MoveX, 0, this.MoveZ) * speed * Time.fixedDeltaTime;
            Vector3 vtY = this.MoveY * Vector3.up;
            Vector3 vtMove = this._stateMachine.transform.TransformDirection(vtXZ + vtY);
            this._characterControler.Move(vtMove);
        }

        void AdjustXZ()
        {
            System.Tuple<float, float> adjustment = CharacterUtils.AdjustXZ(this.MoveX, this.MoveZ);
            this.MoveX = adjustment.Item1;
            this.MoveZ = adjustment.Item2;
        }
        #endregion
    }
}
