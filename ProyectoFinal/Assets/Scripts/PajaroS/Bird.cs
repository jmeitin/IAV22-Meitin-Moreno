using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour //abstract si quieres que sea un template para el raycast
{
    [SerializeField] private int points = 10;
    [SerializeField] private GameObject explosion; //sistema de particulas

    private bool jefe = false;

    private void Start() //no es awake dado que se tiene que crear el GM primero
    {
        //AVISA AL GM ==> PajarosEnLaEscena++
        GameManager.instance.AddBird();
        //NO SIGUE A OTRO PAJARO ==> ES EL JEFE
        if (!GetComponent<PajaroSeguir>()) 
            jefe = true;
    }

    public void KillBird()
    {
        //INSTANCIA LA EXPLOSION AL MORIR
        GameObject g = Instantiate<GameObject>(explosion);
        g.transform.position = transform.position;

        //AVISA AL GAME MANAGER
        GameManager.instance.BirdDied(points, transform.position, jefe);

        //SE DESACTIVA
        this.gameObject.SetActive(false);
    }
}
