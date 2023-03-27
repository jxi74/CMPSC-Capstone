using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyUnits : MonoBehaviour
{
    private int _first = -1;
    private int _second = -1;
    Color selectcolor = new Color(0f, 0.329f, 0.416f, 203f/255f);
    private Color resetcolor = new Color(0f, 0f, 0f, 203f/255f);

    [SerializeField] private PartyHuds partyhuds;
    [SerializeField] private UIItem selectedItem;
    [SerializeField] private Party party;
    [SerializeField] private Inventory inventory;

    public void ClickHandler(int box)
    {
        
        if (selectedItem != null && selectedItem.item != null)
        {
            //Cancel swap inputs
            reset();
            Debug.Log($"Item was being held, {selectedItem.item.name}");

            //Use item
            if (selectedItem.item.Use(party.units[box]))
            {
                selectedItem.UpdateItem(null);
                partyhuds.SetPartyNamesParty();
            }
            
        }
        else
        {
            //First unit select
            if (_first == -1)
            {
                _first = box;
                partyhuds.partyhuds[box].transform.GetChild(0).GetComponent<Image>().color = selectcolor;
                Debug.Log("First selected");
            }
            else
            {
                if (_first == box)
                {
                    reset();
                    Debug.Log("Swap Cancelled");
                }
                else
                {
                    //handle 2nd unit to swap
                    //Swap the two units
                    _second = box;

                    Unit tempUnit = party.units[_first];
                    party.units[_first] = party.units[_second];
                    party.units[_second] = tempUnit;
                    partyhuds.SetPartyNamesParty();
                    reset();
                    // Reset the first unit selection
                

                    Debug.Log("Units were swapped");
                }
                
            }
            
            
            Debug.Log("No item was selected, performing swap select");
            
        }
        
    }

    public void UseItem() {
        
    }
    
    public void reset()
    {
        if (_first >= 0 && _first < partyhuds.partyhuds.Count)
        {
            partyhuds.partyhuds[_first].transform.GetChild(0).GetComponent<Image>().color = resetcolor;
        }

        if (_second >= 0 && _second < partyhuds.partyhuds.Count)
        {
            partyhuds.partyhuds[_second].transform.GetChild(0).GetComponent<Image>().color = resetcolor;
        }

        _first = -1;
        _second = -1;
    }
}
