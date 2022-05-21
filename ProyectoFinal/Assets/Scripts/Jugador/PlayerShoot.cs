using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE ENCARGA DE LOS DISPAROS DEL PLAYER
public class PlayerShoot : MonoBehaviour
{
    public LayerMask layerMask; //Layer en la que estan los objetivos
    private float distance = 50f; //distancia que recorre la bala
    private Camera camara;
    private AudioSource audio; //sonido del disparo

    // Start is called before the first frame update
    void Start()
    {
        camara = GetComponent<PlayerLook>().camara;
        if (camara == null)
            Debug.Log("PlayerShoot no encuentra PlayerLook asociado a Player o la Main Camara.");

        audio = GetComponent<AudioSource>();
        if (audio == null)
            Debug.Log("PlayerShoot no encuentra AudioSource asociado a Player.");
    }

    public void Shoot()
    {
        GameManager.instance.AddBullet(); //avisar al GM
        audio.Play(); //play sonido

        Ray ray = new Ray(camara.transform.position, camara.transform.forward);

        //DIBUJAR RAY
        //Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit objetivo; //OBJETIVO ES OUT ==> TOMA VALOR AL PASARLO COMO PARAMETRO
        //EL DISPARO ACERTO
        if (Physics.Raycast(ray, out objetivo, distance, layerMask))
        {
            Bird pajaro = objetivo.collider.GetComponent<Bird>();
            //COLISIONO CON UN PAJARO
            if (pajaro != null && pajaro.gameObject.active)
            {
                pajaro.KillBird();
            }
        }
    }
}
