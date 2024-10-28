using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int cityWidth;
    public int cityLength;
    public int BuildingWidth;
    public int BuildingHeight;
    public int BuildingLength;
    public int RoadWidth;
    public int RoadLength;
    public int RoadHeight;
    public float DistBetweenBuildings = 2.0f;
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> roads = new List<GameObject>();
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCity();
        }
    }
    void GenerateCity()
    {
        GenerateBuildings();
    }
    void GenerateBuildings()
    {
        foreach(GameObject building in buildings)
        {
            Destroy(building);
        }
        buildings.Clear();

        for (int x = 0; x < cityWidth; x++)
        {
            for (int z = 0; z < cityLength; z++)
            {
                BuildingDimension();
                Vector3 pos = new Vector3(x * (BuildingWidth + DistBetweenBuildings), 0, z * (BuildingLength + DistBetweenBuildings));

                if(ValidPos(pos))
                {
                    GameObject building = GenerateBuilding(BuildingWidth, BuildingHeight, BuildingLength);
                    building.transform.position = pos;
                }
            }
        }
    }
    bool ValidPos(Vector3 pos)
    {
        foreach(GameObject building in buildings)
        {
            if(Vector3.Distance(pos, building.transform.position) < BuildingWidth + DistBetweenBuildings)
            {
                return false;
            }
        }
        return true;
    }
    GameObject GenerateBuilding(int BuildingWidth, int BuildingHeight, int BuildingLength)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.transform.localScale = new Vector3(BuildingWidth, BuildingHeight, BuildingLength);
        buildings.Add(building);
        return building;
    }
    void BuildingDimension()
    {
        BuildingWidth = Random.Range(3, 10);
        BuildingHeight = Random.Range(5, 15);
        BuildingLength = Random.Range(3, 10);
    }
    Vector3 BuildingPos(int x, int z)
    {
        x = Random.Range(0, cityWidth);
        z = Random.Range(0, cityLength);
        return new Vector3(x, 0, z);
    }
    
}