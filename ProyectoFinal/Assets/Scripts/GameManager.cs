using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Gestion de escenas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] pajaros;
    private int deadBirds = 0;
    private int aliveBirds = 0;
    private int score = 0;
    private int shots = 0;
    private UIManager uiManager;
    bool ramon = true;

    void Awake()
    {
        if (instance == null)
        {
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
            ramon = false;
        }
    }

    public void BirdDied(int destructionPoints)
    {
        //INTERFAZ------------------
        aliveBirds--;
        deadBirds++;
        Debug.Log("Birds Remaining = " + aliveBirds);
        score += destructionPoints;
        if (uiManager != null)
        {
            uiManager.RemoveBird(deadBirds, aliveBirds, score);
        }
    }

    public void NormalBirdDied(int destructionPoints, Vector3 pos)
    {
        BirdDied(destructionPoints);
        //COMPORTAMIENTO-------------
        for (int i = 0; i < pajaros.Length; i++)
        {
            GameObject bird = pajaros[i];
            if (pajaros[i].active)
            {
                BirdNavMesh nav = bird.GetComponent<BirdNavMesh>();
                PajaroSeguir seguirAJefe = bird.GetComponent<PajaroSeguir>();
                if (nav == null || seguirAJefe == null)
                    Debug.Log("GM encontro un pajaro sin BirdNavMesh/PajaroSeguir");
                else
                {
                    Debug.Log("Normallllll");
                    // DEJA DE SEGUIR AL JEFE ==> HA MUERTO
                    seguirAJefe.enabled = false;
                    //EMPIEZA A MOVERSE RANDOM
                    nav.enabled = true;
                    nav.Huir(pos);
                }
            }
        }
    }

    public void PajaroJefeDied(int destructionPoints)
    {
        //Debug.Log("JAJAJJAA");
        BirdDied(destructionPoints);
        for (int i = 0; i < pajaros.Length; i++)
        {
            GameObject bird = pajaros[i];
            if (pajaros[i].active)
            {
                BirdNavMesh nav = bird.GetComponent<BirdNavMesh>();
                PajaroSeguir seguirAJefe = bird.GetComponent<PajaroSeguir>();
                if (nav == null || seguirAJefe == null)
                    Debug.Log("GM encontro un pajaro sin BirdNavMesh/PajaroSeguir");
                else
                {
                    //DEJA DE SEGUIR AL JEFE ==> HA MUERTO
                    seguirAJefe.enabled = false;
                    //EMPIEZA A MOVERSE RANDOM
                    nav.enabled = true;
                    //nav.Huir(pos);
                }
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddBird()
    {
        aliveBirds++;
        // Debug.Log("Nuevo Bird = " + aliveBirds);
    }

    public void AddBullet()
    {
        shots++;
        uiManager.AmountShots(shots);
    }

    private void GameOver() //menu
    {
        score = 0;
        deadBirds = 0;
        aliveBirds = 0;
        //ChangeScene("Menu");
    }
}