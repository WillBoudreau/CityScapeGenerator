using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStats : MonoBehaviour
{
    public BuildingGenerator buildingGenerator;
    public RoadGenerator roadGenerator;
    public float money;
    public float Electricity;
    public int People;
    public int PeopleMax;
    public float MonthTimer;
    public float CommercialBonus;
    public float costToUpgradeResidential = 10;
    public float costToUpgradeCommercial = 10;
    public float costToUpgradeIndustrial = 10;
    public float costToUpgradeGovernment = 10;

    void Update()
    {
        CheckStats();
    }
    void CheckStats()
    {
        if(MonthTimer >= 5)
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
        else if(money >= costToUpgradeCommercial && Electricity >= 1000)
        {
            UpgradeCommercial();
        }
        else if(money >= costToUpgradeIndustrial)
        {
            UpgradeIndustrial();
        }
        else if(money >= costToUpgradeGovernment && Electricity >= 1000)
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
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                    CommercialBonus += 5f;
                    money -= costToUpgradeCommercial;
                    Electricity -= 1000;
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
                    Electricity -= 1000;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                    money -= costToUpgradeIndustrial;
                    Electricity -= 1000;
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
