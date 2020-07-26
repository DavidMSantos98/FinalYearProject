                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{

    [SerializeField]
    private GameObject LevelManager;

    public int initialCurrencyValue;

    [HideInInspector]
    public int currency;
    private float previousCurrency;
    [SerializeField]
    private GameObject textUI;
    [SerializeField]
    private int currencyValueToBeRecursevlyAdded;
    [SerializeField]
    private float timeBetweenAutomaticCurrencyIncrease;
    private float timeUntilCurrencyIncrease;

    [SerializeField]
    private GameObject EnemiesHolder;

    [SerializeField]
    private GameObject PlayerDefeatUI;

    private TextMeshProUGUI text;
    void Start()
    {
        currency = initialCurrencyValue;
        text = textUI.GetComponent<TextMeshProUGUI>();
        timeUntilCurrencyIncrease = timeBetweenAutomaticCurrencyIncrease;
    }

    void Update()
    {

        if (timeUntilCurrencyIncrease <= 0)
        {
            currency += currencyValueToBeRecursevlyAdded;
            timeUntilCurrencyIncrease = timeBetweenAutomaticCurrencyIncrease;
        }
        else
        {
            timeUntilCurrencyIncrease -= Time.deltaTime;
        }

        if (currency != previousCurrency)
        {
            text.text = currency.ToString();
        }
        previousCurrency = currency;

        if (currency <= 0)
        {
            LevelManager.GetComponent<PauseGame>().Pause();
            PlayerDefeatUI.SetActive(true);
        }
    }

    public void SubtractCurrency(int i)
    {
        currency -= i;
    }

    public void AddCurrency(int i)
    {
        currency += i;
    }

    

}
