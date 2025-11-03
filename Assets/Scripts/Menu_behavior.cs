using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_behavior : MonoBehaviour
{
//Funcionalidad para botones de menu
//START
public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel_1");
    }
//ABOUT
public void AboutGame()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("About");
}
//EXIT
public void QuitGame()
{
    Application.Quit();
    Debug.Log("Quit");
}

}
