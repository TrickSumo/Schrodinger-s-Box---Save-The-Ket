using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloors : MonoBehaviour
{

    public bool isPowerUpFloor = true;
    private GameManager gameManagerScript;

    private Vector3 initialPos;
    private Quaternion initialRot;
    private Rigidbody floorRb;

    private void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        floorRb = GetComponent<Rigidbody>();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < gameManagerScript.yLowerLimit - 300)
        {
            StartCoroutine(RepositionFloor());
            // StartCoroutine(RemoveKinematicPropertyAfterDelay());
        }



        if (isPowerUpFloor) StartCoroutine(DestroyFloorAfterDelay());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPowerUpFloor)
        {
            StartCoroutine(FAllFloorAfterDelay());
        }
    }

    private IEnumerator FAllFloorAfterDelay()
    {

        yield return new WaitForSeconds(0.9f);
        floorRb.useGravity = true;

    }

    private IEnumerator DestroyFloorAfterDelay()
    {

        yield return new WaitForSeconds(0.3f);
        floorRb.useGravity = true;
        Destroy(gameObject);

    }

    private IEnumerator RepositionFloor()
    {

        yield return new WaitForSeconds(6f);
        floorRb.useGravity = false;
        floorRb.isKinematic = true;
        transform.rotation = initialRot;
        transform.position = initialPos;
        StartCoroutine(RemoveKinematicPropertyAfterDelay());

    }

    private IEnumerator RemoveKinematicPropertyAfterDelay()
    {

        yield return new WaitForSeconds(3f);
        floorRb.isKinematic = false;

    }



}
