// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PowerUp : MonoBehaviour
// {
//     public float rotationSpeed = 60f;
//     public ParticleSystem powerup;

//     private GameManager gameManagerScript;

//     private void Awake()
//     {
//         gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //Always rotate

//         float rotationAmount = rotationSpeed * Time.deltaTime;
//         transform.Rotate(0f, rotationAmount, 0f);

//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             powerup.Play();
//             gameManagerScript.playSound("powerUpSound");
//             StartCoroutine(DestroyPowerUp());
//         }
//     }

//     private IEnumerator DestroyPowerUp()
//     {
//         yield return new WaitForSeconds(0.3f);
//         Destroy(gameObject);
//     }
// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public ParticleSystem powerup;

    private GameManager gameManagerScript;
    private Renderer orbRenderer;
    private Collider orbCollider;

    private void Awake()
    {
        orbRenderer = GetComponent<Renderer>();
        orbCollider = GetComponent<Collider>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Always rotate

        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerup.Play();
            gameManagerScript.playSound("powerUpSound");
            StartCoroutine(DestroyPowerUp());
        }
    }

    private IEnumerator DestroyPowerUp()
    {
        yield return new WaitForSeconds(0.3f);
        if (!gameObject.name.Contains("Mind")) Destroy(gameObject);
        else
        {  // Respawn Mind Orb Afetr Some Time
            orbRenderer.enabled = false;
            orbCollider.enabled = false;
            StartCoroutine(ReSpwanPowerUp());
        }
    }

    private IEnumerator ReSpwanPowerUp()
    {
        yield return new WaitForSeconds(12f);
        orbRenderer.enabled = true;
        orbCollider.enabled = true;

    }
}
