using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CityStats : MonoBehaviour
{
    private UnityEngine.UI.Image fillCircleImage;

    [Header("City Stats")]
    [Header("Class Calls")]
    public BuildingGenerator buildingGenerator;
    [Header("UI Elements")]
    public GameObject FillCircle;
    public Slider TimerSlider;
    [Header("City Stats")]
    public float money;
    public float Electricity;
    public int People;
    public float Stability;
    public int PeopleMax;
    public float MonthTimer;
    public float DeadLine = 5;
    public float CommercialBonus;
    public float costToUpgradeResidential = 10;
    public float costToUpgradeCommercial = 10;
    public float costToUpgradeIndustrial = 10;
    public float costToUpgradeGovernment = 10;

    void Start()
    {
        fillCircleImage = FillCircle.GetComponent<UnityEngine.UI.Image>();
    }

    void Update()
    {
        Timer();
        CheckStats();
    }
    void CheckStats()
    {
        Stability -= Time.deltaTime;
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
    public void ResetStats()
    {
        money = 0;
        Electricity = 0;
        People = 0;
        Stability = 100;
        PeopleMax = 0;
        MonthTimer = 0;
        DeadLine = 5;
        CommercialBonus = 0;
    }
    void Timer()
    {
        Debug.Log(MonthTimer);
        DeadLine = TimerSlider.value;
        fillCircleImage.fillAmount = MonthTimer / DeadLine;
    }
    void GenerateStats()
    {
        GenerateMoney();
        GeneratePeople();
        GenerateElectricity();
        GenerateStability();
        SubstractStats();
        CheckBalanceStats();
    }
    bool ChooseUpgrade(Building.BuildingType buildingType, float upgradeCost, float upgradeElectricity, int upgradePeople, float upgradeStability)
    {
        if(money >= upgradeCost && Electricity >= upgradeElectricity && People >= upgradePeople && Stability >= upgradeStability)
        {
            UpgradeBuilding(buildingType);
            return true;
        }
        return false;
    }
    void UpgradeBuilding(Building.BuildingType buildingType)
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == buildingType)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    building.UpgradeSize(Building.BuildingSize.Medium);
                    building.buildingSize = Building.BuildingSize.Medium;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    building.UpgradeSize(Building.BuildingSize.Large);
                    building.buildingSize = Building.BuildingSize.Large;
                }
            }
        }
    }
    void ChooseWhatToUpgrade()
    {
        if(ChooseUpgrade(Building.BuildingType.Commercial, costToUpgradeCommercial, 100, 10, 10))
        {
            UpgradeBuilding(Building.BuildingType.Commercial);
        }
        else if(ChooseUpgrade(Building.BuildingType.Industrial, costToUpgradeIndustrial, 50, 5, 5))
        {
            UpgradeBuilding(Building.BuildingType.Industrial);
        }
        else if(ChooseUpgrade(Building.BuildingType.Government, costToUpgradeGovernment, 200, 20, 20))
        {
            UpgradeBuilding(Building.BuildingType.Government);
        }
        else if(ChooseUpgrade(Building.BuildingType.Residential, costToUpgradeResidential, 100, 10, 10))
        {
            UpgradeBuilding(Building.BuildingType.Residential);
        }
    }
    void GenerateMoney()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Commercial)
            {
                money += 50 * CommercialBonus;
            }
            else if(building.buildingType == Building.BuildingType.Industrial)
            {
                money += 25;
            }
            else if(building.buildingType == Building.BuildingType.Government)
            {
                money += 100;
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
                    People += 5;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    People += 10;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    People += 15;
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
                    Electricity += 50;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    Electricity += 250;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    Electricity += 500;
                }
            }
        }
    }
    void GenerateStability()
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType == Building.BuildingType.Government)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    Stability += 25;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    Stability += 35;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    Stability += 45;
                }
            }
        }
        if(Stability >= 100)
        {
            Stability = 100;
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
                        Electricity -= 5;
                    }
                    else if(building.buildingSize == Building.BuildingSize.Medium)
                    {
                        Electricity -= 10;
                    }
                    else if(building.buildingSize == Building.BuildingSize.Large)
                    {
                        Electricity -= 15;
                    }
                    if(Electricity <= 0)
                    {
                        Electricity = 0;
                    }
                }
            if(building.buildingType != Building.BuildingType.Residential)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    People -= 1;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    People -= 2;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    People -= 3;
                }
                if(People <= 0)
                {
                    People = 0;
                }
            }
            if(building.buildingType != Building.BuildingType.Commercial)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    CommercialBonus -= 0.05f;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    CommercialBonus -= 0.1f;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    CommercialBonus -= 0.15f;
                }
                if(CommercialBonus <= 0)
                {
                    CommercialBonus = 0;
                }
            }
            if(building.buildingType != Building.BuildingType.Government)
            {
                if(building.buildingSize == Building.BuildingSize.Small)
                {
                    Stability -= 1f;
                }
                else if(building.buildingSize == Building.BuildingSize.Medium)
                {
                    Stability -= 2f;
                }
                else if(building.buildingSize == Building.BuildingSize.Large)
                {
                    Stability -= 3f;
                }
                if(Stability <= 0)
                {
                    Stability = 0;
                }
            }
        }
        
    }
    void CheckBalanceStats()
    {
        //Determines whihc stat is the lowest
        float lowestStat = Mathf.Min(money,Electricity, People, Stability);
        if(lowestStat == money && money <= 10)
        {
            DemolishAndReplace(Building.BuildingType.Commercial);
        }
        else if(lowestStat == Electricity && Electricity <= 10)
        {
            DemolishAndReplace(Building.BuildingType.Industrial);
        }
        else if(lowestStat == People && People <= 10)
        {
            DemolishAndReplace(Building.BuildingType.Residential);
        }
        else if(lowestStat == Stability && Stability <= 10)
        {
            DemolishAndReplace(Building.BuildingType.Government);
        }
        
    }
    void DemolishAndReplace(Building.BuildingType TargetbuildingType)
    {
        foreach(Building building in buildingGenerator.buildings)
        {
            if(building.buildingType != TargetbuildingType)
            {
                buildingGenerator.buildings.Remove(building);
                Destroy(building.gameObject);
                
                Building newBuilding = buildingGenerator.GenerateBuilding(TargetbuildingType, building.buildingSize, building.transform.position);
                buildingGenerator.buildings.Add(newBuilding);
                break;
            }
        }
    }
}
