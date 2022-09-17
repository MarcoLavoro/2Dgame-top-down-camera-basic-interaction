using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private Text textCoin;
    [SerializeField]
    private Text textDays;
    [SerializeField]
    private GameObject[] Mushrooms;

    int coin=0;
    int days = 0;
   
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //disable the mushrooms
        SetMushroomState(false);

        //enable the dialog and when the user confirm fade the dialog
        DialogController.Instance.SetTextDialog("What? Why I am sleeping in a open field?" + System.Environment.NewLine +
"Maybe I have to stop to take weird mushrooms....", () =>
{
    FadeScreenManager.Instance.SetFadeInOrOut(false);
        });

        UpdateDaysVisual();
        UpdateCoinsVisual();

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #region public calls
    public void AddCoin(int amount)
    {
        coin += amount;
        UpdateCoinsVisual();
    }
    public void RemoveCoin(int amount)
    {
        coin -= amount;
        UpdateCoinsVisual();
    }
    public void AddDay()
    {
        days++;
        UpdateDaysVisual();
    }
    //set mushroom enable/disable
    public void SetMushroomState(bool state)
    {
        for (int i = 0; i < Mushrooms.Length; i++)
            Mushrooms[i].SetActive(state);
    }
    #endregion

    #region private calls
    private void UpdateDaysVisual()
    { textDays.text = days.ToString(); }
    private void UpdateCoinsVisual()
    { textCoin.text = coin.ToString(); }
    #endregion




}
