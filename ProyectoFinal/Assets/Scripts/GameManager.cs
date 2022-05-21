using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Gestion de escenas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] pajaros;
    bool jefeIsAlive = true;
    private int deadBirds = 0;
    private int aliveBirds = 0;
    private int score = 0;
    private int shots = 0;
    [SerializeField] private int spray = 3;
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

    private void Start()
    {
        if (pajaros.Length == 0)
            Debug.Log("Asocia los pajaros al GM");
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
            uiManager.Init(score, deadBirds, aliveBirds, spray);
            ramon = false;
        }
    }

    public void BirdDied(int destructionPoints, Vector3 pos, bool jefeDied)
    {
        if (jefeDied)
        {
            jefeIsAlive = false;
            Debug.Log("HA MUERTO EL JEFE");
        }
        else Debug.Log("HA MUERTO UN MINION");

        //INTERFAZ------------------
        aliveBirds--;
        deadBirds++;
        //Debug.Log("Birds Remaining = " + aliveBirds);
        score += destructionPoints;
        if (uiManager != null)
        {
            uiManager.RemoveBird(deadBirds, aliveBirds, score);
        }

        //COMPORTAMIENTO
        for (int i = 0; i < pajaros.Length; i++)
        {
            GameObject bird = pajaros[i];
            if (pajaros[i].active)
            {
                BirdNavMesh seguirTarget = bird.GetComponent<BirdNavMesh>();
                PajaroSeguir seguirAJefe = bird.GetComponent<PajaroSeguir>();                    

                //ES JEFE
                if (seguirTarget != null && seguirAJefe == null)
                {
                    Debug.Log("JEFE HUYE");
                    seguirTarget.Huir(pos, jefeDied);
                }
                //ES MINION
                else if (seguirTarget != null && seguirAJefe != null)
                {
                    Debug.Log("NORMAL HUYE");
                    // DEJA DE SEGUIR AL JEFE TEMPORALMENTE
                    seguirAJefe.enabled = false;
                    //EMPIEZA A MOVERSE RANDOM
                    seguirTarget.enabled = true;
                    seguirTarget.Huir(pos, jefeDied);
                }
                else
                {
                    Debug.Log("GM encontro un pajaro sin BirdNavMesh/PajaroSeguir");
                }
            }
        }
    }

    public void AtraerPajaro(Vector3 posPlayer)
    {
        if (pajaros[0].active)
        {
            pajaros[0].GetComponent<BirdNavMesh>().PlayerCall(posPlayer);
        }
    }

    public bool SpraysAvailable()
    {
        if (spray > 0 && jefeIsAlive)
        {
            spray--;
            uiManager.RemoveSpray(spray);
            return true;
        }
        return false;
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