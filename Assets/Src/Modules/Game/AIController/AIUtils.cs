using System.Collections;
using System.Collections.Generic;
using AI;
using Character;
using UnityEngine;

public static class AIUtils
{
    public const float MIN_ACCEPTABLE_DISTANCE = 1;

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
            Debug.Log("Heading to cucumber ", engine, cucumberPos.IndexOf(randomPos) + 1);
            engine.agent.SetDestination(randomPos);
            return true;
        }
        else return false;
    }

    public static List<GameObject> GetListPlayerInSight(this AIBaseEngine engine)
    {
        List<GameObject> enemies = CharacterMgr.Ins.Characters.FindAll(go =>
                go.GetComponent<CharacterStats>().CharacterSide != engine.Side
            );


        List<GameObject> enemiesInSight = new List<GameObject>();
        enemies.ForEach(enemy =>
        {
            MeshRenderer renderer = enemy.GetComponent<Eye>().CharModel.GetComponent<MeshRenderer>();
            if (enemy.activeInHierarchy && (engine.Eye.IsObjectVisible(renderer) || engine.IsEnemyNoticable(enemy)))
                enemiesInSight.Add(enemy);
        });
        return enemiesInSight;
    }

    public static bool IsEnemyNoticable(this AIBaseEngine engine, GameObject enemy)
    {
        Vector3 direction = engine.transform.position - enemy.transform.position;
        float distance = direction.magnitude;

        return Vector3.Distance(engine.transform.position, enemy.transform.position) <= engine.NoticeRange
        && !Physics.Raycast(engine.gameObject.transform.position, direction, distance, LayerMask.GetMask("Map"));
    }

    public static bool IsPlayerAttackable(this AIBaseEngine engine, GameObject enemy)
    {
        if (enemy == null) return false;
        else
        {
            float distance = Vector3.Distance(enemy.transform.position, engine.transform.position);
            return (distance <= engine.AttackRange);
        }
    }
}
