using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Units", menuName = "Units/Create unit")]

public class UnitBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private GameObject model;
    [SerializeField] private Type type1;
    [SerializeField] private Type type2;

    //stats
    [SerializeField] private int max_hp;
    [SerializeField] private int atk;
    [SerializeField] private int flx;
    [SerializeField] private int def;
    [SerializeField] private int res;
    [SerializeField] private int lck;
    [SerializeField] private int spd;
    [SerializeField] private int sta;

    //learnable moves
    [SerializeField] private List<LearnableSkill> learnableSkills;
    //grab value functions
    public string Name
    {
        get { return name; }
    }
    
    public string Description
    {
        get { return description; }
    }

    public GameObject Model
    {
        get { return model;  }
    }

    public Type Type1
    {
        get { return type1; }
    }

    public Type Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return max_hp; }
    }

    public int Atk
    {
        get { return atk; }
    }
    
    public int Flx
    {
        get { return flx; }
    }
    
    public int Def
    {
        get { return def; }
    }
    
    public int Res
    {
        get { return res; }
    }
    
    public int Lck
    {
        get { return lck; }
    }
    
    public int Spd
    {
        get { return spd; }
    }
    
    public int Sta
    {
        get { return sta; }
    }

    public List<LearnableSkill> LearnableSkills
    {
        get { return learnableSkills; }
    }
    
    [System.Serializable]
    //Learnable skills
    public class LearnableSkill
    {
        [SerializeField] private MoveBase movebase;
        [SerializeField] private int level;

        public MoveBase Base
        {
            get { return new MoveBase(); }
        }

        public int Level
        {
            get { return level; }
        }
    }
    
    //Types
    public enum Type
    {
        None,
        Earth,
        Water,
        Wind,
        Fire,
        Thunder,
        Ice,
        Force,
        Neutral,
        Flower,
        Shadow,
        Light,
        Moon
    }
}
