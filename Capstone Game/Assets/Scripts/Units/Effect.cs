using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartMsg { get; set; }

    public Action<Unit> onAfterTurn { get; set; }
}
