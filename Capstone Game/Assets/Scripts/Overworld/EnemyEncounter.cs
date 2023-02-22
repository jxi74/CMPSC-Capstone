using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] public List<Unit> enemyEncounter;
    private List<int> usedUnitIndices = new List<int>(); // List of indices of units that have already been returned
    public Unit GetRandomUnit()
    {
        // Find an index that hasn't been used yet
        int index;
        do
        {
            index = Random.Range(0, enemyEncounter.Count);
        } while (usedUnitIndices.Contains(index));

        // Mark the index as used
        usedUnitIndices.Add(index);

        // Initialize and return the unit at the selected index
        Unit enemy = enemyEncounter[index];
        enemy.Init();
        return enemy;
    }
}
