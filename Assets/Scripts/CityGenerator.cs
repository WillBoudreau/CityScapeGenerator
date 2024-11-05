using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CityGenerator : MonoBehaviour
{
    [Header("City Generation")]
    [Header("Class calls")]
    [SerializeField] private RoadGenerator roadGenerator;
    [SerializeField] private BuildingGenerator buildingGenerator;
    [SerializeField]private CityStats cityStats;
    [Header("City Size")]
    public float CityWidth;
    [SerializeField] private Slider CityWidthSlider;
    public float CityLength;
    [SerializeField] private Slider CityLengthSlider;
    [Header("Road Generation")]  
    [Header("City Prefabs")]
    public GameObject GroundPos;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cityStats.ResetStats();
            GenerateDimensions();
            CreateCity();
        }
    }
    void CreateCity()
    {
        roadGenerator.GenerateRoads(CityWidth, CityLength,buildingGenerator);
        buildingGenerator.GenerateBuildings(CityWidth, CityLength,roadGenerator);
    }
    void GenerateDimensions()
    {
        CityWidthSlider.maxValue = 100;
        CityWidthSlider.minValue = 0;
        CityLengthSlider.maxValue = 100;
        CityLengthSlider.minValue = 0;
        CityWidth = CityWidthSlider.value;
        CityLength = CityLengthSlider.value;
    }
    
}
