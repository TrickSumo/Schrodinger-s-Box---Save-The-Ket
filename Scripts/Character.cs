using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public List<Material> materials;
    private Renderer objectRenderer;
    private TextMeshPro playerText;
    private List<GameObject> playerSword;
    private Animator playerAnimator;

    public GameObject fallStopper;

    // public GameObject fallStopper;
    private GameManager gameManagerScript;

    public enum CharacterState { zero, one, plus, minus };
    public CharacterState currentState = CharacterState.zero;
    //  public Vector3 resetPosition = new Vector3(0f, 0f, 0f);

    private void Awake()

    {
        playerSword = new List<GameObject>();
        transform.parent.transform.position = transform.position;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        // for animation
        playerAnimator = GetComponentInChildren<Animator>();

        // Get all the child transforms of this game object
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>(true);

        // Loop through all the child transforms
        foreach (Transform childTransform in childTransforms)
        {
            // Check if the child has the "PlayerSkin" tag
            if (childTransform.gameObject.CompareTag("PlayerSkin"))
            {
                // Get the renderer component of the child game object
                objectRenderer = childTransform.GetComponent<Renderer>();
            }

            if (childTransform.gameObject.CompareTag("PlayerText"))
            {
                playerText = childTransform.GetComponent<TextMeshPro>();
            }

            if (childTransform.gameObject.CompareTag("Sword"))
            {
                childTransform.gameObject.SetActive(false);
                playerSword.Add(childTransform.gameObject);

            }
        }

    }

    private void Update()
    {




        if (transform.position.y < -100)
        {

            //     // GameObject playerClone = Instantiate(transform.parent.gameObject, new Vector3(0,0,0), Quaternion.identity);
            //     // Destroy(gameObject);
            //     // return;
            // //    Debug.Log("low"+transform.position + resetPosition);
            //     //  transform.position = resetPosition;
            //     // StartCoroutine(DestroyFallStopper(Instantiate(fallStopper, new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity)));
            //     // transform.position = new Vector3(transform.position.x, 30, transform.position.z);

            // gameManagerScript.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Light"))
        {
            if (currentState != CharacterState.zero && currentState != CharacterState.one) gameManagerScript.playSound("watcherDetectedSound");

            if (currentState == CharacterState.plus || currentState == CharacterState.minus)
            {
                //  ideally it should be 50-50 for 0 and 1 collapse
                // But making it 30-70 for 0-1 to make game easier
                if (Random.Range(0, 3) == 0) { SetCharacterStateTo(CharacterState.zero); }
                else { SetCharacterStateTo(CharacterState.one); }

            }
        }

        if (other.CompareTag("Shooter") || other.CompareTag("Watcher"))
        {
            gameManagerScript.playSound("headshot");
            playerAnimator.SetTrigger("Headshot");
            // Debug.Log("headshot sound"+ Random.Range(0, 100000));
            StartCoroutine(DestroyEnemy(other));
        }

        if (other.CompareTag("ket"))
        {
             StartCoroutine(gameManagerScript.ResetPowerUpOver(playerSword,"reset all"));
            gameManagerScript.GameWon();
        }


        if (other.CompareTag("PowerUp"))
        {
            if (other.gameObject.name.Contains("Jump"))
            {
                gameManagerScript.playerJumpForce = 3.0f;
                if (playerSword != null)
                {
                    playerSword[0].SetActive(true);

                }
                StartCoroutine(gameManagerScript.ResetPowerUpOver(playerSword, "Jump"));

            }
            else if (other.gameObject.name.Contains("Mind"))
            {
                 gameManagerScript.isMindPowerUpActive = true;
                 gameManagerScript.isMindPowerUpCoolDownOver=false;
                 Input.GetKeyDown(KeyCode.Space);
                if (playerSword != null)
                {
                    playerSword[1].SetActive(true);
                }
                StartCoroutine(gameManagerScript.ResetPowerUpOver(playerSword, "Mind"));

            }
            else if (other.gameObject.name.Contains("Speed"))
            {
                gameManagerScript.playerSpeed = 9.0f;
                if (playerSword != null)
                {
                    playerSword[2].SetActive(true);
                }
                StartCoroutine(gameManagerScript.ResetPowerUpOver(playerSword, "Speed"));

            }

        }


        if (other.CompareTag("Sensor"))
        {

            transform.parent.transform.position = transform.position;
            Vector3 instantiatePosition = new Vector3(gameManagerScript.lastGroundedPosition.x, gameManagerScript.lastGroundedPosition.y + 90, gameManagerScript.lastGroundedPosition.z);
            GameObject playerClone = Instantiate(transform.parent.gameObject, instantiatePosition, Quaternion.identity);
            gameManagerScript.DestroyFallStopper(Instantiate(fallStopper, gameManagerScript.lastGroundedPosition, Quaternion.identity));
            gameManagerScript.UpdateScore(-10);

            // Todos before destroying gameobject
            // gameManagerScript.playerJumpForce = 1.0f; // in case player fall down with power up
           StartCoroutine(gameManagerScript.ResetPowerUpOver(playerSword,"reset all"));

            Destroy(gameObject);
        }

        if (other.CompareTag("X"))
        {
            gameManagerScript.playSound("xGateHitSound");
            Debug.Log(other.gameObject.tag);
            if (currentState == CharacterState.zero) SetCharacterStateTo(CharacterState.one);
            else if (currentState == CharacterState.one) SetCharacterStateTo(CharacterState.zero);
        }
        else if (other.CompareTag("Z"))
        {
            gameManagerScript.playSound("zGateHitSound");
            Debug.Log(other.gameObject.tag);
            if (currentState == CharacterState.plus) SetCharacterStateTo(CharacterState.minus);
            else if (currentState == CharacterState.minus) SetCharacterStateTo(CharacterState.plus);

        }
        else if (other.CompareTag("H"))
        {
            gameManagerScript.playSound("hGateHitSound");
            Debug.Log(other.gameObject.tag);

            if (currentState == CharacterState.zero) SetCharacterStateTo(CharacterState.plus);
            else if (currentState == CharacterState.one) SetCharacterStateTo(CharacterState.minus);
            else if (currentState == CharacterState.plus) SetCharacterStateTo(CharacterState.zero);
            else if (currentState == CharacterState.minus) SetCharacterStateTo(CharacterState.one);

        }
    }

    public void SetCharacterStateTo(CharacterState newState)


    {
        Debug.Log(newState);
        //Existing current state
        switch (currentState)
        {
            case CharacterState.zero:
                break;
            case CharacterState.one:
                break;
            case CharacterState.plus:
                break;
            case CharacterState.minus:
                break;

        }

        //Entering new state
        switch (newState)
        {
            case CharacterState.zero:
                objectRenderer.material = materials[0];
                playerText.text = "|0>";
                gameManagerScript.playSound("gameOver");
                gameManagerScript.GameOver();
                break;
            case CharacterState.one:
                objectRenderer.material = materials[3];
                playerText.text = "|1>";
                break;
            case CharacterState.plus:
                objectRenderer.material = materials[1];
                playerText.text = "|+>";
                break;
            case CharacterState.minus:
                objectRenderer.material = materials[2];
                playerText.text = "|->";
                break;

        }

        currentState = newState;


    }

    private IEnumerator DestroyEnemy(Collider other)
    {
        yield return new WaitForSeconds(0.30f);
        if (other != null)
        {
            Instantiate(other.gameObject, new Vector3(transform.position.x + Random.Range(-9, 10), transform.position.y, transform.position.z + Random.Range(-9, 10)), Quaternion.identity);
            Destroy(other.gameObject);
            gameManagerScript.UpdateScore(5);
        }

    }

    // private IEnumerator PowerUpOver()
    // {
    //     yield return new WaitForSeconds(9f);
    //     gameManagerScript.playerJumpForce = 1.0f;
    //     // if (playerSword != null) playerSword.SetActive(false);
    //     Debug.Log("PowerUp over");
    // }
}

