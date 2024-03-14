using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOrganizer : MonoBehaviour
{

    GameObject scoreBoard1; //very shitty way of doing this, i can iterate thru a loop to find all child objects...
    GameObject scoreBoard2;
    GameObject scoreBoardAI;

    TextMeshProUGUI p1lives;
    TextMeshProUGUI p2lives;
    TextMeshProUGUI aIlives;


    [SerializeField] int player1lives = 3;
    [SerializeField] int player2lives = 3;
    [SerializeField] int playerAIlives = 3;

    /*this is getting horrible holy...
    idea is to have a prefab game object script combo i can use for both pve and pvp
    once a player wins, its object will call this script, which will do stuff
    stuff to do: 
    update score (best of 3)
    restart scene if noone won yet while still keeping scores
    holds our pause/restart button aswell?
    */

    public static GameOrganizer instance;

    private void Awake()
    {
        // start of new code
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    private void OnEnable()
    {
        Debug.Log("OnEnableCalled");
        UpdateScores();
    }

    private void Update()
    {
        //temporary sol
        if (Input.GetKey(KeyCode.L)) {
            player1lives = 3;
            player2lives = 3;
            playerAIlives = 3;
            RestartScene();
        }
    }

    public void PlayerLoses(int index)
    {
        switch (index)
        {
            case 0: //ai
                playerAIlives -= 1;
               
                aIlives.text = playerAIlives.ToString();

                break;
            case 1: //player 1
                player1lives -= 1;
                
                p1lives.text = player1lives.ToString();
                
                break;
            case 2: //player 2
                player2lives -= 1;
                
                p2lives.text = player2lives.ToString();
                
                break;
        }

        Invoke("RestartScene", 3f); // Adjust the delay time as needed
        


    }

    public void UpdateScores()
    {
        Debug.Log("InsideOnEnableCalled");

        scoreBoardAI = GameObject.FindWithTag("ScoringAI");
        aIlives = scoreBoardAI.GetComponent<TextMeshProUGUI>();

        scoreBoard1 = GameObject.FindWithTag("Scoring1");
        p1lives = scoreBoard1.GetComponent<TextMeshProUGUI>();

        scoreBoard2 = GameObject.FindWithTag("Scoring2");
        p2lives = scoreBoard2.GetComponent<TextMeshProUGUI>();

        aIlives.text = playerAIlives.ToString();
        p1lives.text = player1lives.ToString();
        p2lives.text = player2lives.ToString();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
