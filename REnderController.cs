using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnderController : MonoBehaviour
{
    // Start is called before the first frame update
    public Material newMaterial; // The new material to apply

    private Renderer objectRenderer; // Reference 
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     // Debug.Log(gameObject);
        //     // Renderer rend = GetComponent<Renderer>();
        //     // Material mat = GetComponent<Material>();
        //     // // rend.enabled = false;
        //     Debug.Log(objectRenderer.material);

        //     objectRenderer.material = newMaterial;
        //     transform.position = new Vector3(0, 0, 0);
        //     Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        // }

    }
}
