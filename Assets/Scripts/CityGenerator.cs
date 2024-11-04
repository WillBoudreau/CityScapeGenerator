using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [Header("City Generation")]
    [Header("Class calls")]
    public RoadGenerator roadGenerator;
    public BuildingGenerator buildingGenerator;
    [Header("City Size")]
    [Range(0, 100)]
    public float CityWidth = 100f;
    [Range(0, 100)]
    public float CityLength = 100f;
    [Header("Road Generation")]  
    [Header("City Prefabs")]
    public GameObject GroundPos;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCity();
        }
    }
    // void ClearCity()
    // {
    //     foreach (GameObject road in roadGenerator.roads)
    //     {
    //         Destroy(road);
    //     }
    //     foreach (Building building in buildingGenerator.buildings)
    //     {
    //         Destroy(building);
    //     }
    //     buildingGenerator.buildings.Clear();
    //     roadGenerator.roads.Clear();
    // }
    void GenerateCity()
    {
        CreateCity();
        FindValidPOS(buildingGenerator.buildings, roadGenerator.roads);
    }
    void FindValidPOS(List<Building> Buildings, List<GameObject> Roads)
    {
        foreach (Building building in Buildings)
        {
            foreach (GameObject road in Roads)
            {
                if (building.transform.position == road.transform.position)
                {
                    Destroy(building);
                }
            }
        }
    }
    void CreateCity()
    {
        roadGenerator.GenerateRoads(CityWidth, CityLength);
        buildingGenerator.GenerateBuildings(CityWidth, CityLength);
    }
}
