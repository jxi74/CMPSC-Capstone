using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Units/Create skill")]

public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private UnitBase.Type type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int stamina_cost;

}
