using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Gestion de escenas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int stage; //Comprobar en que nivel estamos 
    private int lives = 3;
    private int deadBirds = 0;
    private int aliveBirds = 0;
    private int score = 0;
   // private UIManager theUIManager;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    //public void SetUIManager(UIManager uim)
    //{
    //    theUIManager = uim;
    //    uim.Init(lives, enemiesInLevel, 0);
    //}

    public void BirdDied(int destructionPoints)
    {
        aliveBirds--;
        deadBirds++;
        Debug.Log("Birds Remaining = " + aliveBirds);
        //if (theUIManager != null) theUIManager.RemoveEnemy(levelScore);
        score += destructionPoints;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddBird()
    {
        aliveBirds++;
        Debug.Log("Nuevo Bird = " + aliveBirds);
    }

    private void GameOver() //menu
    {
        score = 0;
        deadBirds = 0;
        aliveBirds = 0;
        //ChangeScene("Menu");
    }
}
