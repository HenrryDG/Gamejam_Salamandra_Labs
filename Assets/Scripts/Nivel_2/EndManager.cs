using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;

    [Header("Referencias")]
    public GameObject objetoParaNoquear;   // El objeto que estará DESACTIVADO al inicio
    public TMP_Text textoEstado;           // El texto del canvas (compartido para llave y objeto)

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // El objeto NO aparece hasta obtener la llave
        if (objetoParaNoquear != null)
            objetoParaNoquear.SetActive(false);

        if (textoEstado != null)
        {
            textoEstado.text = "Encontrar la llave para liberar a Qori";
            textoEstado.color = Color.white;
        }
    }

    public void OnKeyCollected()
    {
        // Mostrar texto del siguiente objetivo
        textoEstado.text = "Encuentra un objeto para noquear al cazador";
        textoEstado.color = Color.white;

        // Activar el objeto
        objetoParaNoquear.SetActive(true);
    }

    public void OnObjectCollected()
    {
        textoEstado.text = "Objeto encontrado";
        textoEstado.color = Color.green;

        // Cargar escena final
        SceneManager.LoadScene("End");
    }
}
