using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour, IDataPersistence
{
    [SerializeField] public List<Unit> units;
    [SerializeField] private BattleSystem battlesystem;
    private void Start()
    {
        foreach (var unit in units)
        {
            unit.Init();
            //Debug.Log("GetFirstHealthyUnit() returned: " + (unit != null ? unit.Base.Name : "null"));
            //Debug.Log("GetNextHealthyUnit() returned: " + (GetNextHealthyUnit(unit) != null ? GetNextHealthyUnit(unit).Base.Name : "null"));
        }
        
    }

    public Unit GetFirstHealthyUnit()
    {
        return units.Where(x => x.HP > 0).FirstOrDefault();
    }

    public Unit GetNextHealthyUnitStart(Unit currentUnit)
    {
        if (units.Count == 1)
        {
            return null;
        }
        int currentIndex = units.IndexOf(currentUnit);
        for (int i = 0; i < units.Count; i++)
        {
            int index = (currentIndex + i + 1) % units.Count;
            if (units[index].HP > 0)
            {
                return units[index];
            }
        }
        return null;
    }
    
    public Unit GetNextHealthyUnit(Unit currentUnit)
    {
        if (currentUnit == null)
        {
            return GetFirstHealthyUnit();
        }

        int currentIndex = units.IndexOf(currentUnit);
        int iterations = 0;
        int maxIterations = units.Count;

        Debug.Log($"{currentUnit.Base.Name}");
        Debug.Log($"{currentUnit.Base.Name} Index: = {currentIndex}");
        while (iterations < maxIterations)
        {
            int index = (currentIndex + iterations + 1) % units.Count;
            
            if (units[index].HP > 0 && units[index] != battlesystem.unit1.Unit && units[index] != battlesystem.unit2.Unit)
            {
                return units[index];
            }
            iterations++;
        }

        return null;
    }
    
    public void LoadData(GameData data)
    {
        this.units = data.party;
    }

    public void SaveData(GameData data)
    {
        data.party = this.units;
    }
}
