using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuCanvasController : MonoBehaviour
{
    public Button[] menuButtons;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0;

        messageText.gameObject.SetActive(false);

        menuButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        menuButtons[0].onClick.AddListener(() => { StartGame(); });


        //For dev only
        // menuButtons[1].gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Time.timeScale = 1;
        menuButtons[0].gameObject.SetActive(false);
        // messageText.text = "";
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        messageText.gameObject.SetActive(true);

        menuButtons[0].gameObject.SetActive(true);
        menuButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Restart";
        menuButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });


        //For dev only
        // gameObject.SetActive(true);
        // menuButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Ignore and continue";
        // menuButtons[1].gameObject.SetActive(true);
        // menuButtons[1].onClick.AddListener(() => { Time.timeScale = 1; gameObject.SetActive(false); });
        // messageText.text = "";
    }

        public void GameWon()
    {
        Time.timeScale = 0;
        Debug.Log("stopping time");

        messageText.gameObject.SetActive(true);
        messageText.text = "Winner!!!";

        menuButtons[0].gameObject.SetActive(true);
        menuButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Restart";
        menuButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });


        //For dev only
        // gameObject.SetActive(true);
        // menuButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Ignore and continue";
        // menuButtons[1].gameObject.SetActive(true);
        // menuButtons[1].onClick.AddListener(() => { Time.timeScale = 1; gameObject.SetActive(false); });
        // messageText.text = "";

    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
