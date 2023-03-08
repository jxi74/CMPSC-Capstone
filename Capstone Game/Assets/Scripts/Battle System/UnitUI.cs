using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UnitUI : MonoBehaviour
{
    private Unit _unit;
    
    public Slider unitHp;
    public Slider unitSta;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitHptext;
    public TextMeshProUGUI unitStatext;
    
    //Set Data
    public void Setdata(Unit unit)
    {
        _unit = unit;
        
        //Initialize Stats
        unitHp.maxValue = unit.MaxHealth;
        unitHp.value = unit.HP;
        unitHptext.text = unit.HP + "/" + unit.MaxHealth;
        unitSta.maxValue = unit.MaxStamina;
        unitSta.value = unit.STA;
        unitStatext.text = unit.STA + "/" + unit.MaxStamina;
        unitName.text = unit.Base.Name;
        
        
    }

   

    

    public IEnumerator UpdateHpBar()
    {
        float newhp = _unit.HP;
        float currenthp = unitHp.value;
        float changeamt = (currenthp - newhp);

        if (_unit.HpChanged)
        {
            if (currenthp > newhp)
            {
                while (currenthp - newhp > Mathf.Epsilon)
                {
                    currenthp -= changeamt * Time.deltaTime;
                    unitHptext.text = Mathf.FloorToInt(currenthp) + "/" + _unit.MaxHealth;
                    unitHp.value = currenthp;
                    yield return null;
                }
            }
            else
            {
                while (newhp - currenthp > Mathf.Epsilon)
                {
                    currenthp += newhp * Time.deltaTime;
                    unitHptext.text = Mathf.FloorToInt(currenthp) + "/" + _unit.MaxHealth;
                    unitHp.value = currenthp;
                    yield return null;
                }
            }
            unitHp.value = _unit.HP;
            unitHptext.text = _unit.HP + "/" + _unit.MaxHealth;
            _unit.HpChanged = false;
        }
        
    }
    
    public IEnumerator UpdateStaBar()
    {
        float newhp = _unit.STA;
        float currentsta = unitSta.value;
        float changeamt = (currentsta - newhp);

        if (_unit.StaChanged)
        {
            if (currentsta > newhp)
            {
                while (currentsta - newhp > Mathf.Epsilon)
                {
                    currentsta -= changeamt * Time.deltaTime;
                    unitStatext.text = Mathf.FloorToInt(currentsta) + "/" + _unit.MaxStamina;
                    unitSta.value = currentsta;
                    yield return null;
                }
            }
            else
            {
                while (newhp - currentsta > Mathf.Epsilon)
                {
                    currentsta += newhp * Time.deltaTime;
                    unitStatext.text = Mathf.FloorToInt(currentsta) + "/" + _unit.MaxStamina;
                    unitSta.value = currentsta;
                    yield return null;
                }
            }
            unitSta.value = _unit.STA;
            unitStatext.text = _unit.STA + "/" + _unit.MaxStamina;
            _unit.StaChanged = false;
        }
        
        
        
        
    }

}