using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int elevatorID;
    public float speed = 2f;

    public int currentFloor = 0;
    public bool isMoving = false;

    public List<int> requestQueue = new List<int>();

    public Transform[] floorPoints;

    // This locks the elevator to a target until it arrives
    private int currentTargetFloor = -1;

    void Update()
    {
        // If elevator is idle and there are requests
        if (!isMoving && requestQueue.Count > 0)
        {
            // Pick the first request in queue
            currentTargetFloor = requestQueue[0];

            Debug.Log("Elevator " + elevatorID +
                      " starting movement to floor " + currentTargetFloor);

            isMoving = true;
        }

        // If currently moving toward a target
        if (isMoving && currentTargetFloor != -1)
        {
            MoveToFloor(currentTargetFloor);
        }
    }

    public void AddRequest(int floor)
    {
        if (!requestQueue.Contains(floor))
        {
            requestQueue.Add(floor);

            Debug.Log("Elevator " + elevatorID +
                      " received request for floor " + floor +
                      ". Queue size: " + requestQueue.Count);
        }
    }

    void MoveToFloor(int floor)
    {
        Vector3 target = floorPoints[floor].position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            currentFloor = floor;

            Debug.Log("Elevator " + elevatorID +
                      " arrived at floor " + currentFloor);

            // Remove completed request
            requestQueue.RemoveAt(0);

            // Reset target
            currentTargetFloor = -1;

            // Stop movement until next request starts
            isMoving = false;

            Debug.Log("Elevator " + elevatorID +
                      " remaining queue count: " + requestQueue.Count);
        }
    }

    public int DistanceFromFloor(int floor)
    {
        return Mathf.Abs(currentFloor - floor);
    }
}