using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//UI display of what the player currently holds in their inventory
public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceText;
    
    //list of all items currently in player's inventory
    public List<UIItem> uIItems = new List<UIItem>();

    //creates a slot game object using the prefab established
    public GameObject slotPrefab;

    //creates the panels in which the slots will be added for each page
    public Transform[] slotPanels;

    //the number of slots allowed in each page of the inventory
    public int numberOfSlotsPerPage = 16;

    //the current page the player is on
    private int currentPage = 1;

    private void Awake()
    {
        //instantiates the layout of all four inventory pages
        for (int p = 0; p < slotPanels.Length; p++)
        {
            for (int i = 0; i < numberOfSlotsPerPage; i++)
            {
                GameObject instance = Instantiate(slotPrefab);
                instance.transform.SetParent(slotPanels[p]);
                if (p != 0)
                {
                    slotPanels[p].gameObject.SetActive(false);
                }
                uIItems.Add(instance.GetComponentInChildren<UIItem>());
            }
        }
        GameObject previousPageButton = GameObject.Find("PreviousButton");
        previousPageButton.GetComponent<Button>().interactable = false;
    }

    public void UpdateBalance(int balance)
    {
        balanceText.text = string.Format("{0:#,###0}", balance);
    }
    
    //updates the desired slot of the uIItems list with the desired item on the current page
    public void UpdateSlot(int slot, ItemBase item)
    {
        uIItems[(currentPage - 1) * numberOfSlotsPerPage + slot].UpdateItem(item);
    }

    //adds a new item to the next open slot in the inventory on the current page
    public void AddNewItem(ItemBase item)
    {
        UpdateSlot(uIItems.FindIndex((i) => i.item == null), item);
    }

    //removes the first instance of an item from the current inventory page
    public void RemoveItem(ItemBase item)
    {
        UpdateSlot(uIItems.FindIndex((i) => i.item == item), null);
    }

    //switches to the next page of the inventory
    public void NextPage()
    {
        if (currentPage < slotPanels.Length)
        {
            currentPage++;
            GameObject previousPageButton = GameObject.Find("PreviousButton");
            previousPageButton.GetComponent<Button>().interactable = true;
            // Disable the next page button if the current page is the last page
            if (currentPage == slotPanels.Length)
            {
                // Find the next page button in the UI and disable it
                GameObject nextPageButton = GameObject.Find("Next Button");
                nextPageButton.GetComponent<Button>().interactable = false;
            }
            for (int p = 0; p < slotPanels.Length; p++)
            {
                if (p == currentPage - 1)
                {
                    slotPanels[p].gameObject.SetActive(true);
                }
                else
                {
                    slotPanels[p].gameObject.SetActive(false);
                }
            }
        }
    }

    //switches to the previous page of the inventory
    public void PreviousPage()
    {
            currentPage--;
            // Enable the next page button if it was disabled on the last page
            if (currentPage == slotPanels.Length - 1)
            {
                // Find the next page button in the UI and enable it
                GameObject nextPageButton = GameObject.Find("Next Button");
                nextPageButton.GetComponent<Button>().interactable = true;
            }
            if(currentPage == 1)
            {
                GameObject previousPageButton = GameObject.Find("PreviousButton");
                previousPageButton.GetComponent<Button>().interactable = false;
            }
        for (int p = 0; p < slotPanels.Length; p++)
        {
            if (p == currentPage - 1)
            {
                slotPanels[p].gameObject.SetActive(true);
            }
            else
            {
                slotPanels[p].gameObject.SetActive(false);
            }
        }
    }
}