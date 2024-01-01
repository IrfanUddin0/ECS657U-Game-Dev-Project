using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public int scoreCount;
    public Transform MainLight;
    public bool countNext;

    public float lastDeathXLightPosition;
    public float lastDeathYLightPosition;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        scoreText.text = "Day " + scoreCount.ToString();
        countNext = false;
        lastDeathXLightPosition = 0f;
        lastDeathYLightPosition = 60f;

        MainLight = GameObject.FindGameObjectWithTag("MainLight").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // lastDeathXLightPosition and lastDeathYLightPosition values rounded in the PlayerSurvival script
        if (Mathf.Round(MainLight.eulerAngles.x) == lastDeathXLightPosition)
        {
            if (Mathf.Round(MainLight.eulerAngles.y) != lastDeathYLightPosition)
            {
                countNext = true;
            }

            if (Mathf.Round(MainLight.eulerAngles.y) == lastDeathYLightPosition && countNext == true)
            {
                scoreCount++;
                scoreText.text = "Day " + scoreCount.ToString();
                countNext = false;
            }
        }
    }

    public void IncreaseScore()
    {
        scoreCount++;
    }

    public void onPlayerDeath()
    {
        // new changes made by Sulaiman
        // disable the main light animation
        MainLight.GetComponent<Animator>().enabled = false;
        // values rounded to compare with main light rotation value simpler
        lastDeathXLightPosition = Mathf.Round(MainLight.eulerAngles.x);
        lastDeathYLightPosition = Mathf.Round(MainLight.eulerAngles.y);
    }

    public void onPlayerRespawn()
    {
        // new changes made by Sulaiman
        // enable the main light animation
        MainLight.GetComponent<Animator>().enabled = true;
        // set days survived score to 0
        scoreCount = 0;
        scoreText.text = "Day " + scoreCount.ToString();
    }

    public void setScoreFromSave(ScoreManagerSave save)
    {
        scoreCount = save.scoreCount;
        countNext = save.countNext;
        lastDeathXLightPosition = save.lastDeathXLightPosition;
        lastDeathYLightPosition = save.lastDeathYLightPosition;
        scoreText.text = "Day " + scoreCount.ToString();
    }
}
