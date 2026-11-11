using UnityEngine;
using UnityEngine.SceneManagement;

public class Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
