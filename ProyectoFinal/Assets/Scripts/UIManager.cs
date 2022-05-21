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

    private int enemiesLeft;

    void Start()
    {
        GameManager.instance.SetUIManager(this);
    }

    public void Init(int score, int deadBirds, int aliveBirds, int spray)
    {
        //Debug.Log("NUMENEMIES = " + aliveBirds);
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        shotsText.text = "0";
        aliveText.text = aliveBirds.ToString();
        enemiesLeft = aliveBirds;
        sprayText.text = spray.ToString();
    }

    public void RemoveBird(int deadBirds, int aliveBirds, int score)
    {
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        aliveText.text = aliveBirds.ToString();
        enemiesLeft--;
        if (aliveBirds == 0)
           ganarText.enabled = true;
    }

    public void AmountShots(int shots)
    {
        shotsText.text = shots.ToString();
        Debug.Log("HAHAHAHA "+ shots);
    }

    public void RemoveSpray(int spray)
    {
        sprayText.text = spray.ToString();
    }

}
