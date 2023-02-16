using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] private List<Unit> units;
    private void Start()
    {
        foreach (var unit in units)
        {
            unit.Init();
        }
    }

    public Unit GetFirstHealthyUnit()
    {
        return units.Where(x => x.HP > 0).FirstOrDefault();
    }

    public Unit GetNextHealthyUnit(Unit currentUnit)
    {
        int currentIndex = units.IndexOf(currentUnit);
        for (int i = currentIndex + 1; i < units.Count; i++)
        {
            if (units[i].HP > 0)
            {
                return units[i];
            }
        }
        return null;
    }
}
