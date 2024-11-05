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
    void GenerateCity()
    {
        CreateCity();
    }
    void CreateCity()
    {
        roadGenerator.GenerateRoads(CityWidth, CityLength,buildingGenerator);
        buildingGenerator.GenerateBuildings(CityWidth, CityLength,roadGenerator);
    }
    
}
