using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//Battle States
public enum BattleState { Start, PlayerAction1, PlayerAction2, EnemyAction1, EnemyAction2, Busy}

public class BattleSystem : MonoBehaviour
{
    //Battlestate
    private BattleState state;
    [SerializeField] public List<int> movequeue;

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
    [SerializeField] private BattleUnit unit4;
    
    
    
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

    }
    public IEnumerator BattleSetup()
    {
        ResetMoveQueue();
        
        unit1.Setup();
        unit1hud.Setdata(unit1.Unit);
        
        unit2.Setup();
        unit2hud.Setdata(unit2.Unit);
        
        //Message box
        yield return box.DisplayText("A random " + unit3.Base.Name + " and " + unit4.Base.Name + " approaches!");
        
        yield return box.DisplayText(unit1.Base.Name + " and " + unit2.Base.Name + " went into action!");
        
        //Instantiate(unit1, _unit1Spawn.transform);

        PlayerAction(1);



    }

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

    void EnemyAction(int unit)
    {
        BattleUnit unitcontrol;
        int index;
        
        if (unit == 1)
        {
            state = BattleState.EnemyAction1;
            unitcontrol = unit3;
            index = 3;
        }
        else
        {
            state = BattleState.EnemyAction2;
            unitcontrol = unit4;
            index = 4;
        }

        
        //Instead of using decision tree for now, use random gen numbers
        int range = unitcontrol.Unit.Skills.Count;

        if (new Random().Next(0,2) == 0)
        {
            //use skills
            movequeue[index] = new Random().Next(1, unitcontrol.Unit.Skills.Count + 1);
        }
        else
        {
            //guard/rest
            movequeue[index] = new Random().Next(7, 9);
        }

    }

    //Button press handler during player turn
    public void ButtonPress(int button)
    {
        StartCoroutine(ButtonPress2(button));
    }

    public IEnumerator ButtonPress2(int button)
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
        battleUI.SetSkillButtons(false);

        if (button <= 6)
        {
            movename = unit.Unit.Skills[button - 1].Base.Name;
        }
        else if (button == 7)
        {
            movename = "Guard";
        }
        else
        {
            movename = "Rest";
        }
            
        yield return box.DisplayText(unit.Base.Name + " readied up with " + movename);

        if (state == BattleState.PlayerAction1)
        {
            PlayerAction(2);
        }
        else
        {
            Debug.Log("ENEMY TURN TRANSITION");
            //Go to enemy turn
        }
    }



}

