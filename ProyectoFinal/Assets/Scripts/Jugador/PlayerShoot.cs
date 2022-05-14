using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public LayerMask layerMask;
    private float distance = 50f;
    private Camera camara;
    private AudioSource audio;
   

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

    // Update is called once per frame
    void Update() {  }

    public void Shoot()
    {
        GameManager.instance.AddBullet();
        audio.Play();

        Ray ray = new Ray(camara.transform.position, camara.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit objetivo;
        //EL DISPARO ACERTO
        if (Physics.Raycast(ray, out objetivo, distance, layerMask))
        {
            Bird pajaro = objetivo.collider.GetComponent<Bird>();
            if (pajaro != null && pajaro.gameObject.active)
            {
                pajaro.KillBird();
            }
        }
    }
}
