using UnityEngine;
using static Utility;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (GameControl.Instance.inputState == InputState.Player_Character)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            ButtonPress buttonPress = ProcessPlayerCharacterButtonInput();
            ProcessPlayerInput(input, buttonPress);

            if (Input.GetButtonDown("Cycle"))
            {
                CycleSelectedSpell();
            }
        }
        else if (GameControl.Instance.inputState == InputState.Menus)
        {

        }
        else if (GameControl.Instance.inputState == InputState.Dialogue)
        {

        }
    }

    private void CycleSelectedSpell()
    {
        GameData.Instance.CycleSelectedSpell();
        UserInterface.Instance.UpdateSelectedSpell();
    }

    private void ProcessPlayerInput(Vector2 input, ButtonPress buttonPress)
    {
        GameControl.Instance.player.RecieveInput(input, buttonPress);
    }

    private ButtonPress ProcessPlayerCharacterButtonInput()
    {
        ButtonPress buttonPress = ButtonPress.None;

        if (Input.GetButtonDown("Interact"))
        {
            buttonPress = ButtonPress.Interact;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            buttonPress = ButtonPress.Jump;
        }
        else if (Input.GetButtonDown("Cast"))
        {
            buttonPress = ButtonPress.Cast;
        }

        return buttonPress;
    }

    public Vector2 CollectPlayerDirectionalInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}