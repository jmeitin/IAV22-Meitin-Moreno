using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE ENCARGA DEL MOVIMIENTO DE LA CÁMARA
public class PlayerLook : MonoBehaviour
{
    public Camera camara;

    private float xSensitivity = 30f;
    private float ySensitivity = 30f;
    private float xRotation = 0f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calcular la rotacion
        xRotation = xRotation - (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //aplicar la rotacion al transform de la camara
        camara.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotar al player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
