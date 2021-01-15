using UnityEngine;
using System.Collections;
using static Utility;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator bodyAnimator;
    public Animator castingAnimator;
    public Animator scarAnimator;
    public Animator spellAnimator;

    public void PlayAnimation(string animation, float facingDirecion)
    {
        PlayBodyAnimation(animation, facingDirecion);

        if (animation == "Summiting")
        {
            StartCoroutine(Summit());
        }
    }

    private IEnumerator Summit ()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(bodyAnimator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = new Vector3(transform.position.x, gameObject.GetComponent<Player>().climbingUpPosition);
        GameControl.Instance.inputState = InputStates.Player_Character;
    }

    private void PlayBodyAnimation(string animation, float direction)
    {
        if (animation == "Climbing" || animation == "Summiting")
        {
            scarAnimator.gameObject.SetActive(false);
        }
        else
        {
            scarAnimator.gameObject.SetActive(true);

            if (direction == -1)
            {
                bodyAnimator.GetComponent<SpriteRenderer>().flipX = true;
                castingAnimator.GetComponent<SpriteRenderer>().flipX = true;
                scarAnimator.GetComponent<SpriteRenderer>().flipX = true;
                spellAnimator.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction == 1)
            {
                bodyAnimator.GetComponent<SpriteRenderer>().flipX = false;
                castingAnimator.GetComponent<SpriteRenderer>().flipX = false;
                scarAnimator.GetComponent<SpriteRenderer>().flipX = false;
                spellAnimator.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (animation == "AerialCasting")
        {
            castingAnimator.transform.localPosition = new Vector2(0, 2);
            spellAnimator.transform.localPosition = new Vector2(0, 2);
        }
        else
        {
            castingAnimator.transform.localPosition = new Vector2(0, 0);
            spellAnimator.transform.localPosition = new Vector2(0, 0);
        }

        bodyAnimator.Play(animation);
        scarAnimator.Play(animation);
        ChangeScarColor();
    }

    public void PlaySpellAnimation (int spellId)
    {
        castingAnimator.Play("Casting");
        spellAnimator.Play(PlayerSpellControl.Instance.spells[spellId].name);
        if (PlayerSpellControl.Instance.spells[spellId].hasProjectile)
        {
            StartCoroutine(InstantiateSpell(spellId));
        }
    }

    private IEnumerator InstantiateSpell (int spellId)
    {
        GameObject prefab = PlayerSpellControl.Instance.ReturnSpellProjectile(spellId);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(spellAnimator.GetCurrentAnimatorStateInfo(0).length);
        Vector3 position = transform.localPosition;
        position.z = -1;

        if (castingAnimator.transform.localPosition.y == 2)
        {
            position.y += 2;
        }

        GameObject spell = Instantiate(prefab, position, Quaternion.identity);
        spell.GetComponent<ProjectileMovement>().spellId = spellId;

        if (bodyAnimator.GetComponent<SpriteRenderer>().flipX == true)
        {
            spell.GetComponent<SpriteRenderer>().flipX = true;
            spell.GetComponent<BoxCollider2D>().offset = new Vector2(spell.GetComponent<BoxCollider2D>().offset.x * -1, spell.GetComponent<BoxCollider2D>().offset.y);
        }
        else if (bodyAnimator.GetComponent<SpriteRenderer>().flipX == false)
        {
            spell.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void ChangeScarColor ()
    {
        if (castingAnimator.gameObject.GetComponent<CastingAnimationController>().castingPhase == 1)
        {
            scarAnimator.GetComponent<SpriteRenderer>().color = new Color32(118, 128, 110, 255);
        }
        else if (castingAnimator.gameObject.GetComponent<CastingAnimationController>().castingPhase == 2)
        {
            scarAnimator.GetComponent<SpriteRenderer>().color = new Color32(202, 232, 176, 255);
        }
        else
        {
            scarAnimator.GetComponent<SpriteRenderer>().color = new Color32(66, 64, 69, 255);
        }
    }

    //public void PlayWeaponAnimation(Animations animation)
    //{
    //    string animationName = animation.ToString();
    //    if (animation == Animations.Attacking)
    //    {
    //        weaponAnimator.Play(EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentType.ToString() +
    //                            EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentTier.ToString() +
    //                            animationName +
    //                            CombatEngine.combatEngine.comboCount);
    //    }
    //    else
    //    {
    //        weaponAnimator.Play(EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentType.ToString() +
    //                            EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentTier.ToString() +
    //                            animationName);
    //    }
    //}

    //public void PlayEquipmentAnimation(Animations animation)
    //{
    //    string animationName = animation.ToString();
    //    if (animation == Animations.Attacking)
    //    {
    //        equipmentAnimator.Play(EquipmentDatabase.equipmentDatabase.equipment[equippedEquipmentID].equipmentName +
    //                               EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentType + 
    //                               animationName +
    //                               CombatEngine.combatEngine.comboCount);
    //    }
    //    else
    //    {
    //        equipmentAnimator.Play(EquipmentDatabase.equipmentDatabase.equipment[equippedEquipmentID].equipmentName + animationName);
    //    }
    //}

    //public void PlayBackgroundEffectsAnimation(Animations animation)
    //{
    //    string animationName = SkillsController.skillsController.selectedSkill.skillName;
    //    if (SkillsController.skillsController.selectedSkill.locationInScene == "Foreground")
    //    {
    //        backgroundEffectsAnimator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
    //    }
    //    else
    //    {
    //        backgroundEffectsAnimator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    //    }
    //    backgroundEffectsAnimator.Play(animationName);
    //}
}