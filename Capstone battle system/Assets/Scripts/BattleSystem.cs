using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

//Battle States
public enum BattleState { Start, PlayerAction1, PlayerAction2, PerformSkills}

public class BattleSystem : MonoBehaviour
{
    //Battlestate
    private BattleState state;
    [SerializeField] public List<int> movequeue;
    [SerializeField] public List<int> targetlist;

    //UI
    [SerializeField] private BattleUI battleUI;
    [SerializeField] MessageBox box;
    [SerializeField] public SkillsSelect skillsSelect;
    
    //player units
    [SerializeField] private BattleUnit unit1;
    [SerializeField] UnitUI unit1hud;
    [SerializeField] private BattleUnit unit2;
    [SerializeField] UnitUI unit2hud;
    //enemy units
    [SerializeField] private BattleUnit unit3;
    [SerializeField] UnitUI unit3hud;
    [SerializeField] private BattleUnit unit4;
    [SerializeField] UnitUI unit4hud;
    [SerializeField] private List<UnitUI> unithuds;
    
    [SerializeField] List<BattleUnit> inBattleUnits;
    

    void Start()
    {
        StartCoroutine(BattleSetup());
        battleUI.SetupBattle();
        
    }

    void ResetMoveQueue()
    {
        movequeue[0] = 0;
        movequeue[1] = 0;
        movequeue[2] = 0;
        movequeue[3] = 0;
        targetlist[0] = 0;
        targetlist[1] = 0;
        targetlist[2] = 0;
        targetlist[3] = 0;
    }
    public IEnumerator BattleSetup()
    {
        ResetMoveQueue();
        state = BattleState.Start;
        unit1.Setup();
        unit1hud.Setdata(unit1.Unit);
        unit2.Setup();
        unit2hud.Setdata(unit2.Unit);
        unit3.Setup();
        unit3hud.Setdata((unit3.Unit));
        unit4.Setup();
        unit4hud.Setdata(unit4.Unit);
        
        inBattleUnits.Add(unit1);
        inBattleUnits.Add(unit2);
        inBattleUnits.Add(unit3);
        inBattleUnits.Add(unit4);
        
        //Message box
        yield return box.DisplayText("A random " + unit3.Base.Name + " and " + unit4.Base.Name + " approaches!");
        
        yield return box.DisplayText(unit1.Base.Name + " and " + unit2.Base.Name + " went into action!");
        
        //Instantiate(unit1, _unit1Spawn.transform);

        StartCoroutine(NewRound());



    }

