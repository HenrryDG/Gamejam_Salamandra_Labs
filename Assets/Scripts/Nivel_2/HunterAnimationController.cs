using UnityEngine;
 
public class HunterAnimationController : MonoBehaviour
{
    // Arrastra el componente Animator de tu personaje aquí desde el Inspector
    public Animator animator;
 
    // Intervalo de tiempo (en segundos) para la animación de vigilar
    public float minWatchInterval = 5.0f;
    public float maxWatchInterval = 10.0f;
 
    private float watchTimer;
 
    void Start()
    {
        // Si no se asignó el Animator en el Inspector, intenta obtenerlo del mismo GameObject
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
 
        // Inicializa el temporizador por primera vez
        ResetWatchTimer();
    }
 
    void Update()
    {
        // Descuenta el tiempo en cada frame
        watchTimer -= Time.deltaTime;
 
        // Cuando el temporizador llega a cero
        if (watchTimer <= 0)
        {
            // Activa el Trigger en el Animator para cambiar al estado "Vigilar"
            animator.SetTrigger("StartWatch");
            // Reinicia el temporizador para el próximo ciclo
            ResetWatchTimer();
        }
    }
 
    void ResetWatchTimer()
    {
        // Asigna un nuevo valor aleatorio al temporizador dentro del rango definido
        watchTimer = Random.Range(minWatchInterval, maxWatchInterval);
    }
}