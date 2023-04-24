using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody gateRb;

    private Collider gateCollider;

    private float force;

    public bool autoDestroy = true;

    public ParticleSystem collideEffect;

    private GameManager gameManagerScript;
    // Start is called before the first frame update

    private float timer = 0, delay = 9.0f;
    void Awake()
    {
        force = Random.Range(19, 30);

        player = GameObject.FindGameObjectWithTag("Player");
        gateRb = GetComponent<Rigidbody>();
        gateCollider = GetComponent<Collider>();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void FixedUpdate()

    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        timer += Time.deltaTime;
        //Destroy gateOrb after some time or if it goes below certain level
        if (autoDestroy && (timer > delay || transform.position.y < gameManagerScript.yLowerLimit)) StartCoroutine(DestroyGate());

        // sometimes add more force
        int random = Random.Range(0, 10);
        if (random == 0) force = Random.Range(30, 60);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        gateRb.AddForce(direction * Time.deltaTime * force);

    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.CompareTag("Player")){Debug.Log("player detected");collideEffect.Play();}
        collideEffect.Play();
        gateCollider.enabled = false;
        StartCoroutine(DestroyGate());

    }

    private IEnumerator DestroyGate()
    {
        yield return new WaitForSeconds(0.30f);
        if(!gameObject.name.Contains("notObeyMarxOrbs"))gameManagerScript.currentOrbs -= 1; // reduce ball count only if ball follow rule of maxorbs
        Destroy(gameObject);
    }




}


