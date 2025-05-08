using UnityEngine;

public abstract class InteractionModule : MonoBehaviour, IInteractable
{
    public string ModuleName;
    public bool allowCancelInteraction;
    public bool requireInteractionTrigger;
    public abstract void OnInteract();
    public abstract void OnCancleInteraction();
    public abstract void OnGetInteractionTrigger(InteractionTrigger trigger);
}

public interface IInteractable {
    /// <summary>
    /// IMPLEMENT: Actions.OnInteractionState?.Invoke();
    /// </summary>
    void OnInteract();
}