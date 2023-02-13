using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//

public class unithud : MonoBehaviour
{
    private Camera cam;
    
    public TextMeshProUGUI entity_name;
    public TextMeshProUGUI level;
    public Slider ui_max_hp;
    public Slider ui_current_hp;

    void Start()
    {
        
        cam = Camera.main;
    }

    public void setName(string name, int lv)
    {
        entity_name.text = name;
        level.text = ("Lv " + lv);
    }
    
    //use as initializer for health
    public void updatemax(int hp)
    {
        ui_max_hp.maxValue = (float)hp;
        ui_max_hp.value = (float)hp;
        ui_current_hp.maxValue = (float)hp;

    }
    
    
    public void updatehp(int hp)
    {
        ui_current_hp.value = (float)hp;
        
    }

    void Update()
    {
        //HUD follows camera
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

}
