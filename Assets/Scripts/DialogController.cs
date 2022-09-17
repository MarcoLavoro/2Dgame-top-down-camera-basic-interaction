using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance;
    public Text textDialog;
    UnityAction callback;
    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space))|| (Input.GetMouseButtonDown(0)))
        {
            ConfirmedDialog();
        }
    }

    public void SetTextDialog(string text, UnityAction _callback)
    {
        textDialog.text = text;
        callback = _callback;
        this.gameObject.SetActive(true);

    }
    public void ConfirmedDialog()
    {
        callback?.Invoke();
        this.gameObject.SetActive(false);

    }
}
