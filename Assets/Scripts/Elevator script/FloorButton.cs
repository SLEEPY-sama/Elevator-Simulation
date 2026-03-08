using UnityEngine;

public class FloorButton : MonoBehaviour
{
    // The floor number this button represents
    // Example: Ground = 0, Floor1 = 1, Floor2 = 2, Floor3 = 3
    public int floorNumber;

    // Reference to the ElevatorManager
    // This manager decides which elevator should respond
    public ElevatorManager manager;

    // This function is triggered when the UI button is pressed
    // Connect this in the Unity Button -> OnClick() event
    public void CallElevator()
    {
        // Log which floor button was pressed
        Debug.Log("Floor button pressed for floor: " + floorNumber);

        // Safety check to make sure manager reference exists
        if (manager != null)
        {
            Debug.Log("Sending elevator request to ElevatorManager for floor " + floorNumber);

            // Send request to the manager so it can assign the best elevator
            manager.RequestElevator(floorNumber);
        }
        else
        {
            // Warning in case the manager is not assigned in inspector
            Debug.LogWarning("ElevatorManager reference is missing on FloorButton!");
        }
    }
}