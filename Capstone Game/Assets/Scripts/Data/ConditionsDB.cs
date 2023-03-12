using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ConditionsDB : MonoBehaviour
{

    public static Dictionary<ConditionID, Effect> Conditions { get; set; } = new Dictionary<ConditionID, Effect>()
    {
        {
            ConditionID.Poisoned,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Poison",
                StartMsg = "has been poisoned!",
                OnAfterTurn = (Unit unit) =>
                {
                    //PoisonEffect
                    unit.UpdateHP(Mathf.FloorToInt(unit.MaxHealth * .15f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} gets sapped by poison!");
                }
            }
        },
        {
            ConditionID.Rejuvenated,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Rejuvenated",
                StartMsg = "feels rejuvenated!",
                OnAfterTurn = (Unit unit) =>
                {
                    //Regenerate
                    unit.UpdateHP(-Mathf.FloorToInt(unit.MaxHealth * .075f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling rejuvenated!");
                }
            }
        },
        {
            ConditionID.Bleeding,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Bleeding",
                StartMsg = "started to bleed out!",
                OnAfterTurn = (Unit unit) =>
                {
                    //Bleed
                    unit.UpdateHP(Mathf.FloorToInt(unit.MaxHealth * .1f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is bleeding out!");
                }
            }
        },
        {
            ConditionID.Fatigued,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Fatigued",
                StartMsg = "feels fatigued!",
                OnAfterTurn = (Unit unit) =>
                {
                    //Lose Sta
                    unit.UpdateSTA(Mathf.FloorToInt(unit.MaxStamina * .2f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling fatigued!");
                }
            }
        },
        {
            ConditionID.Energized,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Energized",
                StartMsg = "feels energized!",
                OnAfterTurn = (Unit unit) =>
                {
                    //Regen Sta
                    unit.UpdateSTA(-Mathf.FloorToInt(unit.MaxStamina * .1f + 1));
                    Debug.Log("Energize: " + Mathf.FloorToInt(unit.MaxStamina * .1f + 1));
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is feeling energized!");
                }
            }
        },
        {
            ConditionID.Dizzy,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Dizzy",
                StartMsg = "feels dizzy!",
                OnBeforeTurn = (Unit unit) =>
                {
                    if (Random.Range(1, 4) == 1)
                    {
                        unit.StatusChanges.Enqueue($"{unit.Base.name} is too dizzy to perform an action!");
                        return false;
                    }

                    return true;
                }
            }
        },
        
        {
            ConditionID.Stunned,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Stunned",
                StartMsg = "got stunned!",
                OnStart = (Unit unit) =>
                {
                    unit.StatusTime = Random.Range(1,3);
                },
                OnBeforeTurn = (Unit unit) =>
                {
                    if (unit.StatusTime <= 0)
                    {
                        unit.CureStatus();
                        unit.StatusChanges.Enqueue($"{unit.Base.name} is no longer stunned!");
                        return true;
                    }
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is stunned and cannot perform an action!");
                    unit.CureStatus();
                    return false;
                }
            }
        },
        
        {
            ConditionID.Asleep,
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            new Effect()
            {
                Name = "Asleep",
                StartMsg = "fell asleep!",
                OnStart = (Unit unit) =>
                {
                    //Sleep for 3 turns
                    unit.StatusTime = 3;
                },
                OnBeforeTurn = (Unit unit) =>
                {
                    if (unit.StatusTime <= 0)
                    {
                        unit.CureStatus();
                        unit.StatusChanges.Enqueue($"{unit.Base.name} woke up!");
                        return true;
                    }
                    unit.StatusTime--;
                    unit.StatusChanges.Enqueue($"{unit.Base.name} is asleep and cannot perform an action!");
                    
                    return false;
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
