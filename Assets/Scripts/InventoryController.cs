using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //references
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private Transform Container;
    [SerializeField]
    private Sprite[] InvenctorySprites;

    public static InventoryController Instance;

    //private variables
    List<Inventory> inventory;
    List<InventoryElementController> inventoryControllers;
    int maxSize = 7;
    int actualSelection = -1;
    private void Awake()
    {
        inventory = new List<Inventory>();
        inventoryControllers = new List<InventoryElementController>();
        for (int i = 0; i < maxSize; i++)
        {
            GameObject O = GameObject.Instantiate(Prefab, Container);
            InventoryElementController elementController = O.GetComponent<InventoryElementController>();
            inventoryControllers.Add(elementController);
            O.SetActive(true);
        }

            Instance = this;
        RefreshInvenctory();
    }


    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
                RefreshInvenctorySelection(1);
            else
                RefreshInvenctorySelection(-1);

        }
    }

    #region public calls
    //serch if there is a specific element in the inventory
    public bool SearchPresenceInsideInvenctory(InvenctoryType element, int minElements = 0)
    {
        bool finded = false;
        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i].interactable == element)
                if (inventory[i].quantity >= minElements)
                    finded = true;

        return finded;
    }
    //return a specific element if present in the inventory
    public Inventory GetSelectedItem(InvenctoryType type)
    {
        Inventory returned = null;

        for (int i = 0; i < inventory.Count; i++)
            if (i == actualSelection)
                if (inventory[i].interactable == type)
                    returned = inventory[i];
        return returned;
    }
    //remove an element from inventory
    public void RemoveFromInventory(InvenctoryType element, int minElements = 1)
    {

        Inventory elementToRemove = null;

        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i].interactable == element)
            {
                inventory[i].quantity -= minElements;
                if (inventory[i].quantity <= 0)
                    elementToRemove = inventory[i];

            }

        if (elementToRemove != null)
            inventory.Remove(elementToRemove);
        RefreshInvenctory();
    }
    //add an element from inventory
    public void AddToInventory(InvenctoryType element)
    {
        bool present = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].interactable == element)
            {
                present = true;
                inventory[i].quantity++;
            }
        }
        if (!present)
        {
            Inventory element1 = new Inventory();
            element1.interactable = element;
            element1.immagine = InvenctorySprites[(int)element1.interactable];
            element1.quantity = 1;
            inventory.Add(element1);
        }
        RefreshInvenctory();
    }
    #endregion

    #region private calls
    //refresh graphics inventory selection
    private void RefreshInvenctorySelection(int howMuch)
    {
        if (inventory.Count > 0)
        {
            actualSelection += howMuch;
            if (actualSelection >= inventory.Count)
                actualSelection = 0;

            if (actualSelection < 0)
                actualSelection = inventory.Count - 1;
        }
        RefreshInvenctory();
    }
    //refresh graphics inventory
    private void RefreshInvenctory()
    {
        for (int i = 0; i < inventoryControllers.Count; i++)
        {
            inventoryControllers[i].Valorize(false, null, "", false);
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryControllers[i].Valorize(true, inventory[i].immagine, inventory[i].quantity.ToString(), i == actualSelection);
        }
    }
    #endregion
    
}

public class Inventory
{
    public Sprite immagine;
    public InvenctoryType interactable;
    public int quantity;
}

public enum InvenctoryType { Hoe = 0, Mushroom = 1};