using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyInteraction : MonoBehaviour
{
    public float velocidadRotacion = 100f;
    public AudioClip sonidoRecoger;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llave recogida");
            audioSource.PlayOneShot(sonidoRecoger);

            // Avisar al EndManager que se recogió la llave
            EndManager.instance.OnKeyCollected();

            Destroy(gameObject, 0.2f);
        }
    }
}
