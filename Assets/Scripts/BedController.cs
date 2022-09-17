using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : Interactables
{
    public override void Interact()
    {
     FadeScreenManager.Instance.SetFadeInOrOut(true);

     DialogController.Instance.SetTextDialog("Ahhhh" + System.Environment.NewLine +
    "I needed a little rest...", () =>
    {
        GameManager.Instance.AddDay();
        FadeScreenManager.Instance.SetFadeInOrOut(false);
    });
    }
}
