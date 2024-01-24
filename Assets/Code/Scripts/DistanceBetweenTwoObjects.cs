using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBetweenTwoObjects
{
    public static float CalculateDistanceBetweenObjects(Transform origen, Transform destination)
    {
        float distanceBetweenObjects = Vector3.Distance(origen.position, destination.position);

        return distanceBetweenObjects;
    }

}

