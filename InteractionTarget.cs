using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    [Tooltip("There is no need for putting modules into this list manualy lol")]
    public List<InteractionModule> InteractionModules = new List<InteractionModule>();

    private void Awake() {
        InteractionModules.AddRange(gameObject.GetComponents<InteractionModule>());
    }

    public void Interact(InteractionTrigger trigger) // triggers all interactions for this target
    {
        foreach (var target in InteractionModules) // triggers all associated interaction modules
        {
            Debug.Log($"{target.ModuleName} got invoked");
            if (target.requireInteractionTrigger == true)
            {
                target.OnGetInteractionTrigger(trigger);
            }
            target.OnInteract();
        }
    }

    private void OnValidate() {
        if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
    }

    public bool CancleInteraction()
    {
        if (InteractionModules.FindAll(InteractionTarget => InteractionTarget.allowCancelInteraction == true) != null)
        {
            foreach(var module in InteractionModules)
            {
                Debug.Log("Cancle Interaction ");
                module.OnCancleInteraction();
            }
            return true;
        }else
        {
            return false;
        }
    }
}
