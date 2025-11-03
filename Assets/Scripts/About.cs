using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class About : MonoBehaviour
{
	[Header("Images (ordenadas)")]
	[Tooltip("Arrastra aquí las Images en el orden en que deben mostrarse (primera, segunda, tercera)")]
	public Image[] images;

	[Header("Buttons")]
	[Tooltip("Botón 'Anterior' que se activará al llegar a la tercera imagen (opcional)")]
	public Button previousButton;

	[Tooltip("Botón 'Siguiente' (opcional)")]
	public Button nextButton;

	int currentIndex = 0;

	void Awake()
	{
		// Validaciones mínimas
		if (images == null || images.Length == 0)
		{
			Debug.LogWarning("About: no se han asignado Images en el inspector.");
			return;
		}

		// Asegurar que solo la primera imagen esté activa al inicio
		for (int i = 0; i < images.Length; i++)
		{
			if (images[i] != null)
				images[i].gameObject.SetActive(i == 0);
		}

		currentIndex = 0;

		// Inicialmente el botón anterior está oculto (según requisitos)
		if (previousButton != null)
			previousButton.gameObject.SetActive(false);

		// nextButton se deja tal cual; opcionalmente se puede desactivar al llegar al final
	}

	// Llamar desde el botón "Siguiente" (OnClick) o desde código
	public void Next()
	{
		if (images == null || images.Length == 0)
			return;

		if (currentIndex >= images.Length - 1)
			return; // ya estamos en la última

		// ocultar actual
		if (images[currentIndex] != null)
			images[currentIndex].gameObject.SetActive(false);

		currentIndex++;

		// mostrar siguiente
		if (images[currentIndex] != null)
			images[currentIndex].gameObject.SetActive(true);

		// Si llegamos a la tercera (última) activamos el botón "Anterior"
		if (currentIndex == images.Length - 1)
		{
			if (previousButton != null)
				previousButton.gameObject.SetActive(true);

			// opcional: desactivar nextButton si no quieres pasar más
			if (nextButton != null)
				nextButton.interactable = false;
		}
	}

	// Llamar desde el botón "Anterior" (OnClick) o desde código
	public void Previous()
	{
		if (images == null || images.Length == 0)
			return;

		if (currentIndex <= 0)
			return; // ya estamos en la primera

		// ocultar actual
		if (images[currentIndex] != null)
			images[currentIndex].gameObject.SetActive(false);

		currentIndex--;

		// mostrar anterior
		if (images[currentIndex] != null)
			images[currentIndex].gameObject.SetActive(true);

		// Si volvemos a la primera, ocultamos el botón anterior
		if (currentIndex == 0)
		{
			if (previousButton != null)
				previousButton.gameObject.SetActive(false);

			// reactivar nextButton si estaba desactivado
			if (nextButton != null)
				nextButton.interactable = true;
		}
		else
		{
			// Si estamos en una posición intermedia, aseguramos que next esté activo
			if (nextButton != null)
				nextButton.interactable = true;
		}
	}
}
