using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Road Variables")]
    public int RoadWidth;
    public int RoadLength;
    public int RoadHeight;
    public List<GameObject> roads = new List<GameObject>();

    public GameObject GenerateRoad(int RoadWidth, int RoadLength)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);
        roads.Add(road);
        return road;
    }
    public void RoadDimension()
    {
        RoadLength = 5;
        RoadWidth = 5;
    }
    public IEnumerator ScaleRoad(GameObject road, float targetWidth)
    {
        Vector3 TargetScale = new Vector3(targetWidth, road.transform.localScale.y, road.transform.localScale.z);
        while(road.transform.localScale.x < targetWidth)
        {
            road.transform.localScale = Vector3.Lerp(road.transform.localScale, TargetScale, Time.deltaTime * 2);
            yield return null;
        }
        road.transform.localScale = TargetScale;
    }
}
