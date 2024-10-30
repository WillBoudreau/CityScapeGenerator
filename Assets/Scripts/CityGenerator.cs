using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [Header("City Variables")]
    public int cityWidth;
    public int cityLength;
    public float DistBetweenBuildings = 2.0f;
    public float DistBetweenBlocks = 5.0f;
    public GameObject CityBlock;
    public GameObject CityCenter;
    public GameObject RoadBlock;
    [Header("City Block Variables")]
    public int CityBlockWidth;
    public int CityBlockLength;
    [Header("Road Block Variables")]
    public int RoadWidth;
    public int RoadLength;
    [Header("Lists")]
    public List<GameObject> cityBlocks = new List<GameObject>();
    public List<GameObject> roadBlocks = new List<GameObject>();
    [Header("Class calls")]
    public BuildingGenerator buildingGenerator;
    public RoadGenerator roadGenerator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCity();
        }
    }
    void GenerateCity()
    {
        GenerateRoadBlocks();
        GenerateCityBlocks();
        GenerateBuildings();
        GenerateRoads();
    }
    void GenerateBuildings()
    {
        foreach(GameObject building in buildingGenerator.buildings)
        {
            Destroy(building);
        }
        buildingGenerator.buildings.Clear();
        foreach (GameObject cityBlock in cityBlocks)
        {
            buildingGenerator.BuildingDimension();
            if (buildingGenerator.BuildingWidth != 0 && buildingGenerator.BuildingHeight != 0 && buildingGenerator.BuildingLength != 0)
            {
                Vector3 pos = cityBlock.transform.position + new Vector3(0, -5, 0);

                if(ValidPos(CityBlockPos((int) pos.x, (int) pos.z)))
                {
                    Debug.Log("Valid Position");
                    GameObject building = buildingGenerator.GenerateBuilding(buildingGenerator.BuildingWidth, buildingGenerator.BuildingHeight, buildingGenerator.BuildingLength);
                    building.transform.position = pos;
                    building.transform.localScale = new Vector3(buildingGenerator.BuildingWidth, buildingGenerator.BuildingHeight, buildingGenerator.BuildingLength);
                    StartCoroutine(buildingGenerator.ScaleBuilding(building, buildingGenerator.BuildingHeight)); // Scale up based on y value
                }
            }
        }
    }
    void GenerateRoads()
    {
        foreach(GameObject road in roadGenerator.roads)
        {
            Destroy(road);
        }
        roadGenerator.roads.Clear();
        foreach(GameObject roadBlock in roadBlocks)
        {
            roadGenerator.RoadDimension();
            if(roadGenerator.RoadWidth != 0 && roadGenerator.RoadLength != 0)
            {
                Vector3 pos = roadBlock.transform.position + new Vector3(0, -5, 0);
                if(ValidPos(RoadBlockPos((int) pos.x, (int) pos.z)))
                {
                    GameObject road = roadGenerator.GenerateRoad(roadGenerator.RoadWidth, roadGenerator.RoadLength);
                    road.transform.position = pos;
                    StartCoroutine(roadGenerator.ScaleRoad(road, roadGenerator.RoadLength));
                    roadGenerator.roads.Add(road);
                }
            }
        }
    }
    void BlockCheck(List<GameObject> cityBlocks,List<GameObject> roadBlocks)
    {
        foreach(GameObject cityBlock in cityBlocks)
        {
            foreach(GameObject roadBlock in roadBlocks)
            {
                if(Vector3.Distance(cityBlock.transform.position, roadBlock.transform.position) < 1)
                {
                    Destroy(RoadBlock);
                    cityBlocks.Remove(RoadBlock);
                }
            }
        }
    }
    void GenerateCityBlocks()
    {
        foreach(GameObject cityBlock in cityBlocks)
        {
            Destroy(cityBlock);
        }
        cityBlocks.Clear();
        for(int i = 0; i<= cityWidth; i ++)
        {
            for(int j = 0; j <= cityLength; j ++)
            {
                CityBlockDimension();
                if(CityBlockWidth != 0 && CityBlockLength != 0)
                {
                    Vector3 pos = new Vector3(i * (CityBlockWidth + DistBetweenBlocks), 0, j * (CityBlockLength + DistBetweenBlocks));
                    GameObject cityBlock = GenerateCityBlock(CityBlockWidth, CityBlockLength);
                    cityBlock.transform.position = pos;
                    cityBlocks.Add(cityBlock);
                }
            }
        }
    }
    void GenerateRoadBlocks()
    {
        foreach(GameObject road in roadBlocks)
        {
            Destroy(road);
        }
        roadBlocks.Clear();
        for(int i = 0; i <= cityWidth; i += 2)
        {
            for(int j = 0; j <= cityLength; j += 2)
            {
                RoadBlockDimension();
                if(RoadWidth != 0 && RoadLength != 0)
                {
                    Vector3 pos = new Vector3(i * (RoadWidth + DistBetweenBlocks), 0, j * (RoadLength + DistBetweenBlocks));
                    GameObject road = GenerateRoadBlock(RoadWidth, RoadLength);
                    road.transform.position = pos;
                    roadBlocks.Add(road);
                }
            }
        }
    }
    bool ValidPos(Vector3 pos)
    {
        foreach(GameObject building in buildingGenerator.buildings)
        {
            if(Vector3.Distance(pos, building.transform.position) < buildingGenerator.BuildingWidth + DistBetweenBuildings)
            {
                return false;
            }
        }
        return true;
    }
    GameObject GenerateCityBlock(int CityBlockWidth, int CityBlockLength)
    {
        GameObject cityBlock = Instantiate(CityBlock);
        return cityBlock;
    }
    void CityBlockDimension()
    {
        CityBlockLength = 5;
        CityBlockWidth = 5;
    }
    Vector3 CityBlockPos(int x, int y)
    {
        x = Random.Range(0, cityWidth);
        y = Random.Range(0, cityLength);
        return new Vector3(x, 0, y);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(cityWidth, 0, cityLength));
    }
    GameObject GenerateRoadBlock(int RoadWidth, int RoadLength)
    {
        GameObject road = Instantiate(RoadBlock);
        return road;
    }
    void RoadBlockDimension()
    {
        RoadLength = 5;
        RoadWidth = 5;
    }
    Vector3 RoadBlockPos(int x, int y)
    {
        x = Random.Range(1, cityWidth);
        y = Random.Range(1, cityLength);
        return new Vector3(x, 0, y);
    }
}
