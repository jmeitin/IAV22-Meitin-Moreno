using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //package

// En vez del InputSystem que viene por defecto utilizo un paquete que me es más cómodo.
// Véase el README con la docmuentación para ver como funciona en mayor detalle.

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; //solo se utiliza en el Awake
    private PlayerInput.AndandoActions andando;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerShoot playerShoot;
    private Camera camara;

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

        camara = playerLook.camara;
        if (camara == null)
            Debug.Log("PlayerShoot no encuentra PlayerLook asociado a Player o la Main Camara.");

        //DISPARAR PLAYER
        playerShoot = GetComponent<PlayerShoot>();
        if (playerShoot == null)
            Debug.Log("InputManager no encuentra PlayerShoot asociado a Player.");
    }

    private void Update()
    {
        // DISPARAR
        if (andando.Disparar.triggered)
            playerShoot.Shoot();

        // LANZAR SPRAY
        else if (andando.Atraer.triggered)
        {
            if(GameManager.instance.SpraysAvailable())
                GameManager.instance.AtraerPajaro(transform.position);
        }
    }
    private void FixedUpdate()
    {
        // MOVERSE
        playerMotor.ProcessMove(andando.Movimiento.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // MOVER LA CAMARA
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
