using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltiptext;
    private Text tooltip;
    // Start is called before the first frame update
    void Start()
    {
        tooltip = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }
    public void GenerateTooltip(ItemBase item)
    {
        string statText = "";
        
        string tooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>",
               item.name, item.description, statText);
        tooltiptext.text = tooltip;
        gameObject.SetActive(true);
    }
}
