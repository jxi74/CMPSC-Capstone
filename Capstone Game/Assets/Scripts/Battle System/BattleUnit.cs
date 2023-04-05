using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleUnit : MonoBehaviour
{
    [FormerlySerializedAs("base")] // use this attribute to rename the 'base' field to 'unitBase'
    public UnitBase unitBase; // rename the field to 'unitBase'
    public DamageIndictator damageText;
    private GameObject model;
    public GameObject baseprefab;

    public UnitUI hud;
    public int level;
    private bool _isPlayer;
    
    public bool isPlayer
    {
        get { return _isPlayer; }
    }

    public Unit Unit { get; set; }

    public void ModelSetup(bool a)
    {
        if (model == baseprefab)
        {
            return;
        }
        model = unitBase.Model;
        //Replace cylinder
        GameObject newModel = Instantiate(model, transform.position, transform.rotation, transform.parent);
        newModel.transform.SetPositionAndRotation(transform.position, transform.rotation);

        //Update unithud position
        unithud unitHud = GetComponentInChildren<unithud>();
        unitHud.transform.SetParent(newModel.transform);
        unitHud.transform.position = newModel.transform.position + Vector3.up * 5f;
        
        // Add the BattleUnit and Enemy components to the new model
        BattleUnit newBattleUnit = newModel.AddComponent<BattleUnit>();
        newBattleUnit.Unit = Unit;
        newBattleUnit.unitBase = unitBase;
        newBattleUnit.damageText = damageText;
        newBattleUnit.model = model;
        newBattleUnit.baseprefab = model;
        newBattleUnit.hud = hud;
        newBattleUnit.level = level;
        newBattleUnit._isPlayer = _isPlayer;
        
        
        model = newModel;
        
        
        //if overworld
        if (a)
        {
            Enemy newEnemy = newModel.AddComponent<Enemy>();
            newEnemy.distance = GetComponent<Enemy>().distance;

            EnemyEncounter newEnemyEncounter = newModel.AddComponent<EnemyEncounter>();
            newEnemyEncounter.enemyEncounter = GetComponent<EnemyEncounter>().enemyEncounter;
            newEnemyEncounter.unit = GetComponent<EnemyEncounter>().unit;
            model.layer = 7;
        }
        else
        {
            // Make the new model look at the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            newModel.transform.LookAt(player.transform);
            newModel.tag = gameObject.tag;
            //play sound?
        }
        //Destroy original cylinder
        Destroy(this.gameObject);
    }
    
    
    public void Setup(Unit unit, bool player)
    {
        Unit = unit;
        unitBase = unit.Base;
        level = unit.Level;
        _isPlayer = player;
        
        hud.Setdata(unit);
        //Display Unit model
        //Debug.Log($"{Unit.Base.name} setup");
        //Debug.Log(GetComponentInChildren<unithud>() != null);
        GetComponentInChildren<unithud>().setName(Unit.Base.Name, Unit.Level);
        //Replace cylinder
        model = unit.Base.Model;
        ModelSetup(false);

    }

    public void TakeDamage(int dmg)
    {
        
        DamageIndictator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndictator>();
        indicator.SetDamageText(dmg);
    }
}