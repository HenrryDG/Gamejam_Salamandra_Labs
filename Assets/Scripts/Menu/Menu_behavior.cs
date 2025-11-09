using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_behavior : MonoBehaviour
{
    //Funcionalidad para botones de menu
    //START
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
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
    //MENU
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    //GAME OVER
    public void BackToGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game_Over");
    }
    //NIVEL 2
    public void BackToNivel2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel_2");
    }
    //NIVEL 1
    public void BackToNivel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel_1");
    }

}
