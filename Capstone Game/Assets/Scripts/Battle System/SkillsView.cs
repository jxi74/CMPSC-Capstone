using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillsView : MonoBehaviour
{
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillType;
    public TextMeshProUGUI skillPower;
    public TextMeshProUGUI skillAccuracy;
    public TextMeshProUGUI skillStamina;
    
    public void SetData(Skill skill)
    {
        skillName.text = skill.Base.Name;
        skillType.text = skill.Base.Type.ToString();
        skillPower.text = skill.Power.ToString();
        skillAccuracy.text = skill.Accuracy.ToString();
        skillStamina.text = skill.StaminaCost.ToString();
    }

    public void Empty()
    {
        skillName.text = "";
        skillType.text = "";
        skillPower.text = "";
        skillAccuracy.text = "";
        skillStamina.text = "";
    }
}
