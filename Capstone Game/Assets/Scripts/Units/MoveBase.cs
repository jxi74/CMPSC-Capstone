using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Skills", menuName = "Units/Create skill")]

public class MoveBase : ScriptableObject
{
    [SerializeField] public new string name;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private UnitBase.Type type;
    [SerializeField] public int power;
    [SerializeField] public int accuracy;
    [SerializeField] public bool alwaysHits;
    [SerializeField] private SkillCategory category;
    [SerializeField] private SkillEffects effects;
    [SerializeField] private List<SecondaryEffects> secondaryEffects;
    [SerializeField] private SkillTarget target;

    [SerializeField] private GameObject vfx;
    [SerializeField] private SkillTargetVFX vfxTarget;
    [SerializeField] private AudioClip sfx;
    [FormerlySerializedAs("stamina_cost")] [SerializeField] public int staminaCost;
    


    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public UnitBase.Type Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }
    
    public bool AlwaysHits
    {
        get { return alwaysHits; }
    }

    public int Stamina
    {
        get { return staminaCost; }
    }

    public SkillCategory Category
    {
        get { return category; }
    }

    public SkillEffects Effects
    {
        get { return effects; }
    }

    public List<SecondaryEffects> Secondary
    {
        get { return secondaryEffects; }
    }

    public SkillTarget Target
    {
        get { return target; }
    }

    public AudioClip Audio
    {
        get { return sfx; }
    }
    
    public GameObject VFX
    {
        get { return vfx; }
    }
    
    public SkillTargetVFX VFXTarget
    {
        get { return vfxTarget; }
    }
}

[System.Serializable]
public class SkillEffects
{
    [SerializeField] List<StatBoost> boosts;
    [SerializeField] private ConditionID status;

    public List<StatBoost> Boosts
    {
        get { return boosts; }
    }

    public ConditionID Status
    {
        get { return status; }
    }
    
}

[System.Serializable]
public class SecondaryEffects : SkillEffects
{
    [SerializeField] private int chance;
    [SerializeField] private SkillTarget target;

    public int Chance
    {
        get { return chance; }
    }

    public SkillTarget Target
    {
        get { return target; }
    }
}

[System.Serializable]
public class StatBoost
{
    public UnitBase.Stat stat;
    public int boost;

}

public enum SkillCategory
{
    Physical,
    Special,
    Status
}

public enum SkillTarget
{
    Foe, Self
}

public enum SkillTargetVFX
{
    Foe, Self, SelfToFoe
}

