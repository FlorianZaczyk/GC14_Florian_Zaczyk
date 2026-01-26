using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{
    public List<Interactable> interactables;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>())
        {
            interactables.Add(other.gameObject.GetComponent<Interactable>());
            //interactables[interactables.Count -1].onSelected?.Invoke();
            //other.gameObject.GetComponent<Interactable>().onSelected?.Invoke();
            interactables[^1].onSelected?.Invoke();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>())
        {
            other.GetComponent<Interactable>().onDeselected?.Invoke(); 
            interactables.Remove(other.GetComponent<Interactable>());
        }
    }

    public void TryInteract()
    {
        if (interactables.Count < 1) return;
        
        interactables[0].Interact();

        if (interactables[0] == null)
        {
            interactables.RemoveAt(0);
            return;
        }
        
        if (interactables[0].reusable)
        {
            Interactable currentInteractable = interactables[0];
            interactables.RemoveAt(0);
            interactables.Add(currentInteractable);
        }
        else
        {
            interactables.RemoveAt(0);
            
        }
    }
}