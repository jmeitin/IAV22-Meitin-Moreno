using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh

public class BirdNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("BirdNavMesh no encuentra NavMeshAgent asociado a Player.");
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            Debug.Log("Colision: Bird x Target");

            RandomMoveTarget target = other.gameObject.GetComponent<RandomMoveTarget>();
            target.SetRandomPosition();
        }
    }
}
