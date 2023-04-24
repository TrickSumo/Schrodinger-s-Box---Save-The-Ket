using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Camera GameCamera;
    public float playerSpeed = 5.0f;
    private float JumpForce = 1.0f;
    
    private CharacterController m_Controller;
    private Animator m_Animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private GameManager gameManagerScript;


    private void Start()
    {
        m_Controller = gameObject.GetComponent<CharacterController>();
        m_Animator = gameObject.GetComponentInChildren<Animator>();

        gameManagerScript=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

//change to normal update if any bug
    void Update()
    {
        JumpForce = gameManagerScript.playerJumpForce;
        playerSpeed = gameManagerScript.playerSpeed;

        groundedPlayer = m_Controller.isGrounded;
        gameManagerScript.groundedPlayer = m_Controller.isGrounded;
        
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
            m_Animator.SetBool("IsFalling", false);

        }

        if(!groundedPlayer){
              m_Animator.SetBool("IsFalling", true);
        }

        if(groundedPlayer && gameManagerScript.isMindPowerUpCoolDownOver){ // do not update grounded position if mind powerup is active
        //    Debug.Log("g pos updated"+Random.Range(0,100000));
            gameManagerScript.lastGroundedPosition=transform.position;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //trasnform input into camera space
        var forward = GameCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = Vector3.Cross(Vector3.up, forward);
        
        Vector3 move = forward * input.z + right * input.x;
        move.y = 0;
        
        m_Controller.Move(move * Time.deltaTime * playerSpeed);

        m_Animator.SetFloat("MovementX", input.x);
        m_Animator.SetFloat("MovementZ", input.z);

        if (input != Vector3.zero)
        {
            gameObject.transform.forward = forward;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JumpForce * -3.0f * gravityValue);
            m_Animator.SetTrigger("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        m_Controller.Move(playerVelocity * Time.deltaTime);
    }

    // private void OnTriggerEnter(Collider other) {
    //     Debug.Log("bump"+other.name);
    //     if(other.name =="Ellen")
    //     {
    //      //Destroy(other.gameObject);   
    //      GameObject HomeGate = GameObject.FindWithTag("HomeGate");
    //      Destroy(HomeGate);
    //      Debug.Log("delete"+HomeGate.name);
    //     }
    // }
}
