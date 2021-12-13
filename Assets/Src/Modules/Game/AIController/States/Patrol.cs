using System.Collections;
using System.Collections.Generic;
using Character;
using ShieldCenter;
using UnityEngine;

namespace AI
{
    public class Patrol : BaseState, IAIState
    {
        public bool IsTargetLockMode() { return false; }
        bool destinationSet = false;

        public AIBaseEngine StateMachine => (AIBaseEngine)this._stateMachine;
        public Patrol(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            base.OnEnter();
            this.destinationSet = false;
            this.StateMachine.GetComponent<Eye>().ResetRotation();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!destinationSet)
            {
                List<GameObject> listPoint = this.GetListPoint();
                Vector3? destination = this.StateMachine.GetRandomPoint(listPoint);
                if (destination != null)
                {
                    destinationSet = true;
                    this.StateMachine.agent.SetDestination((Vector3)destination);
                }

                if (this.StateMachine.Side == CharacterSide.CATS)
                {
                    Debug.Log("Set destination ", destinationSet, this.StateMachine.Side, this.StateMachine.agent.destination);
                }
            }
        }

        protected override void CheckNextState()
        {
            Vector3 destination = this.StateMachine.agent.destination;
            Vector3 curPos = this._gameObject.transform.position;
            if (Vector3.Distance(destination, curPos) <= AIUtils.MIN_ACCEPTABLE_DISTANCE)
            {
                this.StateMachine.OnEndAction();
            }
        }

        public override void OnExit()
        {
            this.StateMachine.agent.destination = this._gameObject.transform.position;
            base.OnExit();
        }

        List<GameObject> GetListPoint()
        {
            List<GameObject> listPoint = new List<GameObject>();
            CharacterSide side = this.StateMachine.Side;
            switch (side)
            {
                case CharacterSide.CATS:
                    if (ObjectiveTracker.Ins != null && ShieldCenterEngine.Ins != null)
                    {
                        listPoint.AddRange(ObjectiveTracker.Ins.Cucumbers);
                        listPoint.Add(ShieldCenterEngine.Ins.gameObject);
                    }
                    break;
                case CharacterSide.MICE:
                    if (Spawner.Ins != null)
                    {
                        listPoint.AddRange(Spawner.Ins.CucumberPoints);
                    }
                    break;
            }
            return listPoint;
        }
    }
}
