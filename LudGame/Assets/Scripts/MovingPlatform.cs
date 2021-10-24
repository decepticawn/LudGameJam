using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] platformWayPoints;
    [SerializeField] private int currentPlatformWayPointIndex;
    [SerializeField] private float timeToMoveToNextPoint = 2f;

    private void Start()
    {
        transform.localPosition = platformWayPoints[currentPlatformWayPointIndex].localPosition;
        currentPlatformWayPointIndex++;
        currentPlatformWayPointIndex %= platformWayPoints.Length;
        MovePlatform();
    }

    private void MovePlatform()
    {
        transform.LeanMoveLocal(platformWayPoints[currentPlatformWayPointIndex].localPosition,
            timeToMoveToNextPoint).setOnComplete(
            () =>
            {
                currentPlatformWayPointIndex++;
                currentPlatformWayPointIndex %= platformWayPoints.Length;
                MovePlatform();
            });
    }
}
