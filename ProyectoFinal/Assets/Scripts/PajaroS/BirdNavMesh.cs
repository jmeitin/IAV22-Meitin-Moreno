using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class BirdNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private int distanciaHuida = 20000;
    [SerializeField] private int dimensiones = 40;
    [SerializeField] private int tiempoHuida = 5;

    private RandomMoveTarget target;
    private NavMeshAgent navMeshAgent;

    private bool esJefe = false;
    private bool huyendo = false;
    private bool jefeFallecio = false;
    private bool playerCalled = false;
    private Vector3 playerPos = Vector3.zero;
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
        if (esJefe || jefeFallecio)
        {
            //Debug.Log("Activado");
            target.SetRandomPosition();
        }
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
        if (!huyendo && !playerCalled)
        {
            lastChange += Time.deltaTime;
            if(lastChange > INTERVAL)
            {
                target.SetRandomPosition();
                INTERVAL = Random.Range(3, 15) + lastChange;
            }

        }
    }

    public void Huir(Vector3 disparo, bool jefeDied)
    {
        huyendo = true;
        Vector3 dir = transform.position- disparo;
        dir.Normalize();
        Vector3 newPos = dir*distanciaHuida * 2 + transform.position;
        if (newPos.x > dimensiones || newPos.x < -dimensiones || newPos.z < -dimensiones ||newPos.z > dimensiones)
        {
            if (dir.x > 0) newPos.x = dimensiones;
            else newPos.x = -dimensiones;
            if (dir.z > 0) newPos.z = dimensiones;
            else newPos.x = -dimensiones;
        }
        //Debug.Log("POS TARGET"+newPos);
        target.SetPosition(newPos);

        if (jefeDied) jefeFallecio = jefeDied;
        Invoke("SinMiedo", tiempoHuida);
    }

    private void SinMiedo()
    {
        if (huyendo)
        {
            Debug.Log("SinMiedo");
            if (jefeFallecio || esJefe)
                target.SetRandomPosition();

            else //sigue vivo el jefe
            {
                GetComponent<PajaroSeguir>().enabled = true;
                this.enabled = false;
            }
        }
    }

    public void PlayerCall(Vector3 pos)
    {
        if (esJefe)
        {
            playerPos = pos;
            Invoke("PlayerCallAux", 2);
        }
    }

    private void PlayerCallAux()
    {
        target.SetPosition(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        if (explosion != null)
        {
            GameObject g = Instantiate<GameObject>(explosion);
            g.transform.position = playerPos;
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            playerCalled = false;
            huyendo = false;
            //Debug.Log("Colision: Bird x Target");
            target.SetRandomPosition();
        }
    }
}