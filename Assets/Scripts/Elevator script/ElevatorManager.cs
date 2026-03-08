using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    // Array storing all elevators present in the system
   
    public Elevator[] elevators;

    // This function is called when a floor button is pressed
    // It decides which elevator should respond
    public void RequestElevator(int floor)
    {
        Debug.Log("Floor " + floor + " requested.");

        // Variable to store the best elevator candidate
        Elevator bestElevator = null;

        // Stores shortest distance found so far
        int bestDistance = int.MaxValue;

        // Loop through all elevators to find the nearest one
        foreach (Elevator elevator in elevators)
        {
            // Skip elevator if it is already idle on the requested floor
            // No need to send it again
            if (elevator.currentFloor == floor && !elevator.isMoving)
            {
                Debug.Log("Elevator " + elevator.elevatorID +
                          " is already on floor " + floor + " and idle. Skipping.");
                continue;
            }

            // Calculate distance between elevator and requested floor
            int distance = elevator.DistanceFromFloor(floor);

            Debug.Log("Checking Elevator " + elevator.elevatorID +
                      " | Current Floor: " + elevator.currentFloor +
                      " | Distance: " + distance);

            // Choose elevator with minimum distance
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestElevator = elevator;
            }
        }

        // If we found a suitable elevator
        if (bestElevator != null)
        {
            Debug.Log("Assigning Elevator " + bestElevator.elevatorID +
                      " to floor request " + floor);

            // Add the floor request to that elevator's queue
            bestElevator.AddRequest(floor);
        }
        else
        {
            Debug.LogWarning("No elevator available for floor request " + floor);
        }
    }
}