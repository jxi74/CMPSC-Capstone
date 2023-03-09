using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParty : MonoBehaviour
{
    //creates a slot game object using the prefab established
    public GameObject slotPrefab;

    public Transform partyPanel;

    public int partySize = 6;

    private void Awake()
    {
        for (int p = 0; p < partySize; p++)
        { 
           GameObject instance = Instantiate(slotPrefab);
           instance.transform.SetParent(partyPanel);
        }
    }

}
