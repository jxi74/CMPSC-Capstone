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
    [SerializeField] private int revive;
    [SerializeField] private bool maxRevive;

    public override bool Use(Unit unit)
    {
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

        return true;
    }
}
