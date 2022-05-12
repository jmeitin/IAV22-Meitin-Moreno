using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private void Start() //no es awake dado que se tiene que crear el GM primero
    {
        GameManager.instance.AddBird();
    }
}
