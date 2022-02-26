using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

     public static int currentHealth = 1;
    private static bool gameOver;

    public GameObject gameOverPanel;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentHealth <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }


    }
}
