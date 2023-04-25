using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizController : MonoBehaviour
{

    [System.Serializable]
    private class QuizData
    {
        public Question[] questions;
    }

    [System.Serializable]
    private class Question
    {
        public string question;
        public string[] options;
        public string answer;
    }

    public TextMeshProUGUI questionText;
    public List<Button> answerButtons;

    private Question currentQuiz;
    private string currentQuestion;
    private string currentAnswer;

    private int currentScore = 0;
    private QuizData quizData;

    private int level;

    public RawImage correct;
    public RawImage wrong;

    private GameManager gameManagerScript;

    private void Awake()
    {
        correct.enabled = false;
        wrong.enabled = false;
        Time.timeScale = 0;

        // import gamemanager to get level details
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        level = gameManagerScript.level;

        string json = Resources.Load<TextAsset>("QuizData").text;
        quizData = JsonUtility.FromJson<QuizData>(json);

        UpdateCanvas();

        // answerButtons[0].onClick.AddListener(CheckAnswer);
        //adding event listners
        answerButtons[0].onClick.AddListener(() => { CheckAnswer(answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text); });
        answerButtons[1].onClick.AddListener(() => { CheckAnswer(answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text); });
        answerButtons[2].onClick.AddListener(() => { CheckAnswer(answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text); });
        answerButtons[3].onClick.AddListener(() => { CheckAnswer(answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text); });
    }



    private void Update()
    {
        if (level != gameManagerScript.level)
        {
            level = gameManagerScript.level;
            Debug.Log(level);
            UpdateCanvas();
            EnableAllOptions();
            Time.timeScale = 0;
        }

    }

    private void UpdateCanvas()

    {
        currentQuiz = quizData.questions[level];
        // Set the question text and answer
        currentQuestion = currentQuiz.question;
        currentAnswer = currentQuiz.answer;

        // // Set the answer options for each button
        questionText.text = currentQuestion;
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = currentQuiz.options[0];
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = currentQuiz.options[1];
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = currentQuiz.options[2];
        answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = currentQuiz.options[3];
    }


    public void CheckAnswer(string answer)
    {
        // potential bug here. buttons might not reload on second quiz - fixed using enableAllOptions() - now if there is any bug, check there
        foreach (Button answerButton in answerButtons)
        {

            if (answerButton.GetComponentInChildren<TextMeshProUGUI>().text != answer) { answerButton.interactable = false; }
        }
        if (answer == currentAnswer)
        {
            Debug.Log("Correct!");
            correct.enabled = true;
        }
        else
        {
            Debug.Log("Incorrect!");
            wrong.enabled = true;
            currentScore -= 60;
        }

        StartCoroutine(DisableQuizCanvas());


    }

    public void EnableAllOptions()
    {
        foreach (Button answerButton in answerButtons) answerButton.interactable = true;
    }

    private IEnumerator DisableQuizCanvas()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        gameManagerScript.UpdateScore(currentScore);
        currentScore = 0;
        gameObject.SetActive(false);


    }


}
