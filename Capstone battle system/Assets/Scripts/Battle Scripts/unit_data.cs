using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_data : MonoBehaviour
{
    [SerializeField] private unithud hud;
    
    
    //initialize unit stats
    //robust system, import stats from battle initializer instead -> later
    public string unit_name;
    public int lv;
    public int max_hp;
    public int current_hp;
    public int atk;
    public int flx;
    public int def;
    public int res;
    public int lck;
    public int spd;
    public int sta;
    
    //implement skills later
    //initialize data
    void Start()
    {
        //update healthbar and data
        hud.updatemax(max_hp);
        update_health(current_hp);
        hud.setName(unit_name, lv);
    }

    public void update_health(int hp)
    {
        current_hp = hp;
        hud.updatehp(hp);
    }
}
