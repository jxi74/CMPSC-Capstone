using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] public List<Unit> enemyEncounter;

    public Unit GetRandomUnit()
    {
        var enemy = enemyEncounter[Random.Range(0, enemyEncounter.Count)];
        enemy.Init();
        return enemy;
    }
}
