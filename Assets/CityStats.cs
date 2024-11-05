using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CityStats : MonoBehaviour
{
    [Header("City Stats")]
    public BuildingGenerator buildingGenerator;
    public RoadGenerator roadGenerator;
    public GameObject FillCircle;
    public float money;
    public float Electricity;
    public int People;
    public int PeopleMax;
    public float MonthTimer;
    public float DeadLine = 5;
    public float CommercialBonus;
    public float costToUpgradeResidential = 10;
    public float costToUpgradeCommercial = 10;
    public float costToUpgradeIndustrial = 10;
    public float costToUpgradeGovernment = 10;

    void Update()
    {
        CheckStats();
        DeadLine = 5;
    }
    void CheckStats()
    {
        FillCircle.GetComponent<UnityEngine.UI.Image>().fillAmount = MonthTimer / DeadLine;
        if(MonthTimer >= DeadLine)
        {
            GenerateStats();
            ChooseWhatToUpgrade();
            MonthTimer = 0;
        }
        else
        {
            MonthTimer += Time.deltaTime;
        }
    }
    void GenerateStats()
    {
        GenerateMoney();
        GeneratePeople();
        GenerateElectricity();
        SubstractStats();
    }
    void GenerateMoney()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Commercial)
            {
                money += 100 * CommercialBonus;
            }
            else if(building.buildingType == Building.BuildingType.Industrial)
            {
                money += 50;
            }
            else if(building.buildingType == Building.BuildingType.Government)
            {
                money += 200;
            }
        }
    }
    void GeneratePeople()
    {
        foreach(Building building in buildingGenerator.buildings)
        { 
            if(building.buildingType == Building.BuildingType.Residential)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    People += 10;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    People += 20;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    People += 30;
                }
            }
        }
    }
    void GenerateElectricity()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Industrial)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    Electricity += 100;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    Electricity += 500;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    Electricity += 1000;
                }
            }
        }
    }
    void SubstractStats()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType != Building.BuildingType.Industrial)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    Electricity -= 10;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    Electricity -= 20;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    Electricity -= 30;
                }
            }
        }
    }
    void ChooseWhatToUpgrade()
    {
        if(money >= costToUpgradeResidential && Electricity >= 1000)
        {
            UpgradeResidential();
        }
        else if(money >= costToUpgradeCommercial && Electricity >= 1000 && People >= 100)
        {
            UpgradeCommercial();
        }
        else if(money >= costToUpgradeIndustrial && People >= 50)
        {
            UpgradeIndustrial();
        }
        else if(money >= costToUpgradeGovernment && Electricity >= 1000 && People >= 100)
        {
            UpgradeGovernment();
        }
    }
    void UpgradeResidential()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Residential)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    building.UpgradeSize(Building.BuildingSize.Medium);
                    building.buildingSize = Building.BuildingSize.Medium;
                    money -= costToUpgradeResidential;
                    Electricity -= 1000;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                    money -= costToUpgradeResidential;
                    Electricity -= 1000;
                }
            }
        }
    }
    void UpgradeCommercial()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Commercial)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    building.UpgradeSize(Building.BuildingSize.Medium);
                    building.buildingSize = Building.BuildingSize.Medium;
                    CommercialBonus += 2f;
                    money -= costToUpgradeCommercial;
                    Electricity -= 1000;
                    People -= 100;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                    CommercialBonus += 5f;
                    money -= costToUpgradeCommercial;
                    Electricity -= 1000;
                    People -= 100;
                }
            }
        }
    }
    void UpgradeIndustrial()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Industrial)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    building.UpgradeSize(Building.BuildingSize.Medium);
                    building.buildingSize = Building.BuildingSize.Medium;
                    money -= costToUpgradeIndustrial;
                    People -= 50;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                    money -= costToUpgradeIndustrial;
                    People -= 50;
                }
            }
        }
    }
    void UpgradeGovernment()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Government)
            {
                building.UpgradeSize(Building.BuildingSize.Medium);
                money -= costToUpgradeGovernment;
            }
        }
    }
}
