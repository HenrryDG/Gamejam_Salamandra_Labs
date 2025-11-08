using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrapCounter : MonoBehaviour
{
    public static TrapCounter instance;

    public TextMeshProUGUI conteoTexto;
    public int totalTrampas = 5;

    public int trampasRecogidas = 0;
    public int TrampasRecogidas => trampasRecogidas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActualizarTexto();
    }

    public void SumarTrampa()
    {
        trampasRecogidas++;
        ActualizarTexto();

        // ✅ Si llegó al máximo, avanzar de nivel
        if (trampasRecogidas >= totalTrampas)
        {
            // esperar 3 segundos antes de cargar el siguiente nivel
            StartCoroutine(EsperarYCargarSiguienteNivel());
        }
    }

    private void ActualizarTexto()
    {
        conteoTexto.text = $"Trampas: {trampasRecogidas}/{totalTrampas}";
    }

    private void CargarSiguienteNivel()
    {
        Debug.Log("¡Todas las trampas recogidas! Cargando Nivel_2...");
        SceneManager.LoadScene("Nivel_2");
    }
    private IEnumerator EsperarYCargarSiguienteNivel()
    {
        yield return new WaitForSeconds(2f);
        CargarSiguienteNivel();
    }
}
