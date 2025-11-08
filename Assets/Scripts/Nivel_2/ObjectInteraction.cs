using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    public float velocidadRotacion = 100f;
    public AudioClip sonidoRecoger;
    public TMP_Text textoEstadoObjeto;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (textoEstadoObjeto != null)
        {
            textoEstadoObjeto.text = "Encuentra un objeto para noquear al cazador";
            textoEstadoObjeto.color = Color.white;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.left * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objeto recogido");

            if (sonidoRecoger != null)
                audioSource.PlayOneShot(sonidoRecoger);

            if (textoEstadoObjeto != null)
            {
                textoEstadoObjeto.text = "Objeto encontrado";
                textoEstadoObjeto.color = Color.green;
            }

            Destroy(gameObject, 0.2f);
        }
    }
}
