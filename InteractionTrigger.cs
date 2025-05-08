using System.Collections;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    public float interactionDistance;
    public bool usePlayerLookDirection;
    public Vector2 injectDirection;

    [Tooltip("-> Same layer as Player")]
    public LayerMask layerMask;

    public bool cooldown;
    public bool isInteracting;

    private InteractionTarget interactingObject;

    private void Awake() {
        if (usePlayerLookDirection == true)
        {
            inputReader.inputActions.Player.Move.performed += ctx =>
            {
                injectDirection = ctx.ReadValue<Vector2>();
            };
        }
    }

    private void OnEnable() {
        inputReader.OnInteractAction += Interact;
        inputReader.OnCancelAction += SubmitInteractionCancleRequest;
        Actions.OnInteractionState += RecieveInteraction;
        Actions.OnInteractionCooldown += StartCooldown;
    }
    private void OnDisable() {
        inputReader.OnInteractAction -= Interact;
        inputReader.OnCancelAction -= SubmitInteractionCancleRequest;
        Actions.OnInteractionState -= RecieveInteraction;
        Actions.OnInteractionCooldown -= StartCooldown;
    }

    private void Update() {
        if (!usePlayerLookDirection)
        {
            Debug.DrawRay(transform.position, transform.right, Color.black);
        }else
        {
            Debug.DrawRay(transform.position, injectDirection, Color.green);
        }
    }

    public void Interact()
    {
        Debug.Log("[InteractionTrigger] Interacting...");
        if (usePlayerLookDirection == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactionDistance, layerMask);
            interactingObject = hit.collider?.GetComponent<InteractionTarget>();
            Debug.Log($"{gameObject.name} is interacting with: {hit.collider.gameObject.name}!");
            if (hit.collider.GetComponent<InteractionTarget>() != null && isInteracting == false && cooldown == false)
            {
                hit.collider.GetComponent<InteractionTarget>().Interact(this);
            }
        }else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, injectDirection, interactionDistance, layerMask);
            interactingObject = hit.collider?.GetComponent<InteractionTarget>();
            Debug.Log($"{gameObject.name} is interacting with: {hit.collider.gameObject.name}!");
            if (hit.collider.GetComponent<InteractionTarget>() != null && isInteracting == false && cooldown == false)
            {
                hit.collider.GetComponent<InteractionTarget>().Interact(this);
            }
        }
    }

    public void StartCooldown()
    {
        Debug.LogError("Starting interaction cooldown");
        StartCoroutine(InteractionCooldown());
    }
    private IEnumerator InteractionCooldown()
    {
        cooldown = true;
        yield return new WaitForSecondsRealtime(1);
        cooldown = false;
    }

    public void RecieveInteraction()
    {
        if (isInteracting == false)
        {
            isInteracting = true;
        }else
        {
            isInteracting = false;
        }
    }

    public void SubmitInteractionCancleRequest()
    {
        Debug.LogWarning("Submitting cancle Request: " + this.name);
        if (interactingObject.CancleInteraction() == true)
        {
            isInteracting = false;
        }
    } 
}