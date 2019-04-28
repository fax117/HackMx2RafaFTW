using UnityEngine;
using UnityEngine.SceneManagement;


public class GamerManager : MonoBehaviour
{
    public bool gameHasEnded = false;
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GG");
            Restart();
        } 
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
