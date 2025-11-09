using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar las escenas

public class HunterDetection : MonoBehaviour
{
    // Esta funci�n se llama autom�ticamente cuando otro Collider entra en el Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que entr� tiene el Tag "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("�El jugador ha sido detectado! Reiniciando nivel...");
            SceneManager.LoadScene("GameOver_2"); // Cambia a la escena de Game Over
        }
    }

    private void RestartLevel()
    {
        // Obtiene el �ndice de la escena actual.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Vuelve a cargar la escena actual.
        SceneManager.LoadScene(currentSceneIndex);
    }
}