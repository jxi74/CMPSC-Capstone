using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_data : MonoBehaviour
{
    
    public string unit_name;

    [SerializeField] private Healthbar _healthbar;
    public int max_hp;
    public int current_hp;

    public int atk;
    public int def;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _healthbar.updatemax(max_hp);
        update_health(current_hp);
        _healthbar.setName(unit_name);
    }
    
    //Update UI health bar and player health
    public void update_health(int hp)
    {
        current_hp = hp;
        _healthbar.updatehp(hp);
    }
    

}
