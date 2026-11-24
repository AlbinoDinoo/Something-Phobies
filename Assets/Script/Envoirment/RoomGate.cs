
using UnityEngine;

public class RoomGate : MonoBehaviour
{
    private InfiniteRoomGenerator generator;
    private GameObject roomRoot;
    private GameObject previousRoom;
    private bool isExit;
    private bool activated;

    public void Setup(
        InfiniteRoomGenerator roomGenerator,
        GameObject currentRoom,
        bool exitGate,
        GameObject oldRoom)
    {
        generator = roomGenerator;
        roomRoot = currentRoom;
        isExit = exitGate;
        previousRoom = oldRoom;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        activated = true;

        if (isExit)
        {
            generator.GenerateNextRoom(
                roomRoot,
                transform
            );
        }
        else
        {
            generator.OnPlayerEnteredRoom(previousRoom);
        }
    }
}
