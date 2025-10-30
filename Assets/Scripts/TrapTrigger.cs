using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            // Detener cualquier movimiento o caída 
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            // Mover al jugador al spawn point 
            playerRb.MovePosition(spawnPoint.position);
        }
        else
        {
            player.transform.position = spawnPoint.position;
        }
    }
}