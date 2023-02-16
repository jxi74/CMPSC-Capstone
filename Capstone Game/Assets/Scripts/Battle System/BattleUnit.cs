using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleUnit : MonoBehaviour
{
    [FormerlySerializedAs("base")] // use this attribute to rename the 'base' field to 'unitBase'
    public UnitBase unitBase; // rename the field to 'unitBase'
    public int level;

    public Unit Unit { get; set; }

    public void Setup(Unit unit)
    {
        Unit = unit;
        unitBase = unit.Base;
        level = unit.Level;
        //Display Unit model
    }
}