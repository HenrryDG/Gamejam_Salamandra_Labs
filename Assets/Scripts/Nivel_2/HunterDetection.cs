using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar las escenas

public class HunterDetection : MonoBehaviour
{
    // Esta función se llama automáticamente cuando otro Collider entra en el Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que entró tiene el Tag "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha sido detectado! Reiniciando nivel...");
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        // Obtiene el índice de la escena actual.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Vuelve a cargar la escena actual.
        SceneManager.LoadScene(currentSceneIndex);
    }
}