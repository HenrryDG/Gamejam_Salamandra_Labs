using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public Light lightningLight;
    public AudioSource thunderSound;

    public float minDelay = 10;
    public float maxDelay = 15f;

    void Start()
    {
        lightningLight.intensity = 0;
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);

            // Flash 1
            lightningLight.intensity = 4f;
            yield return new WaitForSeconds(0.05f);
            lightningLight.intensity = 0f;

            yield return new WaitForSeconds(0.1f);

            // Flash 2 (para hacerlo más real)
            lightningLight.intensity = 6f;
            yield return new WaitForSeconds(0.08f);
            lightningLight.intensity = 0f;

            // El trueno suena un poco después del flash
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
            thunderSound.Play();
        }
    }
}
