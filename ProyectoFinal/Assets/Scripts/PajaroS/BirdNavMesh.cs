using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class BirdNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject spray;
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private int distanciaHuida = 20000;
    [SerializeField] private int dimensiones = 40;
    [SerializeField] private int tiempoHuida = 5;

    private RandomMoveTarget target;
    private NavMeshAgent navMeshAgent;

    //BOOLS PARA DEFINIR EL COMPORTAMIENTO
    private bool esJefe = false;
    private bool huyendo = false;
    private bool jefeFallecio = false;
    private bool playerCalled = false;
    //POSICION DEL PLAYER (A VECES SE NECESITA)
    private Vector3 playerPos = Vector3.zero;
    //TIMERS---------------------------
    private float lastChange = 0;
    private float INTERVAL = 3;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("BirdNavMesh no encuentra NavMeshAgent asociado a Pajaro Jefe.");

        target = movePositionTransform.gameObject.GetComponent<RandomMoveTarget>();
        if (target == null)
            Debug.Log("El movePosistionTransform del BirdNavMesh no tiene asociado RandomMoveTarget");

        if (GetComponent<PajaroSeguir>() == null) 
            esJefe = true;
    }

    private void OnEnable()
    {
        target.enabled = true;
        // merodeo
        if (esJefe || jefeFallecio)
            target.SetRandomPosition(); 
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
        if (!huyendo && !playerCalled)
        {
            lastChange += Time.deltaTime;
            if(lastChange > INTERVAL)
            {
                //CAMBIA DE DIRECCION CADA CIERTO INTERVALO DE SEGUNDOS
                //SALVO CUANDO ESTA HUYENDO O LE ATRAE EL SPRAY
                target.SetRandomPosition();
                INTERVAL = Random.Range(3, 15) + lastChange;
            }
        }
    }

    public void Huir(Vector3 disparo, bool jefeDied)
    {
        huyendo = true;
        //DIRECCION DE HUIDA
        Vector3 dir = transform.position- disparo;
        dir.Normalize();
        //NUEVO DESTINO
        Vector3 newPos = dir*distanciaHuida * 2 + transform.position;

        //EL DESTINO SE SALE DEL TABLERO
        if (newPos.x > dimensiones || newPos.x < -dimensiones || newPos.z < -dimensiones ||newPos.z > dimensiones)
        {
            if (dir.x > 0) newPos.x = dimensiones;
            else newPos.x = -dimensiones;
            if (dir.z > 0) newPos.z = dimensiones;
            else newPos.x = -dimensiones;
        }

        target.SetPosition(newPos);

        if (jefeDied) jefeFallecio = jefeDied;
        //AL CABO DE UN PAR DE SEGUNDOS DEJA DE HUIR
        Invoke("SinMiedo", tiempoHuida);
    }

    private void SinMiedo()
    {
        if (jefeFallecio || esJefe)
            target.SetRandomPosition(); //VULEVE A MERODEAR

        else //sigue vivo el jefe ==> LE VUELVE A SEGUIR
        {
            GetComponent<PajaroSeguir>().enabled = true;
            this.enabled = false;
        }
    }

    //EL PLAYER HA TIRADO EL SPRAY
    public void PlayerCall(Vector3 pos)
    {
        if (esJefe) { //GUARDA LA NUEVA POS OBJETIVO
            playerPos = pos;
            Invoke("PlayerCallAux", 2); //EL SPRAY SE ACTIVA AL CABO DE 2 SEGUNDOS
        }
    }

    private void PlayerCallAux()
    {
        //EL SPRAY SE PONE EN LA ULTIMA POS DE PLAYER (SOLO SE ACTUALIZA EN PlayerCall)
        target.SetPosition(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        if (spray != null)
        {
            //INSTANCIA EL SPRAY
            GameObject g = Instantiate<GameObject>(spray);
            g.transform.position = playerPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PAJARO ALCANZA SU OBJETIVO
        if (other.gameObject == target.gameObject)
        {
            //DEJA DE HUIR O SER ATRAIDO POR EL PLAYER
            playerCalled = false;
            huyendo = false;
            //NUEVO DESTINO
            target.SetRandomPosition();
        }
    }
}