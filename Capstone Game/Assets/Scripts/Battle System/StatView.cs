using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    public Slider unitHp;
    public Slider unitSta;
    public TextMeshProUGUI unitHptext;
    public TextMeshProUGUI unitStatext;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitLevel;
    public TextMeshProUGUI unitStatuses;
    public List<TextMeshProUGUI> unitStats;
    
    public void SetData(Unit unit)
    {
        //Initialize Stats
        unitHp.maxValue = unit.MaxHealth;
        unitHp.value = unit.HP;
        unitHptext.text = unit.HP + "/" + unit.MaxHealth;
        unitSta.maxValue = unit.MaxStamina;
        unitSta.value = unit.STA;
        unitStatext.text = unit.STA + "/" + unit.MaxStamina;
        unitName.text = unit.Base.Name;
        unitLevel.text = $"Lv {unit.Level}";
        unitStatuses.text = "Implement status display";

        unitStats[0].text = (unit.Attack).ToString();
        unitStats[1].text = (unit.Flux).ToString();
        unitStats[2].text = (unit.Defense).ToString();
        unitStats[3].text = (unit.Resistance).ToString();
        unitStats[4].text = (unit.Luck).ToString();
        unitStats[5].text = (unit.Speed).ToString();

    }
}
