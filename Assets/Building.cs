using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingType
    {
        Residential,
        Commercial,
        Industrial,
        Government,
    }
    public BuildingType buildingType;
    public GameObject buildingPrefab;
    [Range(0, 100)]
    public float BuildingWidth = 5f;
    [Range(0, 100)]
    public float BuildingHeight = 10f;
    [Range(0, 100)]
    public float BuildingLength = 5f;
    public float BuildingsInRange = 10f;
    public List<Building> buildings = new List<Building>();
    void Start()
    {
        GenerateBuilding();
    }
    public Vector3 GenerateSize()
    {
        return new Vector3(BuildingWidth, BuildingHeight, BuildingLength);
    }
    public void SelectType(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.Residential:
                this.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case BuildingType.Commercial:
                this.GetComponent<Renderer>().material.color = Color.green;
                break;
            case BuildingType.Industrial:
                this.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case BuildingType.Government:
                this.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }
    void GenerateBuilding()
    {
        foreach (Building building in buildings)
        {
           CheckBuildingsInRange(buildings);
        }
    }
    void CheckBuildingsInRange(List<Building> buildings)
    {
        foreach (Building building in buildings)
        {
            if (Vector3.Distance(this.transform.position, building.transform.position) <= BuildingsInRange)
            {
                if (building.buildingType == this.buildingType)
                {
                    ChangeBuildingType(building);
                }
            }
        }
    }
    void ChangeBuildingType(Building building)
    {
        BuildingType newType = (BuildingType)(((int)building.buildingType + 1) % System.Enum.GetValues(typeof(BuildingType)).Length);
        building.buildingType = newType;
        building.SelectType(newType);
    }
}
