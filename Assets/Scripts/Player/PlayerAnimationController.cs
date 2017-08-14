using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator bodyAnimator;
    //public Animator weaponAnimator;
    //public Animator equipmentAnimator;
    //public Animator backgroundEffectsAnimator;
    //int equippedWeaponID;
    //int equippedEquipmentID;

    public void PlayAnimation(string animation, float facingDirecion)
    {
        PlayBodyAnimation(animation, facingDirecion);

        if (animation == "Summiting")
        {
            StartCoroutine(Summit());
        }

        //PlayEquipmentAnimation(animation);
        //PlayWeaponAnimation(animation);
        //if (animation == Animations.Buff || animation == Animations.Ability || animation == Animations.MovementAbility || animation == Animations.Ultimate)
        //{
        //    PlayBackgroundEffectsAnimation(animation);
        //}
        //else
        //{
        //    backgroundEffectsAnimator.Play("Nothing");
        //}
    }

    private IEnumerator Summit ()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(bodyAnimator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = new Vector3(transform.position.x, gameObject.GetComponent<Player>().climbingUpPosition);
        GameControl.playerHasControl = true;
    }

    private void PlayBodyAnimation(string animation, float facingDirecion)
    {
        if (animation != "Climbing")
        {
            if (facingDirecion == -1)
            {
                bodyAnimator.GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<BoxCollider2D>().offset = new Vector2(1.5f, -4.5f);
            }
            else if (facingDirecion == 1)
            {
                bodyAnimator.GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<BoxCollider2D>().offset = new Vector2(-1.5f, -4.5f);
            }
        }
        
        bodyAnimator.Play(animation);
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