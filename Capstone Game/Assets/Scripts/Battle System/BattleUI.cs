using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class BattleUI : MonoBehaviour
{
    //UI Buttons
    [SerializeField] private Canvas defaultButtons;
    [SerializeField] private Canvas actionButtons;
    [SerializeField] private Canvas skillButtons;
    [SerializeField] private Canvas selectButtons;
    [SerializeField] private Canvas partyButtons;
    [SerializeField] public Canvas unitHuds;
    [SerializeField] private Canvas statView;
    [SerializeField] private Canvas skillView;
    [SerializeField] private Canvas inventoryView;
    [SerializeField] private Inventory inv;
    
    public void SetDefaultButtons(bool set)
    {
        defaultButtons.enabled = set;
    }

    public void SetActionButtons(bool set)
    {
        actionButtons.enabled = set;
    }

    public void SetSkillButtons(bool set)
    {
        skillButtons.enabled = set;
    }

    public void SetSelectButtons(bool set)
    {
        selectButtons.enabled = set;
    }

    public void SetPartyButtons(bool set)
    {
        partyButtons.enabled = set;
    }

    public void SetUnitHUD(bool set)
    {
        unitHuds.enabled = set;
    }

    public void SetStatView(bool set)
    {
        statView.enabled = set;
    }
    
    public void SetSkillView(bool set)
    {
        skillView.enabled = set;
    }

    public void SetInventoryView(bool set)
    {
        inventoryView.enabled = set;
        if (set)
        {
            inv.OpenUI();
        }
        else
        {
            inv.CloseUI();
        }

        if(FindObjectOfType<BattleSystem>().unit1.Unit != null)
        {
            StartCoroutine(FindObjectOfType<BattleSystem>().unit1.hud.UpdateHpBar());
            StartCoroutine(FindObjectOfType<BattleSystem>().unit1.hud.UpdateStaBar());
        }
        if(FindObjectOfType<BattleSystem>().unit2.Unit != null)
        {
            StartCoroutine(FindObjectOfType<BattleSystem>().unit2.hud.UpdateHpBar());
            StartCoroutine(FindObjectOfType<BattleSystem>().unit2.hud.UpdateStaBar());
        }
        
        
    } 
    
    
    public void SetupBattle()
    {
        actionButtons.enabled = false;
        skillButtons.enabled = false;
        defaultButtons.enabled = false;
        selectButtons.enabled = false;
        statView.enabled = false;
        skillView.enabled = false;
        unitHuds.enabled = true;
        inventoryView.enabled = false;
    }
    
    public void EndBattle()
    {
        actionButtons.enabled = false;
        skillButtons.enabled = false;
        defaultButtons.enabled = false;
        selectButtons.enabled = false;
        statView.enabled = false;
        skillView.enabled = false;
        partyButtons.enabled = false;
        unitHuds.enabled = false;
        statView.enabled = false;
        inventoryView.enabled = false;
        //skillView.enabled = false;
    }
}
