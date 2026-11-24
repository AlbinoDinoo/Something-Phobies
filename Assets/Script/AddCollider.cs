
using UnityEngine;

public class AddCollidersToRoom : MonoBehaviour
{
    private void Awake()
    {
        Collider[] existingColliders =
            GetComponentsInChildren<Collider>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Collider>() != null)
                continue;

            MeshFilter meshFilter = child.GetComponent<MeshFilter>();

            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                MeshCollider meshCollider =
                    child.gameObject.AddComponent<MeshCollider>();

                meshCollider.sharedMesh = meshFilter.sharedMesh;
            }
        }
    }
}
