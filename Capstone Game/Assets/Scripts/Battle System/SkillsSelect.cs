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
    [SerializeField] private List<Button> targetButtons;
    [SerializeField] private List<Material> typeColor;
    public void SetSkillNames(List<Skill> skills)
    {
        Color color = Color.black;
        for (int i = 0; i < 6; i++)
        {
            if (i < skills.Count)
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skills[i].Base.name;
                switch (skills[i].Base.Type)
                {
                    case UnitBase.Type.Earth:
                        color = typeColor[0].color;
                        break;
                    case UnitBase.Type.Fire:
                        color = typeColor[1].color;
                        break;
                    case UnitBase.Type.Flower:
                        color = typeColor[2].color;
                        break;
                    case UnitBase.Type.Force:
                        color = typeColor[3].color;
                        break;
                    case UnitBase.Type.Ice:
                        color = typeColor[4].color;
                        break;
                    case UnitBase.Type.Light:
                        color = typeColor[5].color;
                        break;
                    case UnitBase.Type.Moon:
                        color = typeColor[6].color;
                        break;
                    case UnitBase.Type.Neutral:
                        color = typeColor[7].color;
                        break;
                    case UnitBase.Type.Shadow:
                        color = typeColor[8].color;
                        break;
                    case UnitBase.Type.Thunder:
                        color = typeColor[9].color;
                        break;
                    case UnitBase.Type.Water:
                        color = typeColor[10].color;
                        break;
                    case UnitBase.Type.Wind:
                        color = typeColor[11].color;
                        break;
                }

                ColorBlock buttonColor = skillButtons[i].colors;
                buttonColor.normalColor = color;
                buttonColor.highlightedColor = color;
                skillButtons[i].colors = buttonColor;
                
                skillButtons[i].interactable = true;
            }
            else
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                skillButtons[i].interactable = false;
            }
        }
        
    }

    public void SetTargetNames()
    {
        BattleSystem battlesystem = FindObjectOfType<BattleSystem>();
        
        //Eventually gray out defeated unswapped units
        targetButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = battlesystem.unit1.unitBase.Name;
        targetButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = battlesystem.unit2.unitBase.Name;
        targetButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = battlesystem.unit3.unitBase.Name;
        targetButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = battlesystem.unit4.unitBase.Name;
    }
    
    
}
