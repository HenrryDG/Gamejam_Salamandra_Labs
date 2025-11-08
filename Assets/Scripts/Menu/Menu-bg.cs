using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script para adjuntar a la Image de fondo. Al hacer click en la imagen de
// fondo restaurará su color original y ocultará dos Images configuradas.
public class MenuBg : MonoBehaviour, IPointerClickHandler
{
	[Header("References")]
	[Tooltip("Image que se restaurará")]
	public Image backgroundImage;

	[Tooltip("Primera Image que se debe ocultar al hacer click")]
	public Image imageToHide;

	[Tooltip("Segunda Image que se debe ocultar al hacer click")]
	public Image imageToHide2;

	Color originalColor = Color.white;

	void Awake()
	{
		if (backgroundImage == null)
		{
			// Si no se asignó explicitamente, intentar obtener la Image del mismo GameObject
			backgroundImage = GetComponent<Image>();
		}

		if (backgroundImage != null)
		{
			originalColor = backgroundImage.color;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// Restaurar color original
		if (backgroundImage != null)
		{
			backgroundImage.color = originalColor;
		}

		// Ocultar las imágenes configuradas
		if (imageToHide != null)
			imageToHide.gameObject.SetActive(false);

		if (imageToHide2 != null)
			imageToHide2.gameObject.SetActive(false);
	}

	// Método público - en caso de necesitar boton
	public void Restore()
	{
		OnPointerClick(null);
	}
}
