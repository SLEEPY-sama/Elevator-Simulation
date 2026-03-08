## Elevator Simulation Logic

This project implements a simple **multi-elevator control system** in Unity 2D.
The goal is to simulate how elevators respond to floor requests and move between floors logically.

### System Overview

The simulation consists of three main components:

1. **Floor Buttons** – Trigger elevator requests.
2. **Elevator Manager** – Decides which elevator should respond.
3. **Elevator Units** – Handle movement and request queues.

---

# 1. Floor Request Handling

Each floor contains a button.
When a user presses a button:

* The `FloorButton` script sends a request to the `ElevatorManager`.
* The request includes the **floor number** where the elevator is needed.

```
FloorButton → ElevatorManager.RequestElevator(floor)
```

The manager then decides which elevator should respond.

---

# 2. Elevator Selection Logic

The `ElevatorManager` is responsible for choosing the **most suitable elevator**.

Steps performed:

1. The manager checks **all elevators in the system**.
2. Elevators that are **already idle on the requested floor** are ignored.
3. For each remaining elevator, the distance from the requested floor is calculated.
4. The elevator with the **smallest floor distance** is selected.
5. The request is assigned to that elevator.

Distance is calculated as:

```
distance = | elevatorCurrentFloor - requestedFloor |
```

This ensures:

* Only **one elevator responds** to a request.
* The **nearest elevator is chosen**.

---

# 3. Elevator Request Queue

Each elevator maintains its own **request queue**.

When the manager assigns a request:

```
Elevator.AddRequest(floor)
```

The request is added to the elevator's queue if it is not already present.

This allows elevators to handle **multiple floor requests sequentially**.

Example queue:

```
[2, 3, 1]
```

Meaning the elevator will visit:

* Floor 2
* Floor 3
* Floor 1

in that order.

---

# 4. Elevator Movement

Elevators move smoothly between floors using Unity's `Vector3.MoveTowards`.

Every frame:

1. The elevator checks its request queue.
2. If a request exists, it moves toward the **target floor position**.
3. When the elevator reaches the floor:

   * The current floor value is updated.
   * The completed request is removed from the queue.
   * The elevator becomes idle if no requests remain.

Movement is continuous and does not teleport.

---

# 5. Floor Position System

Each floor has a predefined **Transform position** in the scene.

These positions are stored in:

```
Transform[] floorPoints
```

Example:

```
floorPoints[0] → Ground Floor
floorPoints[1] → Floor 1
floorPoints[2] → Floor 2
floorPoints[3] → Floor 3
```

The elevator moves toward the selected floor position.

---

# 6. Debug Logging

Debug logs are included to help track system behavior.

The console shows:

* When a floor button is pressed
* Which elevator is selected
* When an elevator receives a request
* When an elevator arrives at a floor

Example output:

```
Floor button pressed for floor: 2
Checking Elevator 1 | Current Floor: 0 | Distance: 2
Assigning Elevator 1 to floor request 2
Elevator 1 arrived at floor 2
```

These logs make it easier to debug and demonstrate system behavior.

---

# Summary

The elevator system follows this flow:

Floor Button Press
        ↓
ElevatorManager receives request
        ↓
Nearest elevator is selected
        ↓
Request added to elevator queue
        ↓
Elevator moves smoothly to floor
        ↓
Request completed


This structure keeps the system modular and scalable, allowing additional elevators or floors to be added easily.
