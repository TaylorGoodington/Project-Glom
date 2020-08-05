using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject spellSlot;
    public GameObject userInterface;
    public List<Sprite> spells;
    public List<Sprite> userInterfaces;
    public List<GameObject> healthBars;

    public static GameObject staticSpellSlot;
    public static GameObject staticUserInterface;
    public static List<Sprite> staticSpells;
    public static List<Sprite> staticUserInterfaces;
    public static List<GameObject> staticHealthBars;

    private static GameObject healthBar;
    private static Transform parent;

    void Start ()
    {
        staticSpellSlot = spellSlot;
        staticUserInterface = userInterface;
        staticSpells = spells;
        staticUserInterfaces = userInterfaces;
        staticHealthBars = healthBars;
        parent = transform;
        UpdateUserInterface();
        GetComponent<Canvas>().worldCamera = GameControl.mainCamera;
	}

    //Updates all pieces of the UI, used at the begining of a level.
    private static void UpdateUserInterface ()
    {
        staticSpellSlot.GetComponent<SpriteRenderer>().sprite = staticSpells[GameControl.selectedSpellId];
        staticUserInterface.GetComponent<SpriteRenderer>().sprite = staticUserInterfaces[GameControl.healthLevel - 1];
        Destroy(healthBar);
        healthBar = Instantiate(staticHealthBars[GameControl.healthLevel - 1], parent);
    }

    public static void UpdateHealth ()
    {
        healthBar.GetComponent<Slider>().value = healthBar.GetComponent<Slider>().maxValue - GameControl.playerCurrentHP;
    }

    public static void UpdateSelectedSpell ()
    {
        staticSpellSlot.GetComponent<SpriteRenderer>().sprite = staticSpells[GameControl.selectedSpellId ];
    }
}