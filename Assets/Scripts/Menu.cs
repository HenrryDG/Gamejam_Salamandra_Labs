using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Menu : MonoBehaviour, IPointerClickHandler
{
	[Header("References")]
	[Tooltip("Image que se oscurecerá")]
	public Image backgroundImage;

	[Tooltip("Image que está oculta y debe aparecer al hacer click")]
	public Image imageToShow;

    [Tooltip("Image que está se debe ocultar al hacer click")]
	public Image imageToHide;

	[Header("Darken settings")]
	[Range(0f, 1f)]
	[Tooltip("0 = sin cambio, 1 = negro completo")] 
	public float darkenAmount = 0.5f;

	Color originalBackgroundColor = Color.white;

	void Awake()
	{
		if (imageToShow != null)
			imageToShow.gameObject.SetActive(false);
		if (backgroundImage != null)
			originalBackgroundColor = backgroundImage.color;            
	}

	// IPointerClickHandler: recibe el click cuando este GameObject es clickeado.
	public void OnPointerClick(PointerEventData eventData)
	{
		// Oscurecer el fondo mezclando con negro según darkenAmount
		if (backgroundImage != null)
		{
			backgroundImage.color = Color.Lerp(originalBackgroundColor, Color.black, Mathf.Clamp01(darkenAmount));
		}

		// Mostrar la imagen oculta (activar su GameObject)
		if (imageToShow != null)
		{
			imageToShow.gameObject.SetActive(true);
		}

		// Ocultar la imagen que se debe ocultar (desactivar su GameObject)
		if (imageToHide != null)
		{
			imageToHide.gameObject.SetActive(false);
		}
	}

	// Método público opcional para conectar desde un Button onClick en caso de preferirlo
	public void OnClick()
	{
		OnPointerClick(null);
	}
}
