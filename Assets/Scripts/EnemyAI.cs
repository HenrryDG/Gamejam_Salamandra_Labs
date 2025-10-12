using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Objetivo y Patrulla")]
    public Transform target; // El objetivo que el enemigo debe seguir
    public Transform[] patrolPoints; // Puntos dentro de la zona marcada

    [Header("Configuración de movimiento")]
    public float movementSpeed = 3.0f; // Velocidad de movimiento del enemigo
    public float chaseRange = 20.0f;   // Distancia a la que empieza a seguir al jugador
    public float stopChasingRange = 15.0f; // Distancia a la que deja de seguir

    private int currentPatrolIndex = 0;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;

        if (patrolPoints.Length > 0)
        {
            SetDestinationToNextPatrolPoint();
        }
        else
        {
            Debug.LogWarning("No se han asignado puntos de patrulla.");
        }
    }

    void Update()
    {
        if (!navMeshAgent.isOnNavMesh)
            return; // Evita errores si el agente no está sobre el NavMesh

        // Si hay objetivo, calculamos la distancia
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Si está dentro del rango de persecución → seguir
            if (!isChasing && distanceToTarget <= chaseRange)
            {
                isChasing = true;
                Debug.Log("Iniciando persecución");
            }
            // Si ya está persiguiendo pero el jugador se aleja mucho → volver a patrullar
            else if (isChasing && distanceToTarget > stopChasingRange)
            {
                isChasing = false;
                Debug.Log("Jugador fuera de rango, volviendo a patrulla");
                SetDestinationToNextPatrolPoint();
            }
        }

        // Comportamiento según el estado
        if (isChasing && target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            // Patrulla normal
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                SetDestinationToNextPatrolPoint();
            }
        }
    }

    void SetDestinationToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja visualmente los rangos en la escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopChasingRange);
    }
}
