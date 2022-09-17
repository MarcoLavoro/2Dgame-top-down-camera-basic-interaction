using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : Interactables
{
    int state = 0;
    public override void Interact()
    {
        ManageInteract();
    }

    private void ManageInteract()
    {
        //search if there are the Hoe in the invencoty, if yes set state 1
        if (InventoryController.Instance.SearchPresenceInsideInvenctory(InvenctoryType.Hoe))
            state = 1;
        //search if the Hoe in the equipped, if yes set state 2
        Inventory equipped = InventoryController.Instance.GetSelectedItem(InvenctoryType.Hoe);
        if(equipped!=null)
        if (equipped.interactable == InvenctoryType.Hoe)
            state = 2;

        ManageDialog();
      
    }

    private void ManageDialog()
    {
        switch (state)
        {
            case 0: //If I not have the hoe send message and do nothing
                DialogController.Instance.SetTextDialog("Seems I need a Hoe" + System.Environment.NewLine +
  "to take it", null);
              //  GameManager.Instance.SetMushroomState(true);
                state = 1;
                break;

            case 1://If I have the hoe but not equipped send message and do nothing
                DialogController.Instance.SetTextDialog("I need to equip" + System.Environment.NewLine +
  "The hoe to dig it!!", null);
                break;

            case 2://If I have the hoe equipped send message and take the mushroom
                PlayerController.Instance.SetAnimationDig(() =>{
                    DialogController.Instance.SetTextDialog("I taken" + System.Environment.NewLine + "a mushroom!!", null);
                    InventoryController.Instance.AddToInventory(InvenctoryType.Mushroom); 
                    Destroy(this.gameObject); 
                });
                
                break;
        }
    }

}
