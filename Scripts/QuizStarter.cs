using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizStarter : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManagerScript;
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Let's start quiz");
            gameManagerScript.level += 1;
            gameManagerScript.StartQuiz();
            Destroy(gameObject);
        }
    }
}
