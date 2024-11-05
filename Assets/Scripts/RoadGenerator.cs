using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoadGenerator : MonoBehaviour
{
    public float RoadWidth = 5f;
    public float RoadSpacing = 10f;
    public float RoadHeight = 0.1f; 
    public GameObject roadPrefab;
    public List<GameObject> roads = new List<GameObject>();
    public void GenerateRoads(float CityWidth, float CityLength, BuildingGenerator buildingGenerator)
    {
        Debug.Log("Generating Roads");
        foreach (GameObject road in roads)
        {
            Destroy(road);
        }
        roads.Clear();

        for (int i = 0; i < CityWidth; i++)
        {
            for (int j = 0; j < CityLength; j++)
            {
                if (i % RoadSpacing == 0 || j % RoadSpacing == 0)
                {
                    GameObject road = Instantiate(roadPrefab, new Vector3(i, RoadHeight / 2, j), Quaternion.identity);
                    road.transform.localScale = new Vector3(RoadWidth, RoadHeight, RoadWidth);
                    roads.Add(road);
                }
            }
        }
    }
}
