using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartMsg { get; set; }

    public ConditionID id { get; set; }

    public Action<Unit> OnStart { get; set; }
    public Func<Unit, bool> OnBeforeTurn { get; set; }
    public Action<Unit> OnAfterTurn { get; set; }
    
    public override string ToString()
    {
        return Name;
    }
}
