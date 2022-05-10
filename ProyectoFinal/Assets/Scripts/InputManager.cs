using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //package

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; //solo se utiliza en el Awake
    private PlayerInput.AndandoActions andando;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    private void Awake()
    {
        //GESTOR INPUT SYSTEM
        playerInput = new PlayerInput(); //creamos instancia del gestor de Input
        if (playerInput == null) 
            Debug.Log("InputManager no encuentra la clase PlayerInput.");
        andando = playerInput.Andando;
        
        //MOVIMENTO PLAYER
        playerMotor = GetComponent<PlayerMotor>();
        if (playerMotor == null)
            Debug.Log("InputManager no encuentra PlayerMotor asociado a Player.");

        //MIRAR PLAYER
        playerLook = GetComponent<PlayerLook>();
        if (playerLook == null)
            Debug.Log("InputManager no encuentra PlayerLook asociado a Player.");
    }

    private void FixedUpdate()
    {
        playerMotor.ProcessMove(andando.Movimiento.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(andando.Mirar.ReadValue<Vector2>());

    }

    private void OnEnable()
    {
        andando.Enable();
    }

    private void OnDisable()
    {
        andando.Disable();
    }

   
}
