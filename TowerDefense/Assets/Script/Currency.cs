using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int currency;
    private float previousCurrency;
    [SerializeField]
    private GameObject textUI;
    [SerializeField]
    private int currencyValueToBeRecursevlyAdded;
    [SerializeField]
    private float timeBetweenAutomaticCurrencyIncrease;
    private float timeUntilCurrencyIncrease;

    private TextMeshProUGUI text;
    void Start()
    {
        text = textUI.GetComponent<TextMeshProUGUI>();
        currency = 200;
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
