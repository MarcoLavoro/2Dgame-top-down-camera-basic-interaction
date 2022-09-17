using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeController : Interactables
{
    public override void Interact()
    {
        DialogController.Instance.SetTextDialog("this is" + System.Environment.NewLine +
        "a hoe...", null);

        InventoryController.Instance.AddToInventory(InvenctoryType.Hoe);
        Destroy(this.gameObject);
    }
}
