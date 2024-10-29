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
    [Header("Building Variables")]
    public int BuildingWidth;
    public int BuildingHeight;
    public int BuildingLength;
    public int BuildingMinWidth;
    public int BuildingMaxWidth;
    public int BuildingMinHeight;
    public int BuildingMaxHeight;
    public int BuildingMaxLength;
    public int BuildingMinLength;
    public float DistFromCityCenter;
    [Header("Road Variables")]
    public int RoadWidth;
    public int RoadLength;
    public int RoadHeight;
    [Header("City Block Variables")]
    public int CityBlockWidth;
    public int CityBlockLength;
    [Header("Lists")]
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> roads = new List<GameObject>();
    public List<GameObject> cityBlocks = new List<GameObject>();
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCity();
        }
    }
    void GenerateCity()
    {
        GenerateCityBlocks();
        GenerateBuildings();
    }
    void GenerateBuildings()
    {
        foreach(GameObject building in buildings)
        {
            Destroy(building);
        }
        buildings.Clear();

        foreach (GameObject cityBlock in cityBlocks)
        {
            BuildingDimension();
            if (BuildingWidth != 0 && BuildingHeight != 0 && BuildingLength != 0)
            {
                Vector3 pos = cityBlock.transform.position + new Vector3(0, -5, 0);

                if(ValidPos(CityBlockPos((int)pos.x, (int)pos.z)))
                {
                    GameObject building = GenerateBuilding(BuildingWidth, BuildingHeight, BuildingLength);
                    building.transform.position = pos;
                    building.transform.localScale = new Vector3(BuildingWidth, 1, BuildingLength);
                    StartCoroutine(ScaleBuilding(building, BuildingHeight)); // Scale up based on y value
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
        buildings.Add(building);
        return building;
    }
    void BuildingDimension(GameObject CityCenter)
    {
        foreach(GameObject building in buildings)
        {
            if(DistFromCityCenter < 10)
            {
                BuildingWidth = Random.Range(BuildingMinWidth, BuildingMaxWidth);
                BuildingHeight = Random.Range(BuildingMinHeight, BuildingMaxHeight);
                BuildingLength = Random.Range(BuildingMinLength, BuildingMaxLength);
            }
            else
            {
                BuildingWidth = Random.Range(BuildingMinWidth, BuildingMaxWidth);
                BuildingHeight = Random.Range(BuildingMinHeight, BuildingMaxHeight);
                BuildingLength = Random.Range(BuildingMinLength, BuildingMaxLength);
            }
        }
    }
    Vector3 BuildingPos(int x, int z)
    {
        x = Random.Range(0, cityWidth);
        z = Random.Range(0, cityLength);
        return new Vector3(x, -5, z);
    }
    IEnumerator ScaleBuilding(GameObject building, float targetHeight)
    {
        float currentHeight = 1;
        while (currentHeight < targetHeight)
        {
            currentHeight += Time.deltaTime * 2;
            building.transform.localScale = new Vector3(building.transform.localScale.x, currentHeight, building.transform.localScale.z);
            yield return null;
        }
        building.transform.localScale = new Vector3(building.transform.localScale.x, targetHeight, building.transform.localScale.z);
    }
    void GenerateCityBlocks()
    {
        foreach(GameObject cityBlock in cityBlocks)
        {
            Destroy(cityBlock);
        }
        cityBlocks.Clear();
        for(int i = 0; i<= cityWidth; i++)
        {
            for(int j = 0; j <= cityLength; j++)
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
}
