using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator hairAnimator;
    public Animator bodyAnimator;
    public Animator weaponAnimator;
    public Animator equipmentAnimator;
    public Animator backgroundEffectsAnimator;
    public Animations currentAnimation;
    int equippedWeaponID;
    int equippedEquipmentID;

    //Naming Conventions for hair and body are as follows:
    //For Attacking: Hair1SwordAttacking1, (animator type)(animator type index)(weapon type)(animation name)(combo count)
    //Not Attacking: Hair1Jumping, (animator type)(animator type index)(animation name)

    //Naming Conventions for weapons are as follows:
    //For Attacking: Sword1Attacking1, (weapon type)(weapon tier index)(animation name)(combo count)
    //Not Attacking: Sword1Jumping, (weapon type)(weapon tier index)(animation name)
    
    //Naming Conventions for equipment are as follows:
    //For Attacking: Wizard RobesSwordAttacking1, (equipment name)(weapon type)(animation name)(combo count)
    //Not Attacking: Wizard RobesJumping, (equipment name)(animation name)

    //Master list of animations.
    public enum Animations
    {
        Idle,
        Running,
        Jumping,
        Flinching,
        Climbing,
        ClimbingUp,
        Attacking,
        AerialAttacking,
        DeathFalling,
        DeathStanding,
        Buff,
        Ability,
        MovementAbility,
        Ultimate
    }

    //public void PlayAnimation (Animations animation)
    //{
    //    equippedWeaponID = (GameControl.gameControl.currentProfile == 1) ? GameControl.gameControl.profile1Weapon : GameControl.gameControl.profile2Weapon;
    //    equippedEquipmentID = (GameControl.gameControl.currentProfile == 1) ? GameControl.gameControl.profile1Equipment : GameControl.gameControl.profile2Equipment;
    //    currentAnimation = animation;
        
    //    PlayHairAnimation(animation);
    //    PlayBodyAnimation(animation);
    //    PlayEquipmentAnimation(animation);
    //    PlayWeaponAnimation(animation);
    //    if (animation == Animations.Buff || animation == Animations.Ability || animation == Animations.MovementAbility || animation == Animations.Ultimate)
    //    {
    //        PlayBackgroundEffectsAnimation(animation);
    //    }
    //    else
    //    {
    //        backgroundEffectsAnimator.Play("Nothing");
    //    }
    //}

    //public void PlayHairAnimation (Animations animation)
    //{
    //    string animationName = animation.ToString();
    //    if (animation == Animations.Attacking)
    //    {
    //        hairAnimator.Play("Hair" + 
    //                          GameControl.gameControl.hairIndex + 
    //                          EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentType + 
    //                          animationName + 
    //                          CombatEngine.combatEngine.comboCount);
    //    }
    //    else
    //    {
    //        hairAnimator.Play("Hair" + GameControl.gameControl.hairIndex + animationName);
    //    }
    //}

    //public void PlayBodyAnimation(Animations animation)
    //{
    //    string animationName = animation.ToString();
    //    if (animation == Animations.Attacking)
    //    {
    //        bodyAnimator.Play("Body" + 
    //                          GameControl.gameControl.skinColorIndex +
    //                          EquipmentDatabase.equipmentDatabase.equipment[equippedWeaponID].equipmentType + 
    //                          animationName +
    //                          CombatEngine.combatEngine.comboCount);
    //    }
    //    else
    //    {
    //        bodyAnimator.Play("Body" + GameControl.gameControl.skinColorIndex + animationName);
    //    }
    //}

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

    //public void PlayBackgroundEffectsAnimation (Animations animation)
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