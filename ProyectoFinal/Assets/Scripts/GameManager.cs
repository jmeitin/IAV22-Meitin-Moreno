using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Gestion de escenas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] pajaros;
    //numero de veces que puede player llamar al pajaro jefe
    [SerializeField] private int spray = 3; 
    bool jefeIsAlive = true;
    private int deadBirds = 0;
    private int aliveBirds = 0;
    private int score = 0;
    private int shots = 0;
    private UIManager uiManager;
    bool actualizarUI = true;

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
        //Aviso 
        Debug.Log("Número de pájaros asociados al GM: " + pajaros.Length);
    }

    //GUARDAR REFERENCIA AL UI MANAGER
    public void SetUIManager(UIManager uim) { uiManager = uim; }

    //ACTUALIZAR EL NUMERO DE PAJAROS
    public void AddBird() { aliveBirds++; }

    //ACTUALIZAR EL NUMERO DE BALAS
    public void AddBullet()
    {   
        shots++;
        uiManager.AmountShots(shots);
    }

    private void Update()
    {
        if (actualizarUI == true)  { //ESTADO INICIAL UI MANAGER
            uiManager.Init(score, deadBirds, aliveBirds, spray);
            actualizarUI = false;
        }
    }

    public void BirdDied(int destructionPoints, Vector3 pos, bool jefeDied)
    {
        if (jefeDied)
            jefeIsAlive = false;

        //ACTUALIZAR INTERFAZ------------------
        aliveBirds--;
        deadBirds++;
        score += destructionPoints;
        if (uiManager != null)
            uiManager.RemoveBird(deadBirds, aliveBirds, score);

        //ACTUALIZAR COMPORTAMIENTO DE LOS PAJAROS
        for (int i = 0; i < pajaros.Length; i++)
        {
            GameObject bird = pajaros[i];
            if (pajaros[i].active)
            {
                BirdNavMesh seguirTarget = bird.GetComponent<BirdNavMesh>();
                PajaroSeguir seguirAJefe = bird.GetComponent<PajaroSeguir>();                    

                //HUYE EL JEFE
                if (seguirTarget != null && seguirAJefe == null)
                    seguirTarget.Huir(pos, jefeDied);

                //HUYE EL PAJARO MENOR
                else if (seguirTarget != null && seguirAJefe != null)
                {
                    // DEJA DE SEGUIR AL JEFE TEMPORALMENTE
                    seguirAJefe.enabled = false;
                    //EMPIEZA A MOVERSE RANDOM
                    seguirTarget.enabled = true;
                    seguirTarget.Huir(pos, jefeDied);
                }

                else Debug.Log("GM encontro un pajaro sin BirdNavMesh/PajaroSeguir");
            }
        }
    }

    //EL PLAYER QUIERE TIRAR UN SPARY ==> ATRAER AL JEFE
    public void AtraerPajaro(Vector3 posPlayer)
    {
        if (pajaros[0].active) //Pajaro Jefe es el 0
            pajaros[0].GetComponent<BirdNavMesh>().PlayerCall(posPlayer);
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

    //POR SI ACASO EXPANDO EL JUEGO---------------------------------------------
    private void GameOver() //menu
    {
        score = 0;
        deadBirds = 0;
        aliveBirds = 0;
        //ChangeScene("Menu");
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}