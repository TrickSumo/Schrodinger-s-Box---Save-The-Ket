using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxOrbs = 3;
    public int currentOrbs = 0;
    public int yLowerLimit = -50;

    public int level = -1;
    public int gameScore = 3; // its public so update from ui

    public List<GameObject> gateOrbs;

    private Character characterScript;

    private GameObject player;

    public Canvas quizCanvas;
    public Canvas menuCanvas;
    public Canvas dialogCanvas;

    private bool isGameOver = false;

    public Vector3 lastGroundedPosition;

    public AudioClip watcherDetectedSound;
    public AudioClip watcherSuspected1;
    public AudioClip watcherSuspected2;

    public AudioClip shooterShoot1;
    public AudioClip shooterShoot2;
    public AudioClip xGateHitSound;
    public AudioClip hGateHitSound;
    public AudioClip zGateHitSound;
    public AudioClip gameOver;
    public AudioClip headshot;
    public AudioClip introEllen;

    public AudioClip powerUpSound;
    public AudioClip gameOverMusic;

    private AudioSource audioSource;

    public float playerJumpForce = 1.0f;
    public float playerSpeed = 5.0f;

    public bool isMindPowerUpActive = false;
    public bool isMindPowerUpCoolDownOver = true;
    public GameObject fallingFloor;
    public bool groundedPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterScript = player.GetComponent<Character>();


        UpdateScore(0);
        playerSpeed = 5.0f;

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // To handle fall of player
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            characterScript = player.GetComponent<Character>();
        }
        // to handle game over

        if (gameScore <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log(player.name);
            characterScript.SetCharacterStateTo(Character.CharacterState.zero);
        }

        // to handle mind power up
        if (isMindPowerUpActive && !groundedPlayer)
        {

            Instantiate(fallingFloor, player.transform.position, Quaternion.identity);

        }

    }



    public void InstantiateGateOrb(Vector3 enemyPosition, Animator shooterAnim = null)
    {
        GameObject gateOrbToSpwan;

        Debug.Log(maxOrbs + " " + currentOrbs);

        // To bring randomness in type of orbs spawned
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                Debug.Log("Difficult");
                gateOrbToSpwan = gateOrbs[Random.Range(0, 3)];

                if (characterScript.currentState == Character.CharacterState.one)
                {
                    gateOrbToSpwan = gateOrbs[0];
                }
                else if (characterScript.currentState == Character.CharacterState.plus)
                {
                    gateOrbToSpwan = gateOrbs[2];
                }
                else if (characterScript.currentState == Character.CharacterState.minus)
                {
                    gateOrbToSpwan = gateOrbs[1];
                }
            }
            else
            {
                gateOrbToSpwan = gateOrbs[Random.Range(0, 3)];
            }
        }
        Debug.Log(currentOrbs + " " + maxOrbs);
        if (currentOrbs < maxOrbs)
        {
            if (shooterAnim != null) shooterAnim.SetTrigger("Shoot");
            Instantiate(gateOrbToSpwan, enemyPosition, Quaternion.identity);
            currentOrbs += 1;
            if (Random.Range(0, 9) == 0) playSound("shooterShoot");
        }

    }

    public void InstantiateXGateOrb(Vector3 enemyPosition, bool obeyMarxOrbs = false, Animator shooterAnim = null)
    {
        GameObject gateOrbToSpwan = gateOrbs[0];

        if (currentOrbs < maxOrbs || !obeyMarxOrbs)
        {

            if (shooterAnim != null) shooterAnim.SetTrigger("Shoot");
            GameObject Orb = Instantiate(gateOrbToSpwan, enemyPosition, Quaternion.identity);
            if (!obeyMarxOrbs) Orb.name = "notObeyMarxOrbs";
            if (obeyMarxOrbs) currentOrbs += 1;
            if (Random.Range(0, 9) == 0) playSound("shooterShoot");
        }

    }

       public void InstantiateHZGateOrb(Vector3 enemyPosition, bool obeyMarxOrbs = false, Animator shooterAnim = null)
    {
        GameObject gateOrbToSpwan = Random.Range(0, 2) == 0?gateOrbs[1]:gateOrbs[2];

        if (currentOrbs < maxOrbs || !obeyMarxOrbs)
        {

            if (shooterAnim != null) shooterAnim.SetTrigger("Shoot");
            GameObject Orb = Instantiate(gateOrbToSpwan, enemyPosition, Quaternion.identity);
            if (!obeyMarxOrbs) Orb.name = "notObeyMarxOrbs";
            if (obeyMarxOrbs) currentOrbs += 1;
            if (Random.Range(0, 9) == 0) playSound("shooterShoot");
        }

    }


    public float CalculateDistanceToPlayer(Vector3 objectPosition)
    {

        float distanceToPlayer = Vector3.Distance(objectPosition, player.transform.position);

        return distanceToPlayer;
    }


    public void StartQuiz()
    {
        quizCanvas.gameObject.SetActive(true);

    }

    public void UpdateScore(int scoreToAdd)
    {
        gameScore += scoreToAdd;
        menuCanvas.GetComponent<MenuCanvasController>().UpdateScore(gameScore);
    }
    public void GameOver()
    {
        menuCanvas.GetComponent<MenuCanvasController>().GameOver();
        playSound("gameOverMusic");
        Debug.Log("Game Over" + Random.Range(0, 100000));
        // StartCoroutine(GameOverWithDelay());
    }
    public void GameWon()
    {
        menuCanvas.GetComponent<MenuCanvasController>().GameWon();
        Debug.Log("Game Won" + Random.Range(0, 100000));
    }

    //   private IEnumerator GameOverWithDelay()
    // {
    //     yield return new WaitForSeconds(1.5f);
    //     menuCanvas.GetComponent<MenuCanvasController>().GameOver();
    // }

    public void DestroyFallStopper(GameObject fallS)
    {

        StartCoroutine(DestroyFallStopperAfterDelay(fallS));

    }
    public IEnumerator DestroyFallStopperAfterDelay(GameObject fallS)
    {
        yield return new WaitForSeconds(6f);
        fallS.SetActive(false);

    }

    public void playSound(string audioClipName)
    {
        switch (audioClipName)
        {
            case "watcherDetectedSound":
                audioSource.PlayOneShot(watcherDetectedSound);
                break;
            case "watcherSuspected":
                audioSource.PlayOneShot(Random.Range(0, 3) == 0 ? watcherSuspected1 : watcherSuspected2);
                break;
            case "shooterShoot":
                audioSource.volume = 0.1f;
                audioSource.PlayOneShot(Random.Range(0, 3) == 0 ? shooterShoot1 : shooterShoot2);
                break;
            case "xGateHitSound":
                audioSource.PlayOneShot(xGateHitSound);
                break;
            case "hGateHitSound":
                audioSource.PlayOneShot(hGateHitSound);
                break;
            case "zGateHitSound":
                audioSource.PlayOneShot(zGateHitSound);
                break;
            case "powerUpSound":
                audioSource.PlayOneShot(powerUpSound);
                break;
            case "headshot":
                Debug.Log("headshot sound" + Random.Range(0, 100000) + headshot.loadState);
                audioSource.PlayOneShot(headshot);
                break;
            case "introEllen":
                audioSource.PlayOneShot(introEllen);
                break;
            case "gameOver":
                audioSource.PlayOneShot(gameOver);
                break;
            case "gameOverMusic":
                // audioSource.volume = 0.1f;
                audioSource.PlayOneShot(gameOverMusic);
                break;

        }
    }

    public void updateDialog(string dialogText)
    {
        dialogCanvas.GetComponent<DialogsCanvasController>().updateDialog(dialogText);
    }


    public IEnumerator ResetPowerUpOver(List<GameObject> playerSword, string powerupType)
    {
        //Add something here to disable powerup
        // yield return new WaitForSeconds(9f);
        // gameManagerScript.playerJumpForce = 1.0f;
        // if (playerSword != null) playerSword.SetActive(false);
        // Debug.Log("PowerUp over");

        switch (powerupType)
        {
            case "Jump":
                yield return new WaitForSeconds(9f);
                playerJumpForce = 1.0f;
                playerSword[0].SetActive(false);
                break;
            case "Mind":
                yield return new WaitForSeconds(9f);
                isMindPowerUpActive = false;
                playerSword[1].SetActive(false);
                StartCoroutine(DoMindPowerUpCoolDownOver());
                break;
            case "Speed":
                yield return new WaitForSeconds(9f);
                playerSpeed = 5.0f;
                playerSword[2].SetActive(false);
                break;
            default:
                // yield return new WaitForSeconds(0f);
                playerJumpForce = 1.0f;
                isMindPowerUpActive = false;
                isMindPowerUpCoolDownOver = true;
                playerSpeed = 5.0f;
                // playerSword[0].SetActive(false);
                // playerSword[1].SetActive(false);
                // playerSword[2].SetActive(false);
                Debug.Log("power up reset all");
                break;
        }
    }

    private IEnumerator DoMindPowerUpCoolDownOver()
    {
        yield return new WaitForSeconds(3f);
        isMindPowerUpCoolDownOver = true;
    }
}
