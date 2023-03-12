using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHuds : MonoBehaviour
{
    [SerializeField] Party party;
    [SerializeField] List<UnitUI> partyhuds;
    public void SetPartyNames()
    {
        for (int i = 0; i < partyhuds.Count; i++)
        {
            if (i < party.units.Count)
            {
                partyhuds[i].Setdata(party.units[i]);
                if (party.units[i].HP > 0)
                {
                    partyhuds[i].gameObject.SetActive(true);
                }
                else
                {
                    partyhuds[i].gameObject.SetActive(false);
                }
                
            }
            else
            {
                // Set the UnitUI to be hidden if not enough units
                partyhuds[i].gameObject.SetActive(false);
            }
        }
    }
}
