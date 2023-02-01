using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UnitUI : MonoBehaviour
{
    public Slider unitHp;
    public Slider unitSta;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitHptext;
    public TextMeshProUGUI unitStatext;
    
    //Set Data
    public void Setdata(Unit unit)
    {
        //Initialize Stats
        unitHp.maxValue = unit.MaxHealth;
        unitHp.value = unit.HP;
        unitHptext.text = unit.HP + "/" + unit.MaxHealth;
        unitSta.maxValue = unit.Stamina;
        unitSta.value = unit.Stamina;
        unitStatext.text = unit.Stamina + "/" + unit.Stamina;
        unitName.text = unit.Base.Name;
        
        
    }
    
    //Set hp/sta
    void setbar(Slider bar, TextMeshProUGUI text, int val)
    {
        bar.value = val;
    }
}