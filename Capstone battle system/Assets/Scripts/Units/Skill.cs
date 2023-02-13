using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public MoveBase Base { get; set; }
    public int Power { get; set; }
    public int Accuracy { get; set; }
    public int StaminaCost { get; set; }

    public Skill(MoveBase uBase)
    {
        Base = uBase;
        Power = uBase.power;
        Accuracy = uBase.accuracy;
        StaminaCost = uBase.staminaCost;

    }
}
