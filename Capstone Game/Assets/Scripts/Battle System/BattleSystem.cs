using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

//Battle States
public enum BattleState { Start, PlayerAction1, PlayerAction2, PerformSkills, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameController gamecontroller;
    //Battlestate
    private BattleState state;
    [SerializeField] public List<int> movequeue;
    [SerializeField] public List<int> targetlist;
    [SerializeField] public List<bool> defeatedUnits;

    //UI
    [SerializeField] private Canvas UI;
    [SerializeField] private BattleUI battleUI;
    [SerializeField] MessageBox box;
    [SerializeField] public SkillsSelect skillsSelect;

    //player units
    [SerializeField] public BattleUnit unit1;
    [SerializeField] public BattleUnit unit2;
    //enemy units
    [SerializeField] public BattleUnit unit3;
    [SerializeField] public BattleUnit unit4;
    [SerializeField] private List<UnitUI> unithuds;

    [SerializeField] List<BattleUnit> inBattleUnits;


    [SerializeField] public Party party;
    [SerializeField] public PartyHuds partyhuds;
    [SerializeField] public EnemyEncounter enemygenerator;

    public void StartBattle()
    {
        gamecontroller.inBattle(true);
        StartCoroutine(BattleSetup());
        UI.enabled = true;
        battleUI.SetupBattle();
    }

    void ResetTurn()
    {
        for (int i = 0; i < movequeue.Count; i++)
        {
            movequeue[i] = 0;
            targetlist[i] = 0;
            defeatedUnits[i] = false;
        }
    }

