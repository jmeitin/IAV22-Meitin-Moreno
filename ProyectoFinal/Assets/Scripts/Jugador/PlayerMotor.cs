using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SE ENCARGA DEL MOVIMIENTO DE PLAYER
public class PlayerMotor : MonoBehaviour
{
    //Componente asociado al prefab
    private CharacterController controller; 
    public float speed = 5f;
    //* le podria meter gravedad ==> jump

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            Debug.Log("PlayerMotor no detecta CharacterController asociado a Player.");
    }

    //Recibe el input del InputManager ==> CharacterController
    public void ProcessMove(Vector2 input)
    {
        Vector3 dir = Vector3.zero;
        dir.x = input.x;
        dir.z = input.y;
        controller.Move(transform.TransformDirection(dir) * speed * Time.deltaTime);
    }
}
