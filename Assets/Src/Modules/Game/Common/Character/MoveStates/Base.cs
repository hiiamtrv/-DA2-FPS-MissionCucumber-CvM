using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    namespace MoveState
    {
        public class Base : BaseState
        {
            protected CharacterController _charCtrl;
            protected MoveModel Model => ((MoveEngine)this._stateMachine).Model;

            protected float MoveX { get => this.Model.MoveX; set => this.Model.MoveX = value; }
            protected float MoveY { get => this.Model.MoveY; set => this.Model.MoveY = value; }
            protected float MoveZ { get => this.Model.MoveZ; set => this.Model.MoveZ = value; }

            public Base(StateMachine stateMachine) : base(stateMachine)
            {
                this._charCtrl = _gameObject.GetComponent<CharacterController>();
            }

            public override void PhysicUpdate()
            {
                this.HandleGravity();
                this.ApplyMove();
            }

            #region ACTIONS
            protected void HandleGravity()
            {
                float GRAVITY_DECREASE = 0.7f;
                if (!Utils.IsTouchFoot(this._charCtrl))
                {
                    Vector3 vtGravity = Physics.gravity * Time.fixedDeltaTime * (this.MoveY > 0 ? GRAVITY_DECREASE : 1);
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
                if (walkMove != WalkMode.X_ONLY) this.MoveZ = InputMgr.ZMove;
                if (walkMove != WalkMode.Z_ONLY) this.MoveX = InputMgr.XMove;
            }

            protected void Jump(float? height = null)
            {
                float jumpHeight = (height != null ? (float)height : this.Model.JumpHeight);
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

                float speed = this.Model.Speed;
                Debug.LogFormat("Speed {0}", speed);
                Debug.LogFormat("MoveX {0}", this.MoveX);
                Debug.LogFormat("MoveZ {0}", this.MoveZ);
                Vector3 vtXZ = new Vector3(this.MoveX, 0, this.MoveZ) * speed * Time.fixedDeltaTime;
                Vector3 vtY = this.MoveY * Vector3.up;
                Vector3 vtMove = this._stateMachine.transform.TransformDirection(vtXZ + vtY);
                Debug.LogFormat("Vector Move {0}", vtMove);

                this._charCtrl.Move(vtMove);
            }

            void AdjustXZ()
            {
                (float X, float Z) adjustment = Utils.AdjustXZ(this.MoveX, this.MoveZ);
                this.MoveX = adjustment.X;
                this.MoveZ = adjustment.Z;
            }
            #endregion
        }
    }

    public enum WalkMode
    {
        BOTH,   //ANY DIRECTIONS
        Z_ONLY, //FORWARD O BACK
        X_ONLY, //LEFT O RIGHT
    }
}