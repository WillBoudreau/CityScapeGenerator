using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [Header("City Generation")]
    [Header("City Prefabs")]
    public float CityWidth = 100f;
    public float CityLength = 100f;
    public float BuildingSpacing = 10f;
    public float RoadWidth = 5f;
    public float RoadSpacing = 10f;
    public float RoadHeight = 0.1f; 
    public GameObject buildingPrefab;
    public GameObject roadPrefab;
    public GameObject GroundPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCity();
        }
    }
    public void GenerateCity()
    {
        GenerateRoads();
        //GenerateBuildings();
    }
    void GenerateRoads()
    {
        for (int i = 0; i < CityWidth; i++)
        {
            for (int j = 0; j < CityLength; j++)
            {
                if (i % RoadSpacing == 0 || j % RoadSpacing == 0)
                {
                    // Add caps at the city bounds
                    if (i == 0 || i == CityWidth - 1)
                    {
                        GameObject roadCap = Instantiate(roadPrefab, new Vector3(i, RoadHeight, j + RoadWidth / 2), Quaternion.Euler(0, 90, 0));
                        roadCap.transform.localScale = new Vector3(RoadWidth, RoadHeight, RoadWidth);
                    }
                    if (j == 0 || j == CityLength - 1)
                    {
                        GameObject roadCap = Instantiate(roadPrefab, new Vector3(i + RoadWidth / 2, RoadHeight, j), Quaternion.identity);
                        roadCap.transform.localScale = new Vector3(RoadWidth, RoadHeight, RoadWidth);
                    }
                    GameObject road = Instantiate(roadPrefab, new Vector3(i, RoadHeight, j), Quaternion.identity);
                    road.transform.localScale = new Vector3(RoadWidth, RoadHeight, RoadWidth);
                }
            }
        }
    }
    void GenerateBuildings()
    {
        for (int i = 0; i < CityWidth; i++)
        {
            for (int j = 0; j < CityLength; j++)
            {
                if (i % BuildingSpacing == 0 && j % BuildingSpacing == 0)
                {
                    GameObject building = Instantiate(buildingPrefab, new Vector3(i,-5, j), Quaternion.identity);
                }
            }
        }
    }
}
