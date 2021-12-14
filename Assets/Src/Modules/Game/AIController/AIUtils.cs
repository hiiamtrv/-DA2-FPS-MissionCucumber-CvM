using System.Collections;
using System.Collections.Generic;
using AI;
using Character;
using ShieldCenter;
using UnityEngine;
using Interactable;

public static class AIUtils
{
    public const float MIN_ACCEPTABLE_DISTANCE = 4;

    public static Vector3? GetRandomPoint(this AIBaseEngine engine, List<GameObject> listPoint)
    {
        List<Vector3> pointPos = listPoint.ConvertAll(cucumber => cucumber.transform.position);
        pointPos = pointPos.FindAll(pos => Vector3.Distance(engine.transform.position, pos) > MIN_ACCEPTABLE_DISTANCE);

        if (pointPos.Count > 0)
        {
            return Utils.PickFromList<Vector3>(pointPos);
        }
        else return null;
    }

    public static List<GameObject> GetSpottedEnemies(this AIBaseEngine engine)
    {
        List<GameObject> enemies = CharacterMgr.Ins.Characters.FindAll(go =>
        {
            Debug.Log("Checking object", go);
            CharacterSide side = go.GetComponent<CharacterStats>().CharacterSide;
            return side != CharacterSide.UNDEFINED && side != engine.Side;
        });

        enemies = enemies.FindAll(enemy => engine.CanSeeTarget(enemy));
        return enemies;
    }

    public static bool IsEnemyNoticable(this AIBaseEngine engine, GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - engine.transform.position;
        float distance = direction.magnitude;

        return !Physics.Raycast(engine.transform.position, direction, distance, LayerMask.GetMask("Map"))
        && Vector3.Distance(engine.transform.position, enemy.transform.position) <= engine.NoticeRange;
    }

    public static bool IsEnemyAttackable(this AIBaseEngine engine, GameObject enemy)
    {
        if (enemy == null) return false;
        else
        {
            float distance = Vector3.Distance(enemy.transform.position, engine.transform.position);
            return (distance <= engine.AttackRange);
        }
    }

    public static List<GameObject> GetNearInteractables(this AIBaseEngine engine)
    {
        List<GameObject> interactables = new List<GameObject>();
        interactables.AddRange(ObjectiveTracker.Ins.Cucumbers);
        interactables.Add(ShieldCenterEngine.Ins.gameObject);
        interactables = interactables.FindAll(interactable =>
        {
            if (interactable.activeInHierarchy)
            {
                float range = interactable.GetComponent<InteractModel>().InteractRadius;
                return Vector3.Distance(engine.transform.position, interactable.transform.position) <= range;
            }
            else return false;
        });
        return interactables;
    }

    public static bool CanSeeTarget(this AIBaseEngine engine, GameObject target)
    {
        return (
            target.activeInHierarchy
            && (
                engine.Eye.IsObjectVisible(target.transform.position)
                || engine.IsEnemyNoticable(target)
                )
            );
    }
}
