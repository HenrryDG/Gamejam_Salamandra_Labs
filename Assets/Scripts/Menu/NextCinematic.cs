using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCinematic : MonoBehaviour
{
//Serializable para nombre de siguiente escena
[SerializeField]
private string nextSceneName;

//SIGUIENTE ESCENA
public void NextScene()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
}
}