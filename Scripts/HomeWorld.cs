using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorld : MonoBehaviour

{

    public GameObject Ellen;
    public GameObject HomeWorldGate;
    private GameManager gameManagerScript;
    private bool introSoundPlayed = false;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        //  if (Input.GetKeyDown(KeyCode.F)){
        //     Debug.Log(gameObject);
        //     Renderer rend = GetComponent<Renderer>();
        //     rend.enabled = true;

        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("bump"+other.name+Ellen.name);
        if (other.name == Ellen.name)
        {  
            if(!introSoundPlayed) 
            {
                introSoundPlayed = true;
                gameManagerScript.playSound("introEllen");
            
            gameManagerScript.updateDialog("Hi Stranger! \nMy Cat Got Stuck In Quantum World! \nCan You Please Save Her?  \n\n\n Do Not Collapse To |0> State In Quantum World.\nElse You Will Also Get Stuck There.");
            Destroy(HomeWorldGate);
            }

        }


    }
}
