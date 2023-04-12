using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private bool ePressed = false;
    private IInteractable _interactable;
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, 
            _colliders, _interactableMask);

        if (_numFound == 1) // If we actually found object (num will go up)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>(); // Will find any mono behavior that is implementing IInteractable interface
            if (_interactable != null)
            {
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);

                if (ePressed == false)
                {
                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        //thirdPersonMovement.enabled = false;
                        _interactable.Interact(this);
                        ePressed = true;
                    }
                    // Pass in "this" (player) because we are the interactor interactING with the interactable (object)
                }
                else
                {
                    if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
                }
            }
        }
        else
        {
            //thirdPersonMovement.enabled = true;
            ePressed = false;
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
