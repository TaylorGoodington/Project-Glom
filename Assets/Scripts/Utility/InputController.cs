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
        if (GameControl.Instance.inputState == InputStates.Player_Character)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            ButtonPresses buttonPress = ProcessPlayerCharacterButtonInput();
            ProcessPlayerInput(input, buttonPress);

            if (Input.GetButtonDown("Cycle"))
            {
                CycleSelectedSpell();
            }
        }
        else if (GameControl.Instance.inputState == InputStates.Menus)
        {

        }
        else if (GameControl.Instance.inputState == InputStates.Dialogue)
        {

        }
    }

    private void CycleSelectedSpell()
    {
        GameData.Instance.CycleSelectedSpell();
        UserInterface.Instance.UpdateSelectedSpell();
    }

    private void ProcessPlayerInput(Vector2 input, ButtonPresses buttonPress)
    {
        GameControl.Instance.player.RecieveInput(input, buttonPress);
    }

    private ButtonPresses ProcessPlayerCharacterButtonInput()
    {
        ButtonPresses buttonPress = ButtonPresses.None;

        if (Input.GetButtonDown("Interact"))
        {
            buttonPress = ButtonPresses.Interact;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            buttonPress = ButtonPresses.Jump;
        }
        else if (Input.GetButtonDown("Cast"))
        {
            buttonPress = ButtonPresses.Cast;
        }

        return buttonPress;
    }

    public Vector2 CollectPlayerDirectionalInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}