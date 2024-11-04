using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
   [Header("Building Generation")]
    public Building buildingPrefab;
    public float BuildingSpacing = 10f;
    public List<Building> buildings = new List<Building>();

    public void GenerateBuildings(float CityWidth, float CityLength)
    {
        Debug.Log("Generating Buildings");

        foreach (Building building in buildings)
        {
            Destroy(building.gameObject);
        }
        buildings.Clear();
        for (int i = 0; i < CityWidth; i += (int)BuildingSpacing)
        {
            for (int j = 0; j < CityLength; j += (int)BuildingSpacing)
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
