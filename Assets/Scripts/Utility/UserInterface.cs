using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Instance;

    public GameObject spellSlot;
    public GameObject userInterface;
    public List<Sprite> spells;
    public List<Sprite> userInterfaces;
    public List<GameObject> healthBars;

    //public static GameObject staticSpellSlot;
    //public static GameObject staticUserInterface;
    //public static List<Sprite> staticSpells;
    //public static List<Sprite> staticUserInterfaces;
    //public static List<GameObject> staticHealthBars;

    private GameObject healthBar;
    private Transform parent;

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

    void Start ()
    {
        //staticSpellSlot = spellSlot;
        //staticUserInterface = userInterface;
        //staticSpells = spells;
        //staticUserInterfaces = userInterfaces;
        //staticHealthBars = healthBars;
        parent = transform;
        UpdateUserInterface();
	}

    //Updates all pieces of the UI, used at the begining of a level.
    private void UpdateUserInterface ()
    {
        spellSlot.GetComponent<SpriteRenderer>().sprite = spells[GameData.Instance.selectedSpellId];
        userInterface.GetComponent<SpriteRenderer>().sprite = userInterfaces[GameData.Instance.healthLevel - 1];
        Destroy(healthBar);
        healthBar = Instantiate(healthBars[GameData.Instance.healthLevel - 1], parent);
    }

    public void UpdateHealth ()
    {
        healthBar.GetComponent<Slider>().value = healthBar.GetComponent<Slider>().maxValue - GameData.Instance.playerCurrentHP;
    }

    public void UpdateSelectedSpell ()
    {
        spellSlot.GetComponent<SpriteRenderer>().sprite = spells[GameData.Instance.selectedSpellId ];
    }
}