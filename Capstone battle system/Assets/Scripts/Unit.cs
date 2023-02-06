using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public UnitBase Base { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public int STA { get; set; }

    public List<Skill> Skills { get; set; }
    
    public Unit(UnitBase ubase, int ulevel)
    {
        Base = ubase;
        Level = ulevel;
        HP = MaxHealth;
        STA = MaxStamina;
        
        Skills = new List<Skill>();
        
        //Move generator
        foreach (var skill in Base.LearnableSkills)
        {
            if (skill.level <= Level)
            {
                //Debug.Log(skill.level);
                //Debug.Log(skill.Base.Name);
                Skills.Add(new Skill(skill.Base));
            }
        }
    }

    

    public int MaxHealth
    {
        get { return Mathf.FloorToInt(2 * (Base.Atk * Level) / 100f) + Level + 10; }
    }
    
    public int Attack
    {
        get { return Mathf.FloorToInt(2 * (Base.Atk * Level) / 100f) + 3; }
    }
    
    public int Flux
    {
        get { return Mathf.FloorToInt(2 * (Base.Flx * Level) / 100f) + 3; }
    }
    
    public int Defense
    {
        get { return Mathf.FloorToInt(2 * (Base.Def * Level) / 100f) + 3; }
    }

    public int Resistance
    {
        get { return Mathf.FloorToInt(2 * (Base.Res * Level) / 100f) + 3; }
    }
    
    public int Luck
    {
        get { return Mathf.FloorToInt(2 * (Base.Lck * Level) / 100f) + 3; }
    }
    
    public int Speed
    {
        get { return Mathf.FloorToInt(2 * (Base.Spd * Level) / 100f) + 3; }
    }
    
    public int MaxStamina
    {
        get { return Mathf.FloorToInt(2 * (Base.Sta * Level) / 100f) + 5; }
    }


    //Determine if fainted
    public bool TakeDamage(Skill skill, Unit attacker, bool guard)
    {
        float block = 1;
        if (guard)
        {
            block = 1.5f;
        }
        float modifier = Random.Range(.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * skill.Power * ((float)attacker.Attack / (Defense * block)) + 2;
        int dmg = Mathf.FloorToInt(d * modifier);

        HP -= dmg;

        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
        
        return false;
    }

    public void UseMove(Skill skill)
    {
        STA -= skill.StaminaCost;

        if (STA < 0)
        {
            STA = 0;
        }
    }

    public Skill GetRandomSKill()
    {
        int r = Random.Range(0, Skills.Count);
        return Skills[r];
    }

    public void Rest()
    {
        STA += Mathf.FloorToInt(STA * .15f + 4);
    }
}
