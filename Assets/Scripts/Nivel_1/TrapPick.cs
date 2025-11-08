using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapPick : MonoBehaviour
{
public float velocidadRotacion = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trampa recogida");

            TrapCounter.instance.SumarTrampa();

            Destroy(gameObject);
        }
    }
}
