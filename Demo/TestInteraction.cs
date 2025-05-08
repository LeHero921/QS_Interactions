using UnityEngine;

public class TestInteraction : InteractionModule
{
    public override void OnCancleInteraction(){}

    public override void OnGetInteractionTrigger(InteractionTrigger trigger){}

    public override void OnInteract()
    {
        Debug.Log("Player Interacted with this module that is YOU...");
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
