using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElementController : MonoBehaviour
{
    [SerializeField]
    private Image backGround;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;

    public void Valorize(bool active, Sprite _image, string _text, bool isSelected)
    {
        image.sprite = _image;
        text.text = _text;
        image.gameObject.SetActive(active);
        text.gameObject.SetActive(active);

        if (isSelected)
            backGround.color = Color.green;
        else
            backGround.color = Color.white;

    }

}
