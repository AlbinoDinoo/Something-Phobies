
using UnityEngine;

public class WormSwarmManager : MonoBehaviour
{
    [Header("References")]
    public GameObject WormPrefab;
    public Transform SwarmCenter;

    [Header("Swarm Settings")]
    public int WormCount = 10;
    public float SpawnRadius = 3f;

    [Header("Movement")]
    public float CenterSpeed = 3f;

    private Transform Player;

    private void Start()
    {
        GameObject PlayerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (PlayerObject != null)
            Player = PlayerObject.transform;

        SpawnWorms();
    }

    private void Update()
    {
        if (Player == null || SwarmCenter == null)
            return;

        Vector3 TargetPosition = Player.position;
        TargetPosition.y = SwarmCenter.position.y;

        SwarmCenter.position = Vector3.MoveTowards(
            SwarmCenter.position,
            TargetPosition,
            CenterSpeed * Time.deltaTime
        );
    }

    private void SpawnWorms()
    {
        if (WormPrefab == null || SwarmCenter == null)
        {
            Debug.LogError("Assign WormPrefab and SwarmCenter!");
            return;
        }

        for (int i = 0; i < WormCount; i++)
        {
            Vector2 RandomPosition =
                Random.insideUnitCircle * SpawnRadius;

            Vector3 SpawnPosition = SwarmCenter.position +
                new Vector3(RandomPosition.x, 0f, RandomPosition.y);

            GameObject NewWorm = Instantiate(
                WormPrefab,
                SpawnPosition,
                Quaternion.identity
            );

            WormSimulation Simulation =
                NewWorm.GetComponent<WormSimulation>();

            if (Simulation != null)
            {
                Simulation.SwarmCenter = SwarmCenter;
            }
        }
    }
}
