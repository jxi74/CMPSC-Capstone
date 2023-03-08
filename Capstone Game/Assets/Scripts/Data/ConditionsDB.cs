using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ConditionsDB : MonoBehaviour
{

    public static Dictionary<ConditionID, Effect> Conditions { get; set; } = new Dictionary<ConditionID, Effect>()
    {
        {
            ConditionID.Poisoned,
            new Effect()
            {
                Name = "Poison",
                StartMsg = "has been poisoned!",
                onAfterTurn = (Unit unit) =>
                {
                    //PoisonEffect
                    unit.UpdateHP(Mathf.FloorToInt(unit.MaxHealth * .15f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} gets sapped by poison!");
                }
            }
        },
        {
            ConditionID.Rejuvenated,
            new Effect()
            {
                Name = "Rejuvenated",
                StartMsg = "feels rejuvenated!",
                onAfterTurn = (Unit unit) =>
                {
                    //Regenerate
                    unit.UpdateHP(-Mathf.FloorToInt(unit.MaxHealth * .075f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling rejuvenated!");
                }
            }
        },
        {
            ConditionID.Bleeding,
            new Effect()
            {
                Name = "Bleeding",
                StartMsg = "started to bleed out!",
                onAfterTurn = (Unit unit) =>
                {
                    //Bleed
                    unit.UpdateHP(Mathf.FloorToInt(unit.MaxHealth * .1f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is bleeding out!");
                }
            }
        },
        {
            ConditionID.Fatigued,
            new Effect()
            {
                Name = "Fatigued",
                StartMsg = "feels fatigued!",
                onAfterTurn = (Unit unit) =>
                {
                    //Lose Sta
                    unit.UpdateSTA(Mathf.FloorToInt(unit.MaxStamina * .2f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling fatigued!");
                }
            }
        },
        {
            ConditionID.Energized,
            new Effect()
            {
                Name = "Energized",
                StartMsg = "feels energized!",
                onAfterTurn = (Unit unit) =>
                {
                    //Regen Sta
                    unit.UpdateSTA(-Mathf.FloorToInt(unit.MaxStamina * .1f + 1));
                    Debug.Log("Energize: " + Mathf.FloorToInt(unit.MaxStamina * .1f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling energized!");
                }
            }
        }
        
        
    };
}

public enum ConditionID
{
    None,
    Poisoned,
    Rejuvenated,
    Bleeding,
    Fatigued,
    Energized,
    Asleep,
    Stunned,
    Dizzy
}
