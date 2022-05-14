using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour //abstract si quieres que sea un template para el raycast
{
    bool jefe = false;
    private void Start() //no es awake dado que se tiene que crear el GM primero
    {
        GameManager.instance.AddBird();
        //NO SIGUE A OTRO PAJARO ==> ES EL JEFE
        if (!GetComponent<PajaroSeguir>()) 
            jefe = true;
    }

    public void KillBird()
    {
        if (!jefe)
        {
            GameManager.instance.BirdDied(15);
            Debug.Log("ADIOS PAJARO");
            this.gameObject.SetActive(false);
        }
            
    }
}
