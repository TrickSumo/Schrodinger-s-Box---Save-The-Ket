using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_BAK : MonoBehaviour
{
    public List<Material> materials;
    private Renderer objectRenderer;
    private List<Renderer> objectRenderers;
    private List<GameObject> childrens;

    private enum CharacterState { zero, one, plus, minus };
    private CharacterState currentState = CharacterState.zero;
    private void Awake()

    {
        childrens = new List<GameObject>();
        objectRenderers = new List<Renderer>(); // Added to initialize the list

        // Get all the child transforms of this game object
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();

        // Loop through all the child transforms
        foreach (Transform childTransform in childTransforms)
        {

            // Check if the child has the "hi" tag
            if (childTransform.gameObject.CompareTag("PlayerSkin"))
            {
                // Add the child game object to the list
                childrens.Add(childTransform.gameObject);

                // Get the renderer component of the child game object
                Renderer childRenderer = childTransform.GetComponent<Renderer>();

                // If the child has a renderer component, add it to the list
                if (childRenderer != null)
                {
                    objectRenderers.Add(childRenderer);
                }
            }
        }

        objectRenderer = objectRenderers[0];


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("X"))
        {
            Debug.Log(other.gameObject.tag);
            if (currentState == CharacterState.zero) SetCharacterStateTo(CharacterState.one);
            else if (currentState == CharacterState.one) SetCharacterStateTo(CharacterState.zero);
        }
        else if (other.CompareTag("Z"))
        {
            Debug.Log(other.gameObject.tag);
            if (currentState == CharacterState.plus) SetCharacterStateTo(CharacterState.minus);
            else if (currentState == CharacterState.minus) SetCharacterStateTo(CharacterState.plus);

        }
        else if (other.CompareTag("H"))
        {
            Debug.Log(other.gameObject.tag);

            if (currentState == CharacterState.zero) SetCharacterStateTo(CharacterState.plus);
            else if (currentState == CharacterState.one) SetCharacterStateTo(CharacterState.minus);
            else if (currentState == CharacterState.plus) SetCharacterStateTo(CharacterState.zero);
            else if (currentState == CharacterState.minus) SetCharacterStateTo(CharacterState.one);

        }
    }

    private void SetCharacterStateTo(CharacterState newState)


    {
        Debug.Log(newState);
        //Existing current state
        switch (currentState)
        {
            case CharacterState.zero:
                break;
            case CharacterState.one:
                break;
            case CharacterState.plus:
                break;
            case CharacterState.minus:
                break;

        }

        //Entering new state
        switch (newState)
        {
            case CharacterState.zero:
                objectRenderer.material = materials[0];
                break;
            case CharacterState.one:
                objectRenderer.material = materials[3];
                break;
            case CharacterState.plus:
                objectRenderer.material = materials[1];
                break;
            case CharacterState.minus:
                objectRenderer.material = materials[2];
                break;

        }

        currentState = newState;


    }
}

