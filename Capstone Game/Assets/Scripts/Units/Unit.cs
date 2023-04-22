using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[System.Serializable]
public class Unit
{
    [SerializeField] private UnitBase _base;
    [SerializeField] private int level;
    [SerializeField] private int hp;
    [SerializeField] private int sta;
    [SerializeField] private int experience;
    

    public UnitBase Base
    {
        get
        {
            return _base;
        } 
    }

    public int Level
    {
        get
        {
            return level;
        }
        set => level = value;
    }

    public int HP
    {
        get
        {
            return hp;
        }
        set => hp = value;
    }

    public int STA
    {
        get
        {
            return sta;
        }
        set => sta = value;
    }

    public int Experience
    {
        get
        {
            return experience;
        }
        set => experience = value;
    }
    
    public List<Skill> Skills { get; set; }
    public Dictionary<UnitBase.Stat, int> Stats { get; private set; }
    public Dictionary<UnitBase.Stat, int> StatBoosts { get; private set; }
    public Effect Status { get; set; }
    public int StatusTime { get; set; }
    
    public bool HpChanged { get; set; }
    public bool StaChanged { get; set; }
    
    public Queue<string> StatusChanges { get; private set; } = new Queue<string>();
    
    public void Init()
    {
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

        Experience = Base.GetExpForLevel(level);
        CalcStats();
        hp = MaxHealth;
        sta = MaxStamina;
        
        ResetStatBoost();
        // Add debug statement to check status after initialization
    }

    public void ResetStatBoost()
    {
        StatBoosts = new Dictionary<UnitBase.Stat, int>()
        {
            { UnitBase.Stat.Attack, 0 },
            { UnitBase.Stat.Defense , 0},
            { UnitBase.Stat.Flux , 0},
            { UnitBase.Stat.Resistance , 0},
            { UnitBase.Stat.Luck , 0},
            { UnitBase.Stat.Speed , 0},
            { UnitBase.Stat.Accuracy , 0},
            { UnitBase.Stat.Evasion , 0},
        };
    }

    public void CalcStats()
    {
        Stats = new Dictionary<UnitBase.Stat, int>();
        Stats.Add(UnitBase.Stat.Attack, Mathf.FloorToInt(2 * (Base.Atk * Level) / 100f) + 3);
        Stats.Add(UnitBase.Stat.Defense, Mathf.FloorToInt(2 * (Base.Def * Level) / 100f) + 3);
        Stats.Add(UnitBase.Stat.Flux, Mathf.FloorToInt(2 * (Base.Flx * Level) / 100f) + 3);
        Stats.Add(UnitBase.Stat.Resistance, Mathf.FloorToInt(2 * (Base.Res * Level) / 100f) + 3);
        Stats.Add(UnitBase.Stat.Luck, Mathf.FloorToInt(2 * (Base.Lck * Level) / 100f) + 3);
        Stats.Add(UnitBase.Stat.Speed, Mathf.FloorToInt(2 * (Base.Spd * Level) / 100f) + 3);


        MaxHealth = Mathf.FloorToInt(2 * (Base.MaxHp * Level) / 100f) + Level + 10;;
        MaxStamina = Mathf.FloorToInt(2 * (Base.Sta * Level) / 100f) + 5;
    }

    int GetStat(UnitBase.Stat stat)
    {
        int statVal = Stats[stat];

        //Stat boost calc
        int boost = StatBoosts[stat];
        var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        if (boost >= 0)
        {
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        }
        else
        {
            statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
        }
        
        return statVal;
    }

    
    public void ApplyBoost(List<StatBoost> statBoosts)
    {
        foreach (var statBoost in statBoosts)
        {
            var stat = statBoost.stat;
            var boost = statBoost.boost;
            StatBoosts[stat] = Mathf.Clamp(StatBoosts[stat] + boost, -6, 6);

            if (boost > 0)
            {
                StatusChanges.Enqueue($"{Base.Name}'s {stat} increased!");
            }
            else
            {
                StatusChanges.Enqueue($"{Base.Name}'s {stat} decreased!");
            }
            
            Debug.Log($"{stat} has been boosted to {StatBoosts[stat]}");
        }

        
    }

