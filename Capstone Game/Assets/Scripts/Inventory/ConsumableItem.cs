using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create consumable item")]
public class ConsumableItem : ItemBase
{
    [Header("HP")]
    [SerializeField] private int hpHeal;
    [SerializeField] private bool restoreHp;
    
    [Header("Stamina")]
    [SerializeField] private int staHeal;
    [SerializeField] private bool restoreSta;
    
    [Header("Status")]
    [SerializeField] private int status;
    [SerializeField] private bool restoreAllStatus;

    [Header("Revive")] 
    [SerializeField] private bool revive;
    [SerializeField] private int reviveAmt;
    [SerializeField] private bool maxRevive;

    public override bool Use(Unit unit)
    {
        
        if (unit.HP == 0 && !revive)
        {
            return false;
        }
        if (unit.HP == 0 && revive)
        {
            unit.IncreaseHp(reviveAmt);
            return true;
        }

        if (unit.HP > 0 && revive)
        {
            return false;
        }

        if (hpHeal > 0 && staHeal > 0)
        {
            if (unit.HP == unit.MaxHealth && unit.STA == unit.MaxStamina)
            {
                return false;
            }
            else
            {
                unit.IncreaseHp(hpHeal);
                unit.IncreaseSTA(staHeal);
                return true;
            }
        }
        if (hpHeal > 0)
        {
            if (unit.HP == unit.MaxHealth)
            {
                return false;
            }
            
            unit.IncreaseHp(hpHeal);
        }
        
        if (staHeal > 0)
        {
            if (unit.STA == unit.MaxStamina)
            {
                return false;
            }
            
            unit.IncreaseSTA(staHeal);
        }
        if (restoreAllStatus)
        {
            if (unit.Status == null)
            {
                return false;
            }
            
        }

        return true;
    }
}
