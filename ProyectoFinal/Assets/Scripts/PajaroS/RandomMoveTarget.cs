using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveTarget : MonoBehaviour
{
    public float dimensiones = 30;

    public void SetRandomPosition()
    {
        Debug.Log("Random");
        ////deshace camino ==> vuelve al (0,y,0)
        Vector3 pos = transform.position;
        Debug.Log(pos);
        pos = -pos;
        Debug.Log(pos);
        transform.Translate(pos.x, 0, pos.z);
        //nueva pos
        float x = Random.Range(-dimensiones, dimensiones);
        float z = Random.Range(-dimensiones, dimensiones);
        transform.Translate(x, 0, z);
        Debug.Log("Final Pos = "+ transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            Debug.Log("Colision: Tree x Target");

            SetRandomPosition();
        }
    }
}
