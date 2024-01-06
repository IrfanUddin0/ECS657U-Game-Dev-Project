using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void start_game(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void new_game()
    {
        SaverLoader.DeleteSave();
        start_game();
    }
    
    public void exit_game(){
        Debug.Log("Quit works");
        Application.Quit();
    }
}