    public bool CheckLevelUp()
    {
        if (Experience > Base.GetExpForLevel(level + 1))
        {
            ++level;
            return true;
        }

        return false;
    }
    

    public int MaxHealth { get; set; }
    
    public int MaxStamina { get; set; }
    
    public int Attack { get { return GetStat(UnitBase.Stat.Attack); } }
    
    public int Flux { get { return GetStat(UnitBase.Stat.Flux); } }
    
    public int Defense { get { return GetStat(UnitBase.Stat.Defense); } }

    public int Resistance { get { return GetStat(UnitBase.Stat.Resistance); } }
    
    public int Luck { get { return GetStat(UnitBase.Stat.Luck); } }
    
    public int Speed { get { return GetStat(UnitBase.Stat.Speed); } }
    
    public void DecreaseHp(int dmg)
    {
        HP = Mathf.Clamp(HP - dmg, 0, MaxHealth);
        HpChanged = true;
    }
    
    public void IncreaseHp(int dmg)
    {
        HP = Mathf.Clamp(HP + dmg, 0, MaxHealth);
        HpChanged = true;
    }
    
    public void DecreaseSTA(int dmg)
    {
        STA = Mathf.Clamp(STA - dmg, 0, MaxStamina);
        StaChanged = true;
    }
    
    public void IncreaseSTA(int dmg)
    {
        STA = Mathf.Clamp(STA + dmg, 0, MaxStamina);
        StaChanged = true;
    }

    //Determine if fainted
    public DamageDetails TakeDamage(Skill skill, Unit attacker, bool guard)
    {
        float type = UnitBase.TypeChart.GetEffectiveness(skill.Base.Type, this.Base.Type1) * UnitBase.TypeChart.GetEffectiveness(skill.Base.Type, this.Base.Type2);
        float critical = 1;
        float block = 1;
        if (Random.value * 100f <= attacker.Luck)
        {
            critical = 2f;
        }
        if (guard)
        {
            block = 1.5f;
        }

        var damageDetails = new DamageDetails()
        {
            Effectiveness = type,
            Critical = critical,
            Fainted = false
        };

        float attack = skill.Base.Category == SkillCategory.Special ? attacker.Flux : attacker.Attack;
        float defense = skill.Base.Category == SkillCategory.Special ? Resistance : Defense;
        
        float modifier = Random.Range(.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * skill.Power * ((float)attack / (defense * block)) + 2;
        int dmg = Mathf.FloorToInt(d * modifier);

        HP -= dmg;
        
        DecreaseHp(dmg);
        
        return damageDetails;
    }

    public void UseMove(Skill skill)
    {
        DecreaseSTA(skill.StaminaCost);
    }

    public void CureStatus()
    {
        Status = null;
    }
    
    public bool OnBeforeMove()
    {
        if (Status?.OnBeforeTurn != null)
        {
            return Status.OnBeforeTurn(this);
        }
        return true;
    }
    
    public void OnAfterTurn()
    {
        Status?.OnAfterTurn?.Invoke(this);
    }

    public void SetStatus(ConditionID conditionid)
    {
        Status = ConditionsDB.Conditions[conditionid];
        Status?.OnStart?.Invoke(this);
        StatusChanges.Enqueue($"{Base.Name} {Status.StartMsg}");
    }
    
    public Skill GetRandomSKill()
    {
        int r = Random.Range(0, Skills.Count);
        return Skills[r];
    }

    public void Rest()
    {
        IncreaseSTA(Mathf.FloorToInt(MaxStamina * .15f + Random.Range(0,2)));
    }

    public void OnBattleOver()
    {
        ResetStatBoost();
    }

    public class DamageDetails
    {
        public bool Fainted { get; set; }
        public float Critical { get; set; }
        public float Effectiveness { get; set; }
        
    }
}
