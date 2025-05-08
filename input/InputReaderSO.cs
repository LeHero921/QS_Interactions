using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader",menuName = "INPUT/InputReader")]
public class InputReaderSO : ScriptableObject, INPUT.IUIActions, INPUT.IPlayerActions
{
    public INPUT inputActions;

    public event UnityAction OnSubmitAction;
    public event UnityAction OnCancelAction;
    public event UnityAction OnNavigateAction;

    public event UnityAction OnInteractAction;

    private void OnEnable() {
        Debug.Log($"[DEBUG] OnEnable() called for InputReaderSO at {Time.time}");
        if (inputActions == null)
        {
            inputActions = new INPUT();
            inputActions.UI.SetCallbacks(this);
            inputActions.Player.SetCallbacks(this);
        }
    
        inputActions.UI.Enable();
        inputActions.Player.Enable();
    
        Actions.OnInputSwitchPlayer -= LoadPlayerInput; // Falls doppelt registriert
        Actions.OnInputSwitchPlayer += LoadPlayerInput;
    
        Actions.OnInputSwitchUI -= LoadUIInput; // Falls doppelt registriert
        Actions.OnInputSwitchUI += LoadUIInput;
    }
    private void OnDisable() {
        Actions.OnInputSwitchPlayer -= LoadPlayerInput;
        Actions.OnInputSwitchUI -= LoadUIInput;
        inputActions.Disable();
    }

    public void LoadPlayerInput()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();
    }
    public void LoadUIInput()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
    }

    #region UI
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnCancelAction?.Invoke();
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.LogError("[INPUT] OnSubmit Performed");
            Debug.Log("SomeFunction was called!\n" + StackTraceUtility.ExtractStackTrace());
            OnSubmitAction?.Invoke();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnNavigateAction?.Invoke();
        }
    }
    #endregion

    #region Player
    public void OnMove(InputAction.CallbackContext context)
    {
        // Movement?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("[INPUT] Interacting...");
            OnInteractAction?.Invoke();
        }
    }
    #endregion

    #region Player_Unused
    public void OnAttack(InputAction.CallbackContext context)
    {
        ;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        ;
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        ;
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        ;
    }
    #endregion

    #region UI_Unused
    public void OnClick(InputAction.CallbackContext context)
    {
        ;
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {

    }

    public void OnRightClick(InputAction.CallbackContext context)
    {

    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {

    }
    #endregion
}
