using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [Header("Building Generation")]
    public Building buildingPrefab;
    public float BuildingSpacing;
    public List<Building> buildings = new List<Building>();
    void Start()
    {
        OnDrawGizmos();
    }
    public void GenerateBuildings(float CityWidth, float CityLength,RoadGenerator roadGenerator)
    {
        Debug.Log("Generating Buildings");

        foreach (Building building in buildings)
        {
            Destroy(building.gameObject);
        }
        buildings.Clear();
        for (int i = (int)roadGenerator.RoadWidth; i < CityWidth; i += (int)BuildingSpacing)
        {
            for (int j = (int)roadGenerator.RoadWidth; j < CityLength; j += (int)BuildingSpacing)
            {
                if(!IsAvailible(new Vector3(i, 0, j), roadGenerator))
                {
                    Vector3 position = new Vector3(i, 0, j);
                    Building newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
                
                    newBuilding.buildingType = (Building.BuildingType)Random.Range(0, 4);
                    newBuilding.SelectType(newBuilding.buildingType);

                    newBuilding.transform.localScale = newBuilding.GenerateSize();
                    buildings.Add(newBuilding);
                }
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Building building in buildings)
        {
            Gizmos.DrawWireCube(building.transform.position, building.GenerateSize());
        }
    }
    bool IsAvailible(Vector3 position, RoadGenerator roadGenerator)
    {
        float minDistance = roadGenerator.RoadWidth + BuildingSpacing;
        foreach (GameObject road in roadGenerator.roads)
        {
            if (Vector3.Distance(position, road.transform.position) <= minDistance)
            {
                Debug.Log("Building too close to road");
                return false;
            }
        }
        return true;
    }
}
