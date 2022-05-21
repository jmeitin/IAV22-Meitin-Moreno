using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //CANVAS
using TMPro; //TextMeshPro

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI aliveText;
    public TextMeshProUGUI deadText;
    public TextMeshProUGUI sprayText;
    public TextMeshProUGUI ganarText;

    void Start()
    {
        // PASO REFERENCIA AL GM
        GameManager.instance.SetUIManager(this);
    }

    public void Init(int score, int deadBirds, int aliveBirds, int spray)
    {
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        shotsText.text = "0";
        aliveText.text = aliveBirds.ToString();
        sprayText.text = spray.ToString();
    }

    //ACTUALIZAR INFO INTERFAZ------------------------------------------------------------------
    public void RemoveBird(int deadBirds, int aliveBirds, int score)
    {
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        aliveText.text = aliveBirds.ToString();
        if (aliveBirds == 0) //NO QUEDAN PAJAROS ==> MENSAJE GANAR
           ganarText.enabled = true;
    }

    public void AmountShots(int shots)
    {
        shotsText.text = shots.ToString();
    }

    public void RemoveSpray(int spray)
    {
        sprayText.text = spray.ToString();
    }

}
