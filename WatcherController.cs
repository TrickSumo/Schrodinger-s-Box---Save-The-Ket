using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WatcherController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    private GameObject player;
       private Character characterScript;
    public float moveSpeed = 0.3f;
    public float detectionRadius = 30.0f;

    private Animator watcherAnimator;

    private GameManager gameManagerScript;

    private bool isSuspectedSoundPlayed = false;



    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
         characterScript = player.GetComponent<Character>();

        watcherAnimator = GetComponent<Animator>();

        startPosition = transform.position;
        endPosition = player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
        if (transform.position.y < gameManagerScript.yLowerLimit) { gameManagerScript.UpdateScore(30); Destroy(gameObject); } // delete watcher if fall below some level y axis

    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            characterScript = player.GetComponent<Character>();
        }
        startPosition = transform.position;
        endPosition = player.transform.position;


        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRadius && characterScript.currentState != Character.CharacterState.zero && characterScript.currentState != Character.CharacterState.one)
        {
            if(!isSuspectedSoundPlayed){
                isSuspectedSoundPlayed=true;
                gameManagerScript.playSound("watcherSuspected");
            }
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            transform.LookAt(player.transform);

            // Move towards the player using Lerp and MoveTowards
            transform.position = Vector3.Lerp(startPosition, endPosition, Time.deltaTime * moveSpeed);
            transform.position = Vector3.MoveTowards(startPosition, endPosition, Time.deltaTime * moveSpeed);

            watcherAnimator.SetTrigger("walking");
        }
        else {
            isSuspectedSoundPlayed = false;
        }

    }

}
