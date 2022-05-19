using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class BirdNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private int distanciaHuida = 20000;
    private bool esJefe = false;
    private RandomMoveTarget target;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject explosion;

    bool jefeFallecio = false;
    bool huyendo = false;

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
        if(esJefe || jefeFallecio) target.SetRandomPosition();
    }

    public void Huir(Vector3 disparo, bool jefeDied)
    {
        huyendo = true;
        Vector3 dir = transform.position- disparo;
        dir.Normalize();
        Vector3 newPos = dir * distanciaHuida*2 + transform.position;
        Debug.Log(newPos);
        target.SetPosition(newPos);

        if (jefeDied) jefeFallecio = jefeDied;
        Invoke("SinMiedo", 5);
    }

    private void SinMiedo()
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

    public void PlayerCall(Vector3 playerPos)
    {
        if (esJefe)
        {
            Vector3 pos = new Vector3(playerPos.x, transform.position.y, playerPos.z);
            target.SetPosition(pos);
            if (explosion != null){
                GameObject g = Instantiate<GameObject>(explosion);
                g.transform.position = target.transform.position;  
            }
            
        }
        
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            //Debug.Log("Colision: Bird x Target");
            target.SetRandomPosition();
        }
    }
}