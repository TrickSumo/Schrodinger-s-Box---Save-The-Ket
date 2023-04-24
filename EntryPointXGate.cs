using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPointXGate : MonoBehaviour
{
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    public ParticleSystem entryEffect;
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManagerScript.UpdateScore(30);
            entryEffect.Play();
            Destroy(gameObject);
        }
    }
}
