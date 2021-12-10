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

    public static bool GotoRandomCucumberPos(this AIBaseEngine engine)
    {
        if (Spawner.Ins == null) return false;
        // List<GameObject> cucumbers = Spawner.Ins.CucumberPoints.FindAll(cucumber => cucumber.name.Contains("9") || cucumber.name.Contains("10"));
        List<GameObject> cucumbers = Spawner.Ins.CucumberPoints;
        List<Vector3> cucumberPos = cucumbers.ConvertAll(cucumber => cucumber.transform.position);
        cucumberPos = cucumberPos.FindAll(pos => Vector3.Distance(engine.transform.position, pos) > MIN_ACCEPTABLE_DISTANCE);

        if (cucumberPos.Count > 0)
        {
            Vector3 randomPos = Utils.PickFromList<Vector3>(cucumberPos);
            // Debug.Log("Heading to cucumber ", engine, cucumberPos.IndexOf(randomPos) + 1);
            engine.agent.SetDestination(randomPos);
            return true;
        }
        else return false;
    }

    public static List<GameObject> GetSpottedEnemies(this AIBaseEngine engine)
    {
        List<GameObject> enemies = CharacterMgr.Ins.Characters.FindAll(go =>
                go.GetComponent<CharacterStats>().CharacterSide != engine.Side
            );

        enemies = enemies.FindAll(enemy => engine.CanSeeTarget(enemy));
        return enemies;
    }

    public static bool IsEnemyNoticable(this AIBaseEngine engine, GameObject enemy)
    {
        Vector3 direction = engine.transform.position - enemy.transform.position;
        float distance = direction.magnitude;

        return !Physics.Raycast(engine.gameObject.transform.position, direction, distance, LayerMask.GetMask("Map"))
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
        MeshRenderer renderer = target.GetComponent<Eye>().CharModel.GetComponent<MeshRenderer>();
        return (target.activeInHierarchy && (engine.Eye.IsObjectVisible(renderer) || engine.IsEnemyNoticable(target)));
    }
}
