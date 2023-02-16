using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Skills", menuName = "Units/Create skill")]

public class MoveBase : ScriptableObject
{
    [SerializeField] public new string name;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private UnitBase.Type type;
    [SerializeField] public int power;
    [SerializeField] public int accuracy;
    [FormerlySerializedAs("stamina_cost")] [SerializeField] public int staminaCost;


    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public UnitBase.Type Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int Stamina
    {
        get { return staminaCost; }
    }

    public bool IsSpecial
    {
        get
        {
            if (type == UnitBase.Type.Water || type == UnitBase.Type.Wind || type == UnitBase.Type.Fire || type == UnitBase.Type.Thunder || type == UnitBase.Type.Ice
                || type == UnitBase.Type.Shadow || type == UnitBase.Type.Moon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
