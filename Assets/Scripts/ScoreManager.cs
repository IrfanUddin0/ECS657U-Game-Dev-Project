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
}
