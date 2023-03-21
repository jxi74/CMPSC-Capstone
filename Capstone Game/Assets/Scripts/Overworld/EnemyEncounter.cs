using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] public List<Unit> enemyEncounter;
    public Unit unit;
    private List<int> usedUnitIndices = new List<int>(); // List of indices of units that have already been returned

    private void Start()
    {
        //Assign random enemy encounter unit for parent object (to display in overworld)
        unit = GetRandomUnit();
        GetComponent<BattleUnit>().Unit = unit;
        GetComponentInChildren<unithud>().setName(unit.Base.Name, unit.Level);
    }

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