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
                partyhuds[i].gameObject.SetActive(true);
            }
            else
            {
                // Set the UnitUI to be hidden if not enough units
                partyhuds[i].gameObject.SetActive(false);
            }
        }
    }
}
