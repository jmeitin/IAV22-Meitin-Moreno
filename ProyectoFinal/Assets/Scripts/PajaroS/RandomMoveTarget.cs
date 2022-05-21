using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveTarget : MonoBehaviour
{
    public float dimensiones = 30; //dimensiones del escenario

    public void SetRandomPosition()
    {
        //nueva pos
        float x = Random.Range(-dimensiones, dimensiones);
        float y = transform.position.y;
        float z = Random.Range(-dimensiones, dimensiones);
        transform.position = new Vector3(x, y, z);
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    private void OnTriggerStay(Collider other)
    {
        //LOS PAJAROS NO PUEDEN ATRAVESAR LOS ARBOLES
        //EN CASO DE QUE LA POSICION DEL TARGET SEA LA DE UN ARBOL
        //SE DESPLAZA HACIA LA DERECHA
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree"))
            transform.Translate(5, 0, 0);
    }
}