    IEnumerator NewRound()
    {
        ResetMoveQueue();
        if (unit1.Unit.HP == 0 || unit2.Unit.HP == 0)
        {
            //game over
            yield return box.DisplayText("You lose :(");
            yield return box.DisplayText("Loser.");
        }
        else if (unit3.Unit.HP == 0 || unit4.Unit.HP == 0)
        {
            yield return box.DisplayText("You win :)");
            //win
        }
        else
        {
            StartCoroutine(EnemyAction(3));
            StartCoroutine(EnemyAction(4));
            if (unit1.Unit.HP > 0)
            {
                Debug.Log("Unit1 turn");
                PlayerAction(1);
            }
            else if (unit2.Unit.HP > 0)
            {
                Debug.Log("Unit2 turn");
                PlayerAction(2);
            }
            else
            {
                yield return box.DisplayText("I don't really know what happened, but the game broke.. Fuck.");
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
        
        StartCoroutine(box.DisplayText("Select an action for " + unitcontrol.Base.Name));
        skillsSelect.SetSkillNames(unitcontrol.Unit.Skills);
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
            Debug.Log($"Enemy {index + 1}: {targetlist[index]}");
            //var move = unitcontrol.Unit.GetRandomSKill();
            
        }
        else
        {
            //guard/rest
            movequeue[index] = new Random().Next(7, 9);
            if (movequeue[index] == 7)
            {
                box.DisplayText($"{unitcontrol.Unit.Base.name} readied their guard!");
            }
            else
            {
                box.DisplayText($"{unitcontrol.Unit.Base.name} rested up!");
                unitcontrol.Unit.Rest();
                unithuds[index].UpdateStaBar();
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

        //check if alive
        if (unitcontrol.Unit.HP > 0)
        {
            
            //Only perform if skill
            if (movequeue[index] <= 6)
            {
                //Debug.Log();
                target = targetlist[index];
                var move = unitcontrol.Unit.Skills[movequeue[index] - 1];
                yield return box.DisplayText(unitcontrol.Base.Name + $" used {move.Base.Name}");
                BattleUnit targetUnit = inBattleUnits[target - 1];
                
                //yield return unithuds[targetlist[index] - 1].UpdateStaBar();
                if (targetUnit.Unit.HP <= 0)
                {
                    yield return box.DisplayText(
                        $"{targetUnit.Unit.Base.Name} is already deafeated, the attack misses!");
                }
                else
                {
                    bool guardCheck = (movequeue[target - 1] == 7);
                    bool isDefeated = targetUnit.Unit.TakeDamage(move, unitcontrol.Unit, guardCheck);
                
                    yield return unithuds[targetlist[index] - 1].UpdateHpBar();
                    unitcontrol.Unit.UseMove(move);
                    yield return unithuds[index].UpdateStaBar();
                    if (isDefeated)
                    {
                        
                        yield return box.DisplayText($"{targetUnit.Unit.Base.Name} was defeated!");
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
        Debug.Log("enemy turn: " + unit);
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

        Debug.Log("Unit hp: " + unitcontrol.Unit.HP);
        if (unitcontrol.Unit.HP > 0)
        {
            if (movequeue[index] <= 6)
            {
                target = targetlist[index];
                var move = unitcontrol.Unit.GetRandomSKill();
                yield return box.DisplayText(unitcontrol.Base.Name + $" used {move.Base.Name}");
                BattleUnit targetUnit = inBattleUnits[target - 1];
                
                if (targetUnit.Unit.HP <= 0)
                {
                    yield return box.DisplayText(
                        $"{targetUnit.Unit.Base.Name} is already defeated, the attack misses!");
                }
                else
                {
                    bool guardCheck = (movequeue[target - 1] == 7);
                    bool isDefeated = targetUnit.Unit.TakeDamage(move, unitcontrol.Unit, guardCheck);
                    yield return unithuds[targetlist[index] - 1].UpdateHpBar();
                    unitcontrol.Unit.UseMove(move);
                    yield return unithuds[index].UpdateStaBar();
                    if (isDefeated)
                    {
                        yield return box.DisplayText($"{targetUnit.Unit.Base.Name} was defeated!");
                    }
                }
            }
            
        }
        
        if (unit == 3)
        {
            Debug.Log("Going to unit 4");
            StartCoroutine(PerformEnemyMove(4));
        }
        else
        {
            //Redo turn
            
            //yield return box.DisplayText("Return to player turn");
            StartCoroutine(NewRound());
            
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
        //Debug.Log("Button: " + button);
        battleUI.SetActionButtons(false);
        battleUI.SetSelectButtons(false);

        if (button <= 6)
        {
            movename = unit.Unit.Skills[button - 1].Base.Name;
            yield return box.DisplayText(unit.Base.Name + " readied up with " + movename + "!");
            targetlist[index] = target;

        }
        else if (button == 7)
        {
            //Guard
            yield return box.DisplayText(unit.Base.Name + " readied their guard!");
            targetlist[index] = 0;
        }
        else
        {
            //Rest
            yield return box.DisplayText(unit.Base.Name + " rested up!");
            unit.Unit.Rest();
            yield return unithuds[index].UpdateStaBar();
            targetlist[index] = 0;
            
        }
            
        

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
        else
        {
            StartCoroutine(PerformBattle());
        }
    }



}

