using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    // Arrastra tu objeto "SpawnPoint" aquí en el Inspector
    public Transform spawnPoint;

    /// <summary>
    /// Esta función se llama automáticamente por Unity 
    /// cuando otro Collider entra en este Trigger.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entró tiene el Tag "Player"
        if (other.CompareTag("Player"))
        {
            // ¡Es el jugador! Ahora lo reiniciamos.
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Primero, intentamos obtener el Rigidbody del jugador
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            // 1. Detenemos cualquier movimiento o caída (MUY IMPORTANTE)
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            // 2. Movemos al jugador al spawn point usando el método
            //    seguro para físicas (MovePosition)
            playerRb.MovePosition(spawnPoint.position);
        }
        else
        {
            // Fallback si el jugador no tiene Rigidbody 
            // (aunque el tuyo sí lo tiene)
            player.transform.position = spawnPoint.position;
        }
    }
}