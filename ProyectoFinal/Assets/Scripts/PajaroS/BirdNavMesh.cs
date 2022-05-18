using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class BirdNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private int distanciaHuida = 10;
    private RandomMoveTarget target;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("BirdNavMesh no encuentra NavMeshAgent asociado a Pajaro Jefe.");

        target = movePositionTransform.gameObject.GetComponent<RandomMoveTarget>();
        if (target == null)
            Debug.Log("El movePosistionTransform del BirdNavMesh no tiene asociado RandomMoveTarget");
    }

    private void OnEnable()
    {
        target.enabled = true;
        target.SetRandomPosition();
    }

    public void Huir(Vector3 disparo)
    {
        //OBTENGO DIR DE HUIDA 
        Vector3 dir = transform.position - disparo;
        dir.Normalize();

        //CALCULO NEW POS

        target.SetPosition(dir * distanciaHuida);
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            Debug.Log("Colision: Bird x Target");
            target.SetRandomPosition();
        }
    }
}