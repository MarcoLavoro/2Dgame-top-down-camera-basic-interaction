using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactables
{
    int state = 0;
    public override void Interact()
    {
        ManageInteract();
    }

    private void ManageInteract()
    {
        //if I already interacted once, if player have enought mushroom reward him/her
        if(state==1)
            if (InventoryController.Instance.SearchPresenceInsideInvenctory(InvenctoryType.Mushroom, 5))
            {
                state = 2;
                InventoryController.Instance.RemoveFromInventory(InvenctoryType.Mushroom, 5);
                GameManager.Instance.AddCoin(100);
            }


        ManageDialog();
    }

    private void ManageDialog()
    {
        switch (state)
        {
            case 0: //if is the first interaction enable the mushrooms
                DialogController.Instance.SetTextDialog("I need mushrooms! You can find that under the trees" + System.Environment.NewLine +
  "You probably need an hoe...", null);
                GameManager.Instance.SetMushroomState(true);
                state = 1;
                break;

            case 1://if the mushrooms is not enought
                DialogController.Instance.SetTextDialog("Are you still here?" + System.Environment.NewLine +
  "I need more mushroom...", null);
                break;

            case 2://if you did give all the mushrooms
                DialogController.Instance.SetTextDialog("Cool! this 100 coin is for you!" + System.Environment.NewLine +
  "yey..", null);
                state = 3;
                break;

            case 3://if all tasks is completed
                DialogController.Instance.SetTextDialog("I am sorry....I have nothing left..." + System.Environment.NewLine +
  "I am just a cow.....", null);
                break;
        }
    }

}
