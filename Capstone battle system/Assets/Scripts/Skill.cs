using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public MoveBase Base { get; set; }

    public Skill(MoveBase uBase)
    {
        Base = uBase;
    }
}
