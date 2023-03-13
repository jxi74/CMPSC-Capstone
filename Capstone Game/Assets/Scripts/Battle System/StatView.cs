using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    public Slider unitHp;
    public Slider unitSta;
    public Slider unitXp;
    public TextMeshProUGUI unitHptext;
    public TextMeshProUGUI unitStatext;
    public TextMeshProUGUI unitXptext;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitLevel;
    public TextMeshProUGUI unitStatuses;
    public List<TextMeshProUGUI> unitStats;

    public void SetData(Unit unit)
    {
        int currLevelxp = unit.Base.GetExpForLevel(unit.Level);
        int nextLevelxp = unit.Base.GetExpForLevel(unit.Level + 1);
        
        //Initialize Stats
        unitHp.maxValue = unit.MaxHealth;
        unitHp.value = unit.HP;
        unitHptext.text = unit.HP + "/" + unit.MaxHealth;
        unitSta.maxValue = unit.MaxStamina;
        unitSta.value = unit.STA;
        unitStatext.text = unit.STA + "/" + unit.MaxStamina;
        unitXp.maxValue = nextLevelxp - currLevelxp;
        unitXp.value = unit.Exp - currLevelxp;
        unitXptext.text = $"{unit.Exp - currLevelxp} / {nextLevelxp - currLevelxp}";
        unitName.text = unit.Base.Name;
        unitLevel.text = $"Lv {unit.Level}";
        unitStatuses.text = unit.Status?.ToString() ?? "Healthy";
        //Debug.Log($"Unit Status name: {unit.Status.Name}");
        

        unitStats[0].text = (unit.Attack).ToString();
        unitStats[1].text = (unit.Flux).ToString();
        unitStats[2].text = (unit.Defense).ToString();
        unitStats[3].text = (unit.Resistance).ToString();
        unitStats[4].text = (unit.Luck).ToString();
        unitStats[5].text = (unit.Speed).ToString();

    }
}
