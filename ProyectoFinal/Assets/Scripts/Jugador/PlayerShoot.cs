using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Camera camara;
    private float distance = 50f;
    public LayerMask layerMask; 

    // Start is called before the first frame update
    void Start()
    {
        camara = GetComponent<PlayerLook>().camara;
        if (camara == null)
            Debug.Log("PlayerShoot no encuentra PlayerLook asociado a Player o la Main Camara.");
    }

    // Update is called once per frame
    void Update() {  }

    public void Shoot()
    {
        Debug.Log("Disparar");

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
