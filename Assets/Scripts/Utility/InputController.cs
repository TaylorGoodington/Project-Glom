using UnityEngine;
using static Utility;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    private ButtonPress buttonPress = ButtonPress.None;

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
            ProcessInputForPlayerCharacter();
        }
        else if (GameControl.Instance.inputState == InputState.Menus)
        {

        }
        else if (GameControl.Instance.inputState == InputState.Dialogue)
        {

        }
    }

    private void ProcessInputForPlayerCharacter()
    {
        ProcessPlayerCharacterButtonInput();
        if (buttonPress != ButtonPress.None)
        {
            GameControl.Instance.player.ReceiveAndProcessButtonInput(buttonPress);
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GameControl.Instance.player.ReceiveAndProcessMovementInput(input);

        if (Input.GetButtonDown("Cycle"))
        {
            CycleSelectedSpell();
        }

        //Testing
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameControl.Instance.currentOffensiveSpell = OffensiveSpell.Blast;
            GameControl.Instance.currentOffensiveSpellVariant = OffensiveSpellVariant.Burst;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameControl.Instance.currentOffensiveSpell = OffensiveSpell.Blast;
            GameControl.Instance.currentOffensiveSpellVariant = OffensiveSpellVariant.Charge;
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

    private void ProcessPlayerCharacterButtonInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            buttonPress = ButtonPress.Interact;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            buttonPress = ButtonPress.Jump_Start;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            buttonPress = ButtonPress.Jump_End;
        }
        else if (Input.GetButtonDown("Cast"))
        {
            buttonPress = ButtonPress.Cast;
        }
        else
        {
            buttonPress = ButtonPress.None;
        }
    }

    public Vector2 CollectPlayerDirectionalInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}