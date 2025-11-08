using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class KeyInteraction : MonoBehaviour
{
    public float velocidadRotacion = 100f;
    public AudioClip sonidoRecoger;          // El sonido que reproducirá
    public TMP_Text textoEstadoLlave;        // Referencia al texto en pantalla

    private AudioSource audioSource;         // Para reproducir sonido

    private void Start()
    {
        // Crear un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Rotación constante
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llave recogida");

            // Reproducir sonido
            audioSource.PlayOneShot(sonidoRecoger);

            // Actualizar texto del Canvas
            if (textoEstadoLlave != null)
            {
                textoEstadoLlave.text = "Llave Encontrada";          // o "Llave recogida"
                textoEstadoLlave.color = Color.green;
            }

            // Destruir la llave luego de un pequeño delay para que el audio suene
            Destroy(gameObject, 0.2f);
        }
    }
}