    public IEnumerator BattleSetup()
    {
        ResetTurn();
        state = BattleState.Start;
        unit1.hud = unithuds[0];
        unit2.hud = unithuds[1];
        unit3.hud = unithuds[2];
        unit4.hud = unithuds[3];
        unit1.Setup(party.GetFirstHealthyUnit(), true);
        unit2.Setup(party.GetNextHealthyUnitStart(party.GetFirstHealthyUnit()), true);
        unit3.Setup(unit3.Unit, false);
        unit4.Setup(enemygenerator.GetRandomUnit(), false);
        yield return new WaitForSeconds(.25f);
        unit1 = GameObject.FindWithTag("Unit1").GetComponent<BattleUnit>();
        unit2 = GameObject.FindWithTag("Unit2").GetComponent<BattleUnit>();
        unit3 = GameObject.FindWithTag("Unit3").GetComponent<BattleUnit>();
        unit4 = GameObject.FindWithTag("Unit4").GetComponent<BattleUnit>();

        inBattleUnits[0] = unit1;
        inBattleUnits[1] = unit2;
        inBattleUnits[2] = unit3;
        inBattleUnits[3] = unit4;
        
        yield return box.DisplayText($"A random {unit3.unitBase.Name} and {unit4.unitBase.Name} approaches!");
        yield return box.DisplayText(unit1.unitBase.Name + " and " + unit2.unitBase.Name + " went into action!");

        StartCoroutine(NewRound());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator NewRound()
    {
        ResetTurn();
        skillsSelect.SetTargetNames();
        if (unit1.Unit.HP == 0 && unit2.Unit.HP == 0)
        {
            //game over
            yield return box.DisplayText("You lose!");
            gamecontroller.inBattle(false);
            gamecontroller.Loss();
            UI.enabled = false;
        }
        else if (unit3.Unit.HP == 0 && unit4.Unit.HP == 0)
        {
            yield return box.DisplayText("You win!");
            gamecontroller.inBattle(false);
            gamecontroller.Victory();
            UI.enabled = false;
            //win
        }
        else
        {
            StartCoroutine(EnemyAction(3));
            StartCoroutine(EnemyAction(4));
            if (unit1.Unit.HP > 0)
            {
                //Debug.Log("Unit1 turn");
                PlayerAction(1);
            }
            else if (unit2.Unit.HP > 0)
            {
                //Debug.Log("Unit2 turn");
                PlayerAction(2);
            }
            else
            {
                yield return box.DisplayText("I don't really know what happened, but the game broke.. Ow.");
            }
        }
        
    }

    IEnumerator PerformMoves()
    {
        // Create a list of all units in the battle and sort them by speed.
        List<BattleUnit> allUnits = new List<BattleUnit> { unit1, unit2, unit3, unit4 };
        allUnits.Sort((a, b) => b.Unit.Speed.CompareTo(a.Unit.Speed));

        // Iterate over the units and perform their moves in turn.
        foreach (BattleUnit unit in allUnits)
        {
            if (unit == unit1 || unit == unit2)
            {
                // Perform the player's move.
                yield return StartCoroutine(PerformPlayerMove(unit == unit1 ? 1 : 2));
                
            }
            else
            {
                // Perform the enemy's move.
                yield return StartCoroutine(PerformEnemyMove(unit == unit3 ? 3 : 4));
                
            }
        }

        // Start a new round after all moves have been performed.
        yield return StartCoroutine(NewRound());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void PlayerAction(int unit)
    {
        BattleUnit unitcontrol;
        if (unit == 1)
        {
            state = BattleState.PlayerAction1;
            movequeue[0] = 0;
            unitcontrol = unit1;
            unithuds[0].GetComponentInChildren<Image>();
        }
        else
        {
            state = BattleState.PlayerAction2;
            movequeue[1] = 0;
            unitcontrol = unit2;
        }
        
        StartCoroutine(box.DisplayText("Select an action for " + unitcontrol.unitBase.Name));
        skillsSelect.SetSkillNames(unitcontrol.Unit.Skills);
        partyhuds.SetPartyNames();
        battleUI.SetDefaultButtons(true);
    }

    IEnumerator EnemyAction(int unit)
    {
        BattleUnit unitcontrol;
        int index;
        
        if (unit == 3)
        {
            //state = BattleState.EnemyAction1;
            unitcontrol = unit3;
            index = 2;
        }
        else
        {
            //state = BattleState.EnemyAction2;
            unitcontrol = unit4;
            index = 3;
        }
        
        if (new Random().Next(0,4) != 0)
        {
            movequeue[index] = new Random().Next(1, 7);
            targetlist[index] = new Random().Next(1, 3);
        }
        else
        {
            //HEY HEY!!!!
            //EVENTUALLY IMPLEMENT DISPLAY GUARD/REST AFTER UNIT 2, OR AT BATTLE BEGIN FOR ENEMIES
            
            
            //guard/rest
            movequeue[index] = new Random().Next(7, 9);
            if (movequeue[index] == 7)
            {
                //yield return box.DisplayText($"{unitcontrol.Unit.Base.name} readied their guard!");
            }
            else
            {
                //yield return box.DisplayText($"{unitcontrol.Unit.Base.name} rested up!");
                //unitcontrol.Unit.Rest();
                //yield return unithuds[index].UpdateStaBar();
            }
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator RunSkill(BattleUnit sourceUnit, BattleUnit targetUnit)
    {
        bool canRunSkill = sourceUnit.Unit.OnBeforeMove();
        if (!canRunSkill)
        {
            yield return ShowStatusChange(sourceUnit.Unit);
            yield break;
        }

        yield return ShowStatusChange(sourceUnit.Unit);
        
        var move = sourceUnit.Unit.GetRandomSKill();
        int index = inBattleUnits.IndexOf(sourceUnit);
        int targetindex = inBattleUnits.IndexOf(targetUnit);
        //check if alive and unit was not swapped
        if (sourceUnit.Unit.HP > 0 && !defeatedUnits[index])
        {
        
            //Only perform if skill
            if (movequeue[index] <= 6 && targetlist[index] > 0)
            {
                if (sourceUnit == unit1 || sourceUnit == unit2)
                {
                    move = sourceUnit.Unit.Skills[movequeue[index] - 1];
                }
                if (sourceUnit.Unit.STA < move.StaminaCost && (sourceUnit == unit3 || sourceUnit == unit4))
                {
                    
                    yield return box.DisplayText($"{sourceUnit.unitBase.Name} rested up!");
                    sourceUnit.Unit.Rest();
                    yield return sourceUnit.hud.UpdateStaBar();
                }
                else
                {
                    //Perform skill
                    yield return box.DisplayText(sourceUnit.unitBase.Name + $" used {move.Base.Name}");

                    if (CheckIfSkillHits(move, sourceUnit, targetUnit))
                    {
                        //Status move
                        if (move.Base.Category == SkillCategory.Status)
                        {
                        
                            if (targetUnit.Unit.HP <= 0)
                            {
                                yield return box.DisplayText(
                                    $"{targetUnit.Unit.Base.Name} is already defeated, the attack misses!");
                            }
                            else
                            {
                                GameObject.Find("GameController").GetComponent<GameController>().Skill(move.Base.Audio);
                                sourceUnit.Unit.UseMove(move);
                                StartCoroutine(sourceUnit.hud.UpdateStaBar());
                                yield return RunSkillEffects(move.Base.Effects, sourceUnit, targetUnit, move.Base.Target);
                            }
                        
                        }
                        //Regular attack
                        else
                        {
                            if (targetUnit.Unit.HP <= 0)
                            {
                                yield return box.DisplayText(
                                    $"{targetUnit.Unit.Base.Name} is already defeated, the attack misses!");
                                yield break;
                            }
                            else
                            {
                                bool guardCheck = (movequeue[targetindex] == 7);
                                var damageDetails = targetUnit.Unit.TakeDamage(move, sourceUnit.Unit, guardCheck);
                    
                                //Display UI changes/update values
                                GameObject.Find("GameController").GetComponent<GameController>().Skill(move.Base.Audio);
                                TriggerVFX(move, sourceUnit, targetUnit);
                                targetUnit.TakeDamage((int)(targetUnit.hud.unitHp.value - targetUnit.Unit.HP));
                                StartCoroutine(targetUnit.hud.UpdateHpBar());
                                StartCoroutine(sourceUnit.hud.UpdateStaBar());
                                
                                sourceUnit.Unit.UseMove(move);
                                
                                yield return ShowDamageDetails(damageDetails);
                                
                            }
                            
                            if (targetUnit.Unit.HP <= 0)
                            {
                                yield return HandleDefeatedUnit(targetUnit);
                            }
                        }

                        if (move.Base.Secondary != null && move.Base.Secondary.Count > 0 && targetUnit.Unit.HP > 0)
                        {
                            foreach (var secondary in move.Base.Secondary)
                            {
                                var chance = new Random().Next(1, 100);
                                if (chance <= secondary.Chance)
                                {
                                    yield return RunSkillEffects(secondary, sourceUnit, targetUnit, secondary.Target);
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        yield return box.DisplayText($"{sourceUnit.Unit.Base.Name} missed!");
                    }
                    
                }
                
            }

            
        }
    }

    public void TriggerVFX(Skill move, BattleUnit source, BattleUnit target)
    {
        GameObject vfx;
        if (move.Base.VFXTarget == SkillTargetVFX.Foe)
        {
            //Trigger only on foe
            vfx = Instantiate(move.Base.VFX, target.transform.position, Quaternion.identity);
        }
        else if (move.Base.VFXTarget == SkillTargetVFX.Self)
        {
            vfx = Instantiate(move.Base.VFX, source.transform.position, Quaternion.identity);
        }
        else
        {
            //FAILSAFE
            //Trigger only on foe
            vfx = Instantiate(move.Base.VFX, target.transform.position, Quaternion.identity);
        }
        

        // Destroy the VFX after its duration has passed
        ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
        Destroy(vfx, ps.main.duration);
    }
    
    IEnumerator RunSkillEffects(SkillEffects effects, BattleUnit sourceUnit, BattleUnit targetUnit, SkillTarget skillTarget)
    {
        //sourceUnit.Unit.UseMove(skill);
        //yield return sourceUnit.hud.UpdateStaBar();

        //Stat changes
        if (effects.Boosts != null)
        {
            if (skillTarget == SkillTarget.Self)
            {
                sourceUnit.Unit.ApplyBoost(effects.Boosts);
            }
            else
            {
                targetUnit.Unit.ApplyBoost(effects.Boosts);
            }
            
                            
        }

        //status effect
        if (effects.Status != ConditionID.None)
        {
            Debug.Log("Run status effect");
            targetUnit.Unit.SetStatus(effects.Status);
        }
        
        yield return ShowStatusChange(sourceUnit.Unit);
        yield return ShowStatusChange(targetUnit.Unit);
        
    }
    
    IEnumerator RunAfterTurn(int index)
    {
        index -= 1;
        BattleUnit sourceUnit = inBattleUnits[index];
        //index = inBattleUnits.IndexOf(sourceUnit);
        int tindex = targetlist[index];
        
        //Negate performing effect if unit dead
        if (inBattleUnits[index].Unit.HP <= 0)
        {
            yield break;
        }
        //Status effect checks
        //Debug.Log($"{sourceUnit.Unit.Base.Name} health before status: " + sourceUnit.Unit.HP);
        sourceUnit.Unit.OnAfterTurn();
        yield return StartCoroutine(ShowStatusChange(sourceUnit.Unit));
        if (sourceUnit.hud.unitHp.value != sourceUnit.Unit.HP)
        {
            sourceUnit.TakeDamage((int)(sourceUnit.hud.unitHp.value - sourceUnit.Unit.HP));
        }
        yield return sourceUnit.hud.UpdateHpBar();
        yield return sourceUnit.hud.UpdateStaBar();
        //Debug.Log($"{sourceUnit.Unit.Base.Name} health after status: " + sourceUnit.Unit.HP);
        if (sourceUnit.Unit.HP <= 0)
        {
            HandleDefeatedUnit(sourceUnit);
        }
    }

    bool CheckIfSkillHits(Skill skill, BattleUnit sourceUnit, BattleUnit targetUnit)
    {
        if (skill.Base.AlwaysHits)
        {
            return true;
        }
        
        float skillAccuracy = skill.Accuracy;

        int accuracy = sourceUnit.Unit.StatBoosts[UnitBase.Stat.Accuracy];
        int evasion = targetUnit.Unit.StatBoosts[UnitBase.Stat.Evasion];
        
        var boostValues = new float[] { 1f, 4f / 3f, 5f / 3f, 2f,  7f/3f, 8f/3f, 3f};

        if (accuracy > 0)
        {
            skillAccuracy *= boostValues[accuracy];
        }
        else
        {
            skillAccuracy /= boostValues[-accuracy];
        }
        
        if (evasion > 0)
        {
            skillAccuracy /= boostValues[evasion];
        }
        else
        {
            skillAccuracy *= boostValues[-evasion];
        }
        return (new Random().Next(1, 101) <= skillAccuracy);
    }
    
    IEnumerator ShowStatusChange(Unit unit)
    {
        while (unit.StatusChanges.Count > 0)
        {
            var msg = unit.StatusChanges.Dequeue();
            yield return box.DisplayText(msg);
        }
    }

    IEnumerator HandleDefeatedUnit(BattleUnit defeatedUnit)
    {
        yield return box.DisplayText($"{defeatedUnit.Unit.Base.Name} was defeated!");
        defeatedUnits[inBattleUnits.IndexOf(defeatedUnit)] = true;
        yield return CheckForSwap(defeatedUnit);

        if (!defeatedUnit.isPlayer)
        {
            //Gain xp
            int expGain = defeatedUnit.Unit.Base.ExpGain;
            int unitLevel = defeatedUnit.Unit.Level;

            int expEarned = Mathf.FloorToInt(expGain * unitLevel) / 7;

            BattleUnit unit1 = inBattleUnits[0];
            BattleUnit unit2 = inBattleUnits[1];
                
            unit1.Unit.Exp += expEarned;
            yield return box.DisplayText($"{unit1.Unit.Base.Name} and {unit2.Unit.Base.Name} gained {expEarned} exp!");
            unit2.Unit.Exp += expEarned;
            
            //check level up
            while (unit1.Unit.CheckLevelUp())
            {
                yield return box.DisplayText($"{unit1.Unit.Base.Name} leveled up to {unit1.level}!");
            }
            while (unit2.Unit.CheckLevelUp())
            {
                yield return box.DisplayText($"{unit2.Unit.Base.Name} leveled up to {unit2.level}!");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CheckForSwap(BattleUnit defeatedUnit)
    {
        if (defeatedUnit.isPlayer)
        {
            var nextUnit = party.GetNextHealthyUnit(defeatedUnit.Unit);
            while (nextUnit == unit1.Unit || nextUnit == unit2.Unit)
            {
                Debug.Log("nextUnit same as unit1/unit2.. selecting next");
                nextUnit = party.GetNextHealthyUnit(nextUnit);
                if (nextUnit == null)
                {
                    Debug.Log("Next null... no alive party unit, skipping swap");
                    break;
                }
            }

            if (nextUnit != null)
            {
                int tag = inBattleUnits.IndexOf(defeatedUnit);
                defeatedUnit.Setup(nextUnit, true);
                inBattleUnits[tag] = GameObject.FindWithTag($"Unit{tag + 1}").GetComponent<BattleUnit>();
                                
                defeatedUnit.hud.Setdata(nextUnit);

                GameObject.Find("GameController").GetComponent<GameController>().UnitSwap();
                yield return StartCoroutine(box.DisplayText($"{defeatedUnit.Unit.Base.Name} rises to the challenge!"));
                
                
            }
            else
            {
                Debug.Log("No more healthy units left in the party");
                // Handle the case where there are no more healthy units left in the party
            }
        }
    }
    
    IEnumerator PerformPlayerMove(int unit)
    {
        BattleUnit unitcontrol;
        BattleUnit targetUnit;
        int index;
        int target;
        if (unit == 1)
        {
            unitcontrol = unit1;
            index = 0;
        }
        else
        {
            unitcontrol = unit2;
            index = 1;
        }

        target = targetlist[index];
        if (target != 0)
        {
            targetUnit = inBattleUnits[target - 1];
            yield return RunSkill(unitcontrol, targetUnit);
            yield return RunAfterTurn(unit);
        }
        
        
    }

    IEnumerator PerformEnemyMove(int unit)
    {
        BattleUnit unitcontrol;
        BattleUnit targetUnit;
        int index;
        int target;
        if (unit == 3)
        {
            unitcontrol = unit3;
            index = 2;
        }
        else
        {
            unitcontrol = unit4;
            index = 3;
        }
        
        target = targetlist[index];
        if (target != 0)
        {
            targetUnit = inBattleUnits[target - 1];
            yield return RunSkill(unitcontrol, targetUnit);
            yield return RunAfterTurn(unit);
        }
        
        
    }

    IEnumerator ShowDamageDetails(Unit.DamageDetails details)
    {
        if (details.Critical > 1f)
        {
            yield return box.DisplayText("Lucky hit!");
        }

        if (details.Effectiveness > 1f)
        {
            yield return box.DisplayText("A powerful attack!");
        }
        else if (details.Effectiveness < 1f)
        {
            yield return box.DisplayText("A weaker attack..");
        }
        
    }

    private IEnumerator PerformBattle()
    {
        state = BattleState.PerformSkills;
        yield return new WaitForSeconds(1f);
        StartCoroutine(PerformMoves());
        
    }

    public int buttonval;

    //Skill button press handler during player turn
    public void ButtonPress(int button)
    {
        buttonval = button;
        //StartCoroutine(ButtonPress2(button));
    }
    public void ActionPress(int button)
    {
        //Guard or Rest
        StartCoroutine(ButtonPress2(button, 0));
    }
    
    
    public void SelectTarget(int target)
    {
        StartCoroutine(ButtonPress2(buttonval, target));
    }

    public IEnumerator ButtonPress2(int button, int target)
    {
        int index;
        BattleUnit unit;
        String movename;

        if (state == BattleState.PlayerAction1)
        {
            index = 0;
            unit = unit1;
        }
        else
        {
            index = 1;
            unit = unit2;
        }

        //Skill (1-6) or Guard/Rest (7/8)
        movequeue[index] = button;
        battleUI.SetDefaultButtons(false);
        battleUI.SetActionButtons(false);
        battleUI.SetSelectButtons(false);

        if (button <= 6)
        {
            var move = unit.Unit.Skills[button - 1];

            if (unit.Unit.STA < move.StaminaCost)
            {
                yield return box.DisplayText($"{move.Base.Name} costs too much stamina to use!");
                state = BattleState.Busy;
            }
            else
            {
                movename = unit.Unit.Skills[button - 1].Base.Name;
                yield return box.DisplayText(unit.unitBase.Name + " readied up with " + movename + "!");
                targetlist[index] = target;
            }
        }
        else if (button == 7)
        {
            //Guard
            yield return box.DisplayText(unit.unitBase.Name + " readied their guard!");
            targetlist[index] = 0;
        }
        else if (button == 8)
        {
            //Rest
            yield return box.DisplayText(unit.unitBase.Name + " rested up!");
            unit.Unit.Rest();
            yield return unit.hud.UpdateStaBar();
            targetlist[index] = 0;
        }
        else if (button == 10)
        {
            //swap unit
            if (unit1.Unit == party.units[buttonval - 1] || unit2.Unit == party.units[buttonval - 1])
            {
                yield return box.DisplayText($"You cannot swap with already in-battle units!");
                state = BattleState.Busy;
                //reset
            }
            else
            {
                GameObject.Find("GameController").GetComponent<GameController>().UnitSwap();
                int tag = inBattleUnits.IndexOf(unit);
                //defeatedUnit.Setup(nextUnit, true);
                yield return box.DisplayText($"{unit.unitBase.Name} fell back and swapped with {party.units[buttonval - 1].Base.Name}!");
                //Update swaps
                skillsSelect.SetTargetNames();
                unit.Setup(party.units[buttonval - 1], true);
                inBattleUnits[tag] = GameObject.FindWithTag($"Unit{tag + 1}").GetComponent<BattleUnit>();
                unit.hud.Setdata(party.units[buttonval - 1]);
                
            }
            

        }
        if (button == 9)
        {
            //Escape
            int chance = (((unit.Unit.Speed) * 64) / ((unit3.Unit.Speed + unit2.Unit.Speed) / 2) + 30 % 256);
            int val = new Random().Next(1, 257);
            if (chance >= val)
            {
                //Success
                yield return box.DisplayText("Successfully escaped!");
                gamecontroller.inBattle(false);
                UI.enabled = false;
            }
            else
            {
                yield return box.DisplayText("Escape was unsuccessful!");
                if (state == BattleState.PlayerAction1)
                {
                    if (unit2.Unit.HP > 0)
                    {
                        PlayerAction(2);
                    }
                    else
                    {
                        StartCoroutine(PerformBattle());
                    }
                }
                else if (state == BattleState.PlayerAction2)
                {
                    StartCoroutine(PerformBattle());
                }
                else
                {
                    PlayerAction(index + 1);
                }
            }
        }
        else
        {
            if (state == BattleState.PlayerAction1)
            {
                if (unit2.Unit.HP > 0)
                {
                    PlayerAction(2);
                }
                else
                {
                    StartCoroutine(PerformBattle());
                }
            }
            else if (state == BattleState.PlayerAction2)
            {
                StartCoroutine(PerformBattle());
            }
            else
            {
                PlayerAction(index + 1);
            }
        }
        
    }



}

