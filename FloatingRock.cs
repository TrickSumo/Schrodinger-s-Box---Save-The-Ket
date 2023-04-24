using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRock : MonoBehaviour
{
    // Start is called before the first frame update
    private float movementRangeUp;
    private float movementRangeDown;
    private bool isGoingUp = true;
    private GameObject player;
    public int range = 9;
    public bool isFloating = true;
      public float detectionRadius = 30.0f;
          private GameManager gameManagerScript;
    void Start()
    {
        movementRangeUp = transform.position.y + range;
        movementRangeDown = transform.position.y - range; 
           player = GameObject.FindGameObjectWithTag("Player");
                   gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        

        if (isFloating && distanceToPlayer <= detectionRadius)
        {
            if (isGoingUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
                if (transform.position.y >= movementRangeUp)
                {
                    isGoingUp = false;
                }
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime);
                if (transform.position.y <= movementRangeDown)
                {
                    isGoingUp = true;
                }
            }

        }


    }
}
