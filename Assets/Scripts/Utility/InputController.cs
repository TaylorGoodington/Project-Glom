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

            //Testing
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameControl.Instance.currentOffensiveSpell = OffensiveSpell.Blast;
                GameControl.Instance.currentOffensiveSpellVariant = OffensiveSpellVariant.Burst;
                PlayerSpellControl.Instance.CastSpell();
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
        int currentListPosition = SaveDataController.Instance.availableSpellIds.IndexOf(GameControl.Instance.selectedSpellId);

        if (currentListPosition == (SaveDataController.Instance.availableSpellIds.Count - 1))
        {
            GameControl.Instance.selectedSpellId = SaveDataController.Instance.availableSpellIds[0];
        }
        else
        {
            GameControl.Instance.selectedSpellId = SaveDataController.Instance.availableSpellIds[currentListPosition + 1];
        }

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