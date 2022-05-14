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

    private int enemiesLeft;

    void Start()
    {
        GameManager.instance.SetUIManager(this);
    }

    public void Init(int score, int deadBirds, int aliveBirds)
    {
        Debug.Log("NUMENEMIES = " + aliveBirds);
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        shotsText.text = "0";
        aliveText.text = aliveBirds.ToString();
        enemiesLeft = aliveBirds;
    }

    public void RemoveBird(int deadBirds, int aliveBirds, int score)
    {
        scoreText.text = score.ToString();
        deadText.text = deadBirds.ToString();
        aliveText.text = aliveBirds.ToString();
        enemiesLeft--;
    }

    public void AmountShots(int shots)
    {
        shotsText.text = shots.ToString();
        Debug.Log("HAHAHAHA"+ shots);

    }


}
