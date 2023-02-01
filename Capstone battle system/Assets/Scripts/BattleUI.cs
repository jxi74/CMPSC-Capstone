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

    public void SetupBattle()
    {
        actionButtons.enabled = false;
        skillButtons.enabled = false;
        defaultButtons.enabled = false;
    }
}
