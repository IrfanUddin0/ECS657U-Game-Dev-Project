using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainPlayerScript Player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MainPlayerScript>();
        Player.inputModeSetUI();

        GetComponentInChildren<Image>().rectTransform.localScale = new Vector3(Screen.currentResolution.width, Screen.currentResolution.height);
        GetComponentInChildren<Button>().onClick.AddListener(RespawnButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RespawnButton()
    {
        // set max survival stats
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>().OnRespawn();

        // respawn player
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MainPlayerScript>().Respawn();

        // destory game over screen
        Destroy(gameObject);
    }
}
