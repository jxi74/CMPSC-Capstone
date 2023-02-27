using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
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
    [SerializeField] UnitUI unit1hud;
    [SerializeField] public BattleUnit unit2;
    [SerializeField] UnitUI unit2hud;
    //enemy units
    [SerializeField] public BattleUnit unit3;
    [SerializeField] UnitUI unit3hud;
    [SerializeField] public BattleUnit unit4;
    [SerializeField] UnitUI unit4hud;
    [SerializeField] private List<UnitUI> unithuds;

    [SerializeField] List<BattleUnit> inBattleUnits;


    [SerializeField] public Party party;
    [SerializeField] public PartyHuds partyhuds;
    [SerializeField] public EnemyEncounter enemygenerator;

    public void StartBattle()
    {
        gamecontroller.inBattle(true);
        unithuds = new List<UnitUI> {unit1hud, unit2hud, unit3hud, unit4hud};
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

        unit1.Setup(party.GetFirstHealthyUnit(), true);
        unit1hud.Setdata(unit1.Unit);
        unit2.Setup(party.GetNextHealthyUnitStart(party.GetFirstHealthyUnit()), true);
        unit2hud.Setdata(unit2.Unit);
        unit3.Setup(enemygenerator.GetRandomUnit(), false);
        unit3hud.Setdata(unit3.Unit);
        unit4.Setup(enemygenerator.GetRandomUnit(), false);
        unit4hud.Setdata(unit4.Unit);

        inBattleUnits.Add(unit1);
        inBattleUnits.Add(unit2);
        inBattleUnits.Add(unit3);
        inBattleUnits.Add(unit4);
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
            yield return box.DisplayText("You lose :(");
            yield return box.DisplayText("Loser.");
            gamecontroller.inBattle(false);
            UI.enabled = false;
        }
        else if (unit3.Unit.HP == 0 && unit4.Unit.HP == 0)
        {
            yield return box.DisplayText("You win :)");
            gamecontroller.inBattle(false);
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

    // ReSharper disable Unity.PerformanceAnalysis
    void PlayerAction(int unit)
    {
        
        
        BattleUnit unitcontrol;
        if (unit == 1)
        {
            state = BattleState.PlayerAction1;
            movequeue[0] = 0;
            unitcontrol = unit1;
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
            //use skills
            //Eventually add stamina checks for enemy to reroll move
            movequeue[index] = new Random().Next(1, 7);
            targetlist[index] = new Random().Next(1, 3);
            //Debug.Log($"Enemy {index + 1}: {targetlist[index]}");
            //var move = unitcontrol.Unit.GetRandomSKill();
            
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

    IEnumerator PerformPlayerMove(int unit)
    {
        BattleUnit unitcontrol;
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

        //check if alive and unit was not swapped
        if (unitcontrol.Unit.HP > 0 && !defeatedUnits[index])
        {
        
            //Only perform if skill
            if (movequeue[index] <= 6 && targetlist[index] > 0)
            {
                target = targetlist[index];
                var move = unitcontrol.Unit.Skills[movequeue[index] - 1];
                yield return box.DisplayText(unitcontrol.unitBase.Name + $" used {move.Base.Name}");
                BattleUnit targetUnit = inBattleUnits[target - 1];
            
                //yield return unithuds[targetlist[index] - 1].UpdateStaBar();
                if (targetUnit.Unit.HP <= 0)
                {
                    yield return box.DisplayText(
                        $"{targetUnit.Unit.Base.Name} is already defeated, the attack misses!");
                }
                else
                {
                    bool guardCheck = (movequeue[target - 1] == 7);
                    var damageDetails = targetUnit.Unit.TakeDamage(move, unitcontrol.Unit, guardCheck);
                
                    //Display UI changes/update values
                    yield return unithuds[targetlist[index] - 1].UpdateHpBar();
                    unitcontrol.Unit.UseMove(move);
                    yield return unithuds[index].UpdateStaBar();
                    yield return ShowDamageDetails(damageDetails);
                    
                    if (damageDetails.Fainted)
                    {
                        yield return box.DisplayText($"{targetUnit.Unit.Base.Name} was defeated!");
                        defeatedUnits[target - 1] = true;
                        //Debug.Log("Switching to nextUnit:" + unithuds[target - 1].unitName.text + "to be swapped.");
                        if (targetUnit.isPlayer)
                        {
                            var nextUnit = party.GetNextHealthyUnit(targetUnit.Unit);
                            //Debug.Log("Cycled to next healthy unit: " + nextUnit.Base.Name);
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
                                targetUnit.Setup(nextUnit, true);
                                
                                unithuds[target - 1].Setdata(nextUnit);

                                yield return box.DisplayText($"{targetUnit.Unit.Base.Name} rises to the challenge!");
                            }
                            else
                            {
                                Debug.Log("No more healthy units left in the party");
                                // Handle the case where there are no more healthy units left in the party
                            }
                        }

                    }

                }
            
            
            }
        }
        //Go to unit 2 if unit 1 performing
        if (unit == 1)
        {
            StartCoroutine(PerformPlayerMove(2));
        }
        else
        {
            StartCoroutine(PerformEnemyMove(3));
        }
    }

    IEnumerator PerformEnemyMove(int unit)
    {
        //Debug.Log("enemy turn: " + unit);
        BattleUnit unitcontrol;
        int index;
        int target;
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

        //Debug.Log("Unit hp: " + unitcontrol.Unit.HP);
        if (unitcontrol.Unit.HP > 0 && !defeatedUnits[index])
        {
            if (movequeue[index] <= 6)
            {
                target = targetlist[index];
                var move = unitcontrol.Unit.GetRandomSKill();
                //enemy stamina check
                if (unitcontrol.Unit.STA < move.StaminaCost)
                {
                    yield return box.DisplayText($"{unitcontrol.unitBase.Name} rested up!");
                    unitcontrol.Unit.Rest();
                    yield return unithuds[index].UpdateStaBar();
                }
                else
                {
                    yield return box.DisplayText(unitcontrol.unitBase.Name + $" used {move.Base.Name}");
                    BattleUnit targetUnit = inBattleUnits[target - 1];
                
                    if (targetUnit.Unit.HP <= 0)
                    {
                        yield return box.DisplayText(
                            $"{targetUnit.Unit.Base.Name} is already defeated, the attack misses!");
                    }
                    else
                    {
                        bool guardCheck = (movequeue[target - 1] == 7);
                        var damageDetails = targetUnit.Unit.TakeDamage(move, unitcontrol.Unit, guardCheck);
                    
                        //Display UI changes/update values
                        yield return unithuds[targetlist[index] - 1].UpdateHpBar();
                        unitcontrol.Unit.UseMove(move);
                        yield return unithuds[index].UpdateStaBar();
                        yield return ShowDamageDetails(damageDetails);
                    
                        if (damageDetails.Fainted)
                        {
                            yield return box.DisplayText($"{targetUnit.Unit.Base.Name} was defeated!");
                            defeatedUnits[target - 1] = true;
                            //Debug.Log("Switching to nextUnit:" + unithuds[target - 1].unitName.text + "to be swapped.");
                            if (targetUnit.isPlayer)
                            {
                                var nextUnit = party.GetNextHealthyUnit(targetUnit.Unit);
                                //Debug.Log("Cycled to next healthy unit: " + nextUnit.Base.Name);
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
                                    targetUnit.Setup(nextUnit, true);
                                
                                    unithuds[target - 1].Setdata(nextUnit);

                                    yield return box.DisplayText($"{targetUnit.Unit.Base.Name} rises to the challenge!");
                                }
                                else
                                {
                                    Debug.Log("No more healthy units left in the party");
                                    // Handle the case where there are no more healthy units left in the party
                                }
                            }

                        }

                    }
                }
                
            }
            else if (movequeue[index] == 8)
            {
                yield return box.DisplayText($"{unitcontrol.unitBase.Name} rested up!");
                unitcontrol.Unit.Rest();
                yield return unithuds[index].UpdateStaBar();
            }
            
        }
        
        if (unit == 3)
        {
            //Debug.Log("Going to unit 4");
            StartCoroutine(PerformEnemyMove(4));
        }
        else
        {
            //Redo turn
            
            //yield return box.DisplayText("Return to player turn");
            StartCoroutine(NewRound());
            
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
        StartCoroutine(PerformPlayerMove(1));
        
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
            //Debug.Log("Unit 1 turn");
            index = 0;
            unit = unit1;
        }
        else
        {
            //Debug.Log("Unit 2 turn");
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
            yield return unithuds[index].UpdateStaBar();
            targetlist[index] = 0;
        }
        if (button == 9)
        {
            //Debug.Log("Escape button");
            //Escape
            int chance = (((unit.Unit.Speed) * 64) / ((unit3.Unit.Speed + unit2.Unit.Speed) / 2) + 30 % 256);
            int val = new Random().Next(1, 257);
            Debug.Log("Chance: " + chance);
            Debug.Log("Rolled: " + val);
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

