using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [Header("Building Variables")]
    public int BuildingWidth;
    public int BuildingHeight;
    public int BuildingLength;
    public int BuildingMinWidth = 1;
    public int BuildingMaxWidth = 3;
    public int BuildingMinHeight = 1;
    public int BuildingMaxHeight = 5;
    public int BuildingMaxLength = 3;
    public int BuildingMinLength = 1;
    public float DistFromCityCenter;
    public List<GameObject> buildings = new List<GameObject>();
    public GameObject GenerateBuilding(int BuildingWidth, int BuildingHeight, int BuildingLength)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildings.Add(building);
        return building;
    }
    public void BuildingDimension()
    {
        foreach(GameObject building in buildings)
        {
            BuildingWidth = Random.Range(BuildingMinWidth, BuildingMaxWidth);
            BuildingHeight = Random.Range(BuildingMinHeight, BuildingMaxHeight);
            BuildingLength = Random.Range(BuildingMinLength, BuildingMaxLength);
            Debug.Log("FROM CLASS: Building Width: " + BuildingWidth + " Building Height: " + BuildingHeight + " Building Length: " + BuildingLength);
        }
    }
    public IEnumerator ScaleBuilding(GameObject building, float targetHeight)
    {
        Vector3 TargetScale = new Vector3(building.transform.localScale.x, targetHeight, building.transform.localScale.z);
        while (building.transform.localScale.y < targetHeight)
        {
            building.transform.localScale = Vector3.Lerp(building.transform.localScale, TargetScale, Time.deltaTime * 2);
            yield return null;
        }
        building.transform.localScale = TargetScale;
    }
}
