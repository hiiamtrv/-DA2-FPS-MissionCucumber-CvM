using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Retreat : BaseState, IAIState
    {
        public bool IsTargetLockMode() { return true; }

        bool _destinationSet = false;
        public GameObject Chaser { get; set; }

        public AIBaseEngine StateMachine => (AIBaseEngine)this._stateMachine;
        public Retreat(StateMachine stateMachine) : base(stateMachine)
        {
            this.Chaser = null;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            this.StateMachine.agent.destination = this._gameObject.transform.position;
            base.OnExit();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            if (this.Chaser != null && !this._destinationSet)
            {
                GameObject retreatPoint = this.GetRetreatPoint();
                if (retreatPoint != null)
                {
                    this.StateMachine.agent.SetDestination(retreatPoint.transform.position);
                    this._destinationSet = true;
                }
            }
        }

        protected override void CheckNextState()
        {
            base.CheckNextState();
            if (this.Chaser != null && !this.IsVisibleFromChaser())
            {
                BaseState nextState = this.StateMachine.RollNextState();
                this.SetNextState(nextState);
            }
        }

        GameObject GetRetreatPoint()
        {
            CharacterSide side = this.StateMachine.Side;
            switch (side)
            {
                case CharacterSide.MICE:
                    {
                        List<GameObject> listObject = new List<GameObject>();
                        listObject.AddRange(Spawner.Ins.CucumberPoints);
                        listObject.Add(Spawner.Ins.MouseRetreatPoint);
                        return this.GetFurthestPointFromChaser(listObject);
                    }
            }
            return null;
        }

        GameObject GetFurthestPointFromChaser(List<GameObject> listObject)
        {
            if (this.Chaser == null) return null;

            GameObject furthestObject = null;
            float furthestDistance = 0;

            listObject.ForEach(gameObject =>
            {
                float distance = Vector3.Distance(this.Chaser.transform.position, gameObject.transform.position);
                if (furthestObject == null || distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthestObject = gameObject;
                }
            });

            return furthestObject;
        }

        protected bool IsVisibleFromChaser()
        {
            Vector3 direction = this._gameObject.transform.position - this.Chaser.transform.position;
            float distance = direction.magnitude;
            return !Physics.Raycast(this.Chaser.transform.position, direction, distance, LayerMask.GetMask("Map"));
        }
    }
}
