using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour //abstract si quieres que sea un template para el raycast
{
    private bool jefe = false;
    [SerializeField] private int points = 10;
    [SerializeField] private GameObject explosion;

    private void Start() //no es awake dado que se tiene que crear el GM primero
    {
        GameManager.instance.AddBird();
        //NO SIGUE A OTRO PAJARO ==> ES EL JEFE
        if (!GetComponent<PajaroSeguir>()) 
            jefe = true;
    }

    private void Update()
    {
        if (jefe)
        {
            //Debug.Log(transform.forward);
        }
    }
    public void KillBird()
    {
        GameObject g = Instantiate<GameObject>(explosion);
        g.transform.position = transform.position;

        GameManager.instance.BirdDied(points, transform.position, jefe);

        this.gameObject.SetActive(false);
    }
}
