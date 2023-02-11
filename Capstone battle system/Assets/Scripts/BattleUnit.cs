using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] public UnitBase Base;
    [SerializeField] int level;



    public Unit Unit { get; set; }

    public void Setup()
    {
        Unit = new Unit(Base, level);
        //Display Unit model
    }
}
