using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    // public GameObject orb;

    private GameManager gameManagerScript;
    private Animator shooterAnim;
    public float delay = 1.0f;
    private float timer = 0f;

    public float detectionRadius = 30.0f;
    private bool isSuspectedSoundPlayed = false;
    public bool isClassicalShooter = false;
    public bool isQuantumShooter = false;
    public bool obeyMarxOrbs = false;
    void Start()
    {
        shooterAnim = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.transform.LookAt(player.transform);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < gameManagerScript.yLowerLimit) { gameManagerScript.UpdateScore(30); Destroy(gameObject); }
        if (!player) { player = GameObject.FindWithTag("Player"); }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!player) { player = GameObject.FindWithTag("Player"); }
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (timer >= delay && distanceToPlayer <= detectionRadius)
        {
            if (!isSuspectedSoundPlayed)
            {
                isSuspectedSoundPlayed = true;
                gameManagerScript.playSound("shooterShoot");
            }
            gameObject.transform.LookAt(player.transform);
            if (isClassicalShooter)
            {
                // Debug.Log("classical shooter" + Random.Range(0, 100000));
                gameManagerScript.InstantiateXGateOrb(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 0.3f), obeyMarxOrbs, shooterAnim);
            }
            else if (isQuantumShooter)
            {
                // Debug.Log("classical shooter" + Random.Range(0, 100000));
                gameManagerScript.InstantiateHZGateOrb(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 0.3f), obeyMarxOrbs, shooterAnim);
            }
            else
            {
                // Debug.Log("normal shooter" + Random.Range(0, 100000));
                gameManagerScript.InstantiateGateOrb(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 0.3f), shooterAnim);
            }
            timer = 0f;
        }


    }
}
