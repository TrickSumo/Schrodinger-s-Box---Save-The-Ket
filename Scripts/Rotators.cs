using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotators : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 60f; // 60 degrees per second
    public bool clockWise = true;

    private Animator rotatorAnim;
     private GameManager gameManagerScript;
    void Start()
    {
        rotatorAnim = GetComponent<Animator>();
         gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime * (clockWise ? 1 : -1);
        transform.Rotate(0f, rotationAmount, 0f);
        if (Random.Range(0, 900) == 0) rotatorAnim.SetTrigger("Jump");
         if (transform.position.y < gameManagerScript.yLowerLimit) { gameManagerScript.UpdateScore(30); Destroy(gameObject); } 

    }

    public void AlertObservers()
    {

    }
}
