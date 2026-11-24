
using UnityEngine;

public class InfiniteRoomGenerator : MonoBehaviour
{
    [Header("Room Prefabs")]
    public GameObject startingRoom;

    public GameObject player;

    public GameObject[] randomRooms;

  private void Start()
    {
        Debug.Log("GENERATOR STARTED");

        if (startingRoom == null)
        {
            Debug.LogError("STARTING ROOM IS NOT ASSIGNED!");
            return;
        }

        GameObject firstRoom = Instantiate(
            startingRoom,
            transform.position,
            transform.rotation
        );

        Debug.Log("ROOM CREATED");

        Transform entry = firstRoom.transform.Find("EntryPlace");

        if (entry == null)
        {
            Debug.LogError("ENTRYPLACE NOT FOUND!");
            return;
        }

        Debug.Log("ENTRYPLACE FOUND AT: " + entry.position);

        if (player == null)
        {
            Debug.LogError("PLAYER FIELD IS EMPTY!");
            return;
        }

        Debug.Log("PLAYER REFERENCE FOUND: " + player.name);

        GameObject spawnedPlayer = Instantiate(
        player,
        entry.position + entry.forward * 2f,
        entry.rotation
        );

        spawnedPlayer.name = "Blender Spide";

        Debug.Log("PLAYER POSITION: " + player.transform.position);
        Debug.Log("PLAYER SPAWNED!");
    }

    public void GenerateNextRoom(
        GameObject previousRoom,
        Transform exitPoint)
    {
        if (randomRooms.Length == 0)
            return;

        // Select a random room
        GameObject selectedRoom =
            randomRooms[Random.Range(0, randomRooms.Length)];

        // Spawn it at the previous room's exit
        GameObject newRoom = Instantiate(
            selectedRoom,
            exitPoint.position,
            exitPoint.rotation
        );

        Transform entry = newRoom.transform.Find("EntryPlace");

        if (entry == null)
        {
            Debug.LogError("New room has no EntryPoint!");
            Destroy(newRoom);
            return;
        }

        // Align the new room's entrance with the old exit
        newRoom.transform.rotation =
            exitPoint.rotation * Quaternion.Inverse(entry.localRotation);

        newRoom.transform.position +=
            exitPoint.position - entry.position;

        SetupRoom(newRoom, previousRoom);
    }

    private void SetupRoom(
        GameObject room,
        GameObject previousRoom)
    {
        Transform exit = room.transform.Find("ExitPlace");
        Transform entry = room.transform.Find("EntryPlace");

        if (exit == null || entry == null)
        {
            Debug.LogError(
                room.name + " needs EntryPoint and ExitPoint!"
            );
            return;
        }

        // Set up exit trigger
        BoxCollider exitCollider =
            exit.GetComponent<BoxCollider>();

        if (exitCollider == null)
            exitCollider = exit.gameObject.AddComponent<BoxCollider>();

        exitCollider.isTrigger = true;
        exitCollider.size = new Vector3(2, 3, 1);

        Rigidbody exitBody =
            exit.GetComponent<Rigidbody>();

        if (exitBody == null)
            exitBody = exit.gameObject.AddComponent<Rigidbody>();

        exitBody.isKinematic = true;
        exitBody.useGravity = false;

        RoomGate exitGate =
            exit.GetComponent<RoomGate>();

        if (exitGate == null)
            exitGate = exit.gameObject.AddComponent<RoomGate>();

        exitGate.Setup(this, room, true, null);

        // Create an entry trigger slightly inside the room
        GameObject entryTrigger = new GameObject("EntryTrigger");

        entryTrigger.transform.SetParent(room.transform);
        entryTrigger.transform.position =
            entry.position + entry.forward * 2f;

        BoxCollider entryCollider =
            entryTrigger.AddComponent<BoxCollider>();

        entryCollider.isTrigger = true;
        entryCollider.size = new Vector3(2, 3, 1);

        Rigidbody entryBody =
            entryTrigger.AddComponent<Rigidbody>();

        entryBody.isKinematic = true;
        entryBody.useGravity = false;

        RoomGate entryGate =
            entryTrigger.AddComponent<RoomGate>();

        entryGate.Setup(this, room, false, previousRoom);
    }

    public void OnPlayerEnteredRoom(GameObject previousRoom)
    {
        if (previousRoom != null)
            Destroy(previousRoom);
    }
}
