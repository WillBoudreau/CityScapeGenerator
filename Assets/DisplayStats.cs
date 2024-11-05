using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    [Header("Displaying City Stats")]

    [Header("Class Calls")]
    [SerializeField] private CityStats cityStats;

    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    [SerializeField] private TMPro.TextMeshProUGUI electricityText;
    [SerializeField] private TMPro.TextMeshProUGUI peopleText;
    [SerializeField] private TMPro.TextMeshProUGUI commercialBonusText;

    void Update()
    {
        DisplayStatsOnUI();
    }
    void DisplayStatsOnUI()
    {
        moneyText.text = "Money: " + cityStats.money;
        electricityText.text = "Electricity: " + cityStats.Electricity;
        peopleText.text = "People: " + cityStats.People;
        commercialBonusText.text = "Commercial Bonus: " + cityStats.CommercialBonus;
    }
}
