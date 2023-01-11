using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private UnitBase _base;
    private int level;
    
    public Unit(UnitBase ubase, int ulevel)
    {
        _base = ubase;
        level = ulevel;

        Skills = new List<Skill>();
        
    }

    public List<Skill> Skills { get; set; }

    public int MaxHealth
    {
        get { return Mathf.FloorToInt(2 * (_base.Atk * level) / 100f) + level + 10; }
    }
    
    public int Attack
    {
        get { return Mathf.FloorToInt(2 * (_base.Atk * level) / 100f) + 3; }
    }
    
    public int Flux
    {
        get { return Mathf.FloorToInt(2 * (_base.Flx * level) / 100f) + 3; }
    }
    
    public int Defense
    {
        get { return Mathf.FloorToInt(2 * (_base.Def * level) / 100f) + 3; }
    }
    
    public int Resistance
    {
        get { return Mathf.FloorToInt(2 * (_base.Res * level) / 100f) + 3; }
    }
    
    public int Luck
    {
        get { return Mathf.FloorToInt(2 * (_base.Lck * level) / 100f) + 3; }
    }
    
    public int Speed
    {
        get { return Mathf.FloorToInt(2 * (_base.Spd * level) / 100f) + 3; }
    }
    
    public int Stamina
    {
        get { return Mathf.FloorToInt(2 * (_base.Sta * level) / 100f) + 5; }
    }
}
