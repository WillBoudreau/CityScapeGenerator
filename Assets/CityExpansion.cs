using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityExpansion : MonoBehaviour
{
    [Header("City Expansion")]
    [SerializeField] private CityStats cityStats;
    [SerializeField] private CityGenerator cityGenerator;
    [SerializeField] private float CostToExpandCity = 1000;

    void CheckWhereToExpand()
    {

    }
    void CheckStats()
    {
        float money = cityStats.money;
        if(money >= CostToExpandCity)
        {
            ExpandCity();
        }
    }
    void ExpandCity()
    {
        if(cityStats.money >= 1000)
        {
            cityStats.money -= 1000;
            cityGenerator.CityWidth += 100;
            cityGenerator.CityLength += 100;
        }
    }
}
