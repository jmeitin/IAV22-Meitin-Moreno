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
    private int shots = 0;
    private UIManager uiManager;
    bool ramon = true;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }


    public void SetUIManager(UIManager uim)
    {
        uiManager = uim;
        //uim.Init(score, deadBirds, aliveBirds);
    }
    private void Update()
    {
        if (ramon == true)
        {
            uiManager.Init(score, deadBirds, aliveBirds);
        }
    }

    public void BirdDied(int destructionPoints)
    {
        aliveBirds--;
        deadBirds++;
        Debug.Log("Birds Remaining = " + aliveBirds);
        score += destructionPoints;
        if (uiManager != null)
        {
            uiManager.RemoveBird(deadBirds, aliveBirds, score, shots);
        }
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
