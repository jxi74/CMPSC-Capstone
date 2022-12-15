using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Camera cam;
    
    public Slider ui_max_hp;
    public Slider ui_current_hp;

    void Start()
    {
        cam = Camera.main;
    }
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
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

}
