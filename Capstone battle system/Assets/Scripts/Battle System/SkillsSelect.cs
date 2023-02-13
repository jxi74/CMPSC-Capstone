using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillsSelect : MonoBehaviour
{
    

    [SerializeField] private List<Button> skillButtons;

    public void SetSkillNames(List<Skill> skills)
    {
        
        for (int i = 0; i < 6; i++)
        {
            if (i < skills.Count)
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skills[i].Base.name;
                skillButtons[i].interactable = true;
            }
            else
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                skillButtons[i].interactable = false;
            }
        }
        
    }
}
