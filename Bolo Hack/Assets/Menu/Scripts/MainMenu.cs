using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void EndlessPlay(){
        SceneManager.LoadScene(4);
    }

    public void BrickPlay(){
        SceneManager.LoadScene(2);
    }

    public void MatchPlay(){
        SceneManager.LoadScene(1);
    }

    public void ARPlay(){
        SceneManager.LoadScene(3);
    }

    public void Bolo() {
        SceneManager.LoadScene(5);
    } 
}
