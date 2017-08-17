using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    #region Variables
    [Tooltip("This field is used to specify which layers block the attacking and abilities raycasts.")]
    public LayerMask attackingLayer;

    public float initialMaxJumpHeight;
    private float maxJumpHeight;
    public float initialMinJumpHeight;
    private float minJumpHeight;
    public float timeToJumpApex;
    private bool verticalAcceleration;

    public float InitialMoveSpeed;
    private float moveSpeed;
    public float InitialClimbSpeed;
    private float climbSpeed;
    
    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .1f;
    private Vector2 input;
    //private bool attackLaunched;
    private bool isClimbable;
    private bool summiting;

    //private bool uninteruptable;

    [HideInInspector]
    public int knockBackForce;

    public float climbingUpPosition;

    private PlayerAnimationController animator;
    private Controller2D controller;
    //private BoxCollider2D playerCollider;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    
    //private float targetXMovement;
    //private int movementAbilityDirection;

    //public WeaponColliders weaponCollider;
    #endregion

    void Start()
    {
        GameControl.playerCurrentHP = 10;
        controller = GetComponent<Controller2D>();
        animator = GetComponent<PlayerAnimationController>();
        //playerCollider = GetComponent<BoxCollider2D>();
        
        ResetCharacterPhysics();
        controller.characterState = Controller2D.CharacterStates.Standing;
        //attackLaunched = false;
    }

    void Update()
    {
        if (GameControl.playerHasControl)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            DetermineState(input);
            controller.Move(StateResult(input));
        }
        

        #region Old

        #region Dying
        //if (controller.characterState == Controller2D.CharacterStates.Dying)
        //{
        //    //animator.PlayAnimation(PlayerAnimationController.Animations.DeathStanding);
        //}
        //else if (GameControl.playerCurrentHP <= 0)
        //{
        //    //GameControl.gameControl.dying = true;
        //    if (controller.collisions.below)
        //    {
        //        input = Vector2.zero;
        //        GameControl.playerHasControl = false;
        //        UnPauseAnimators();
        //        //animator.PlayAnimation(PlayerAnimationController.Animations.DeathFalling);
        //        //MusicController.PlayTrack(1);
        //    }
        //    //Player needs to hit the ground before the animation plays.
        //    else
        //    {
        //        gravity = -1000;
        //        velocity.y += gravity * Time.deltaTime;
        //        //controller.Move(velocity * Time.deltaTime, new Vector2(1, 0));
        //    }
        //}
        #endregion

        #region Flinching Section
        //else if (controller.characterState == Controller2D.CharacterStates.Flinching)
        //{
        //    UnPauseAnimators();
        //    attackLaunched = false;
        //    //weaponCollider.DisableActiveCollider();
        //    //SkillsController.skillsController.activatingAbility = false;
        //    //CombatEngine.combatEngine.comboCount = 1;
        //    //animator.PlayAnimation(PlayerAnimationController.Animations.Flinching);
        //    //PlayerSoundEffects.playerSoundEffects.PlaySoundEffect(PlayerSoundEffects.playerSoundEffects.SoundEffectToArrayInt(PlayerSoundEffects.SoundEffect.MenuUnable));

        //    gravity = -1000;
        //    velocity.y = 0;
        //    velocity.y += gravity * Time.deltaTime;
        //    velocity.x = 0;
        //    //controller.Move(velocity * Time.deltaTime, input);
        //}
        #endregion
        //else if (controller.characterState == Controller2D.CharacterStates.KnockedBack)
        //{
        //    gravity = -1000;
        //    velocity.y = 0;
        //    velocity.y += gravity * Time.deltaTime;
        //    //velocity.x = (CombatEngine.combatEngine.enemyKnockBackForce / flinchTime) * CombatEngine.combatEngine.enemyFaceDirection;
        //    //controller.Move(velocity * Time.deltaTime, input);
        //}

        #region Movement Ability Section
        //else if (controller.characterState == Controller2D.CharacterStates.AbilityMoving)
        //{
        //    //if (movementAbilityDirection == 1)
        //    //{
        //    //    if (transform.position.x < targetXMovement && !controller.collisions.right)
        //    //    {
        //    //        velocity.x = 300 * Time.deltaTime;
        //    //        velocity.y = 0;
        //    //        controller.Move(velocity, input);
        //    //    }
        //    //    else
        //    //    {
        //    //        //SkillsController.skillsController.activatingAbility = false;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (transform.position.x > targetXMovement && !controller.collisions.left)
        //    //    {
        //    //        velocity.x = 300 * -1 * Time.deltaTime;
        //    //        controller.Move(velocity, input);
        //    //    }
        //    //    else
        //    //    {
        //    //        //SkillsController.skillsController.activatingAbility = false;
        //    //    }
        //    //}
        //}
        #endregion
        //else
        //{
        //    //If a menu is open or the player doesn't have control.
        //    if (GameControl.inMenus == true || GameControl.playerHasControl == false)
        //    {
        //        input = Vector2.zero;
        //    }

        //    else if (GameControl.inMenus == false || GameControl.playerHasControl == true)
        //    {
        //        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        #region Wall Jumping
        //int wallDirX = (controller.collisions.left) ? -1 : 1;
        //bool wallSliding = false;
        //if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && controller.wallJumpReady == true)
        //{
        //    wallSliding = true;

        //    if (velocity.y < -wallSlideSpeedMax)
        //    {
        //        velocity.y = -wallSlideSpeedMax;
        //    }

        //    if (timeToWallUnstick > 0)
        //    {
        //        velocityXSmoothing = 0;
        //        velocity.x = 0;

        //        if (input.x != wallDirX && input.x != 0)
        //        {
        //            timeToWallUnstick -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            timeToWallUnstick = wallStickTime;
        //        }
        //    }
        //    else
        //    {
        //        timeToWallUnstick = wallStickTime;
        //    }
        //}
        #endregion

        //Stops aerial attacking once on the ground.
        //if (controller.collisions.below && animator.currentAnimation == PlayerAnimationController.Animations.AerialAttacking)
        //{
        //    animator.PlayAnimation(PlayerAnimationController.Animations.Idle);
        //}

        //Launching an attack.
        //if (Input.GetButtonDown("Attack") && !attackLaunched)
        //{
        //    attackLaunched = true;
        //    velocity.y = 0;
        //}

        #region Jumping
        //cant jump if attacking.
        //if (controller.characterState != Controller2D.CharacterStates.Attacking)
        //{
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        if (controller.characterState == Controller2D.CharacterStates.Climbing)
        //        {
        //            velocity.x = controller.collisions.faceDir * wallJumpClimb.x;
        //            velocity.y = wallJumpOff.y;
        //        }

        //        if (wallSliding)
        //        {
        //            if (wallDirX == input.x)
        //            {
        //                velocity.x = -wallDirX * wallJumpClimb.x;
        //                velocity.y = wallJumpClimb.y;
        //            }
        //            else if (input.x == 0)
        //            {
        //                velocity.x = -wallDirX * wallJumpOff.x;
        //                velocity.y = wallJumpOff.y;
        //            }
        //            else
        //            {
        //                velocity.x = -wallDirX * wallLeap.x;
        //                velocity.y = wallLeap.y;
        //            }
        //        }
        //        if (controller.collisions.below)
        //        {
        //            velocity.y = maxJumpVelocity;
        //        }
        //    }
        //    if (Input.GetButtonUp("Jump"))
        //    {
        //        if (velocity.y > minJumpVelocity)
        //        {
        //            velocity.y = minJumpVelocity;
        //        }
        //    }
        //}
        #endregion

        //climbing stuff
        //    if (isClimbable)
        //    {
        //        if (Input.GetButtonDown("Interact"))
        //        {
        //            velocity.y = 0;
        //        }
        //    }
        //}

        //flips sprite depending on direction facing.
        //if (controller.collisions.faceDir == 1)
        //{
        //    //animator.hairAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    //animator.bodyAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    //animator.equipmentAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    //animator.weaponAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    //animator.backgroundEffectsAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else
        //{
        //    //animator.hairAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    //animator.bodyAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    //animator.equipmentAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    //animator.weaponAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    //animator.backgroundEffectsAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //}

        ////cant move if attacking.
        //if (controller.characterState == Controller2D.CharacterStates.Attacking)
        //{
        //    input = Vector2.zero;
        //}

        #region Abilities
        //if (!SkillsController.skillsController.activatingAbility)
        //{
        //    if (Input.GetButtonDown("Ability Cycle"))
        //    {
        //        SkillsController.skillsController.NextSlottedAbility();
        //    }

        //    if (!climbingUp)
        //    {
        //        if (Input.GetButtonDown("Use Ability") && controller.collisions.below)
        //        {
        //            SkillsController.skillsController.activatingAbility = true;
        //        }
        //    }
        //}
        #endregion

        #region Animations
        //if (controller.characterState == Controller2D.CharacterStates.Summiting)
        //{
        //    UnPauseAnimators();
        //animator.PlayAnimation(PlayerAnimationController.Animations.ClimbingUp);
        //}

        //if (SkillsController.skillsController.activatingAbility & !climbingUp)
        //{
        //    UnPauseAnimators();
        //    ActivateAbility();
        //}

        //if (attackLaunched && !climbingUp && !SkillsController.skillsController.activatingAbility)
        //{
        //    if (controller.collisions.below)
        //    {
        //        UnPauseAnimators();
        //        animator.PlayAnimation(PlayerAnimationController.Animations.Attacking);
        //    }
        //    else
        //    {
        //        UnPauseAnimators();
        //        animator.PlayAnimation(PlayerAnimationController.Animations.AerialAttacking);
        //    }
        //}

        //if (climbing && !isAttacking && !climbingUp && !SkillsController.skillsController.activatingAbility)
        //{
        //    UnPauseAnimators();
        //    animator.PlayAnimation(PlayerAnimationController.Animations.Climbing);
        //}
        //if (climbing && (velocity.y == 0 && velocity.x == 0) && !isAttacking && !climbingUp && !SkillsController.skillsController.activatingAbility)
        //{
        //    Invoke("PauseAnimators", 0.1f);
        //}

        //if (velocity.y != 0 && controller.collisions.below == false && isAttacking == false && !climbing && !climbingUp && !climbingUpMovement && !SkillsController.skillsController.activatingAbility)
        //{
        //    UnPauseAnimators();
        //    animator.PlayAnimation(PlayerAnimationController.Animations.Jumping);
        //}

        //if ((input.x != 0 && controller.collisions.below == true && !climbing && !climbingUp) || GameControl.gameControl.endOfLevel == true && !SkillsController.skillsController.activatingAbility)
        //{
        //    UnPauseAnimators();
        //    animator.PlayAnimation(PlayerAnimationController.Animations.Running);
        //}

        //if (input.x == 0 && isAttacking == false && controller.collisions.below == true && !climbing && !climbingUp && GameControl.gameControl.endOfLevel == false && !SkillsController.skillsController.activatingAbility)
        //{
        //    UnPauseAnimators();
        //    animator.PlayAnimation(PlayerAnimationController.Animations.Idle);
        //}
        #endregion

        //float targetVelocityX = input.x * moveSpeed;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        ////Stops movement if colliding with a wall, prevents sliding through based on residual velocity.
        //if (controller.collisions.left && controller.collisions.faceDir == -1)
        //{
        //    velocity.x = 0;
        //}
        //else if (controller.collisions.right && controller.collisions.faceDir == 1)
        //{
        //    velocity.x = 0;
        //}

        //#region Y Velocity Adjustments
        //if (controller.characterState == Controller2D.CharacterStates.Climbing)
        //{
        //    gravity = 0;
        //    velocity.y = input.y * climbSpeed;
        //    velocity.x = input.x * climbSpeed;

        //    if (transform.position.y >= climbingUpPosition)
        //    {
        //        velocity = Vector3.zero;
        //        Invoke("MovePlayerWhenClimbingUp", 0.125f);
        //    }

        //}
        //else
        //{
        //    gravity = -1000;
        //    velocity.y += gravity * Time.deltaTime;
        //}
        //#endregion

        //controller.Move(velocity * Time.deltaTime);

        //if (controller.collisions.above || controller.collisions.below)
        //{
        //    velocity.y = 0;
        //}
        //}
        #endregion
    }

    private void DetermineState (Vector2 input)
    {
        if (GameControl.playerCurrentHP <= 0)
        {
            controller.characterState = Controller2D.CharacterStates.Dying;
        }
        else if (isClimbable && Input.GetButtonDown("Interact"))
        {
            controller.characterState = Controller2D.CharacterStates.Climbing;
        }
        else if ((Input.GetButtonDown("Jump")) && (controller.collisions.below || controller.characterState == Controller2D.CharacterStates.Climbing))
        {
            verticalAcceleration = true;
            controller.characterState = Controller2D.CharacterStates.Jumping;
        }
        else if (!controller.collisions.below && (controller.characterState != Controller2D.CharacterStates.Climbing || !isClimbable))
        {
            controller.characterState = Controller2D.CharacterStates.Jumping;
        }
        else if (input.x == 0 && controller.collisions.below)
        {
            controller.characterState = Controller2D.CharacterStates.Standing;
        }
        else if (controller.collisions.below && input.x != 0)
        {
            controller.characterState = Controller2D.CharacterStates.Running;
        }
        else if (summiting && controller.characterState == Controller2D.CharacterStates.Climbing)
        {
            controller.characterState = Controller2D.CharacterStates.Summiting;
            climbingUpPosition = transform.position.y + 10;
        }
    }

    private Vector2 StateResult (Vector2 input)
    {
        if (controller.characterState == Controller2D.CharacterStates.Dying)
        {
            UnPauseAnimators();
            GameControl.playerHasControl = false;

            if (controller.collisions.below)
            {
                velocity = Vector2.zero;
            }
            else
            {
                velocity.x = 0;
                velocity.y += gravity * Time.deltaTime;
            }
        }
        else if (controller.characterState == Controller2D.CharacterStates.Standing)
        {
            UnPauseAnimators();

            if (velocity.x > 0)
            {
                velocity.x -= 0.1f;
            }
            else if (velocity.x < 0)
            {
                velocity.x += 0.1f;
            }

            if (velocity.x > -0.1 && velocity.x < 0.1)
            {
                velocity.x = 0;
            }

            velocity.y = 0;
            velocity.y += gravity * Time.deltaTime;
        }
        else if (controller.characterState == Controller2D.CharacterStates.Running)
        {
            UnPauseAnimators();

            velocity.y = 0;
            velocity.y += gravity * Time.deltaTime;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }
        else if (controller.characterState == Controller2D.CharacterStates.Jumping)
        {
            UnPauseAnimators();

            if (verticalAcceleration)
            {
                velocity.y = maxJumpVelocity;
                verticalAcceleration = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
            }

            if (controller.collisions.above)
            {
                velocity.y = 0;
            }

            velocity.y += gravity * Time.deltaTime;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }
        else if (controller.characterState == Controller2D.CharacterStates.Climbing)
        {
            velocity.y = input.y * climbSpeed;
            velocity.x = input.x * climbSpeed;
            PlayAnimation(input.x);

            if (input == Vector2.zero)
            {
                Invoke("PauseAnimators", 0.1f);
            }
            else
            {
                UnPauseAnimators();
            }

            return velocity;
        }
        else if (controller.characterState == Controller2D.CharacterStates.Summiting)
        {
            UnPauseAnimators();
            velocity = Vector2.zero;
            GameControl.playerHasControl = false;
        }

        PlayAnimation(input.x);
        return velocity;
    }

    public void AdjustCharacterPhysics(float maxJumpModifier, float minJumpModifier, float moveSpeedModifier, float climbSpeedModifier)
    {
        maxJumpHeight *= maxJumpModifier;
        minJumpHeight *= minJumpModifier;
        moveSpeed *= moveSpeedModifier;
        climbSpeed *= climbSpeedModifier;
        maxJumpVelocity = (Mathf.Abs(gravity) * (timeToJumpApex)) * ((Mathf.Pow(maxJumpHeight, -0.5221f)) * 0.1694f) * maxJumpHeight;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void ResetCharacterPhysics ()
    {
        maxJumpHeight = initialMaxJumpHeight / 56;
        minJumpHeight = initialMinJumpHeight / 56;
        moveSpeed = InitialMoveSpeed;
        climbSpeed = InitialClimbSpeed;

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void PlayAnimation (float input)
    {
        animator.PlayAnimation(controller.characterState.ToString(), input);
    }

    //private void ActivateAbility()
    //{
    //    int skillID = SkillsController.skillsController.selectedSkill.skillID;

    //    if (!callingActivateAbility)
    //    {
    //        callingActivateAbility = true;
    //        SkillsController.skillsController.ActivateAbilityTest(skillID);
    //    }
    //    else
    //    {
    //        if (SkillsController.skillsController.activatingAbility && SkillsDatabase.skillsDatabase.skills[skillID].animationType != Skills.AnimationType.None)
    //        {
    //            input = Vector2.zero;
    //            isAttacking = false;
    //            attackLaunched = false;
    //            if (SkillsController.skillsController.selectedSkill.animationType == Skills.AnimationType.Ability)
    //            {
    //                animator.PlayAnimation(PlayerAnimationController.Animations.Ability);
    //            }
    //            else if (SkillsController.skillsController.selectedSkill.animationType == Skills.AnimationType.Buff)
    //            {
    //                animator.PlayAnimation(PlayerAnimationController.Animations.Buff);
    //            }
    //            else if (SkillsController.skillsController.selectedSkill.animationType == Skills.AnimationType.MovementAbility)
    //            {
    //                Skills skill = SkillsController.skillsController.selectedSkill;
    //                targetXMovement = transform.position.x + (skill.knockbackForce * controller.collisions.faceDir);
    //                animator.PlayAnimation(PlayerAnimationController.Animations.MovementAbility);
    //                movementAbilityDirection = controller.collisions.faceDir;
    //            }
    //            else
    //            {
    //                animator.PlayAnimation(PlayerAnimationController.Animations.Ultimate);
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("no animation/requirements not met.");
    //            callingActivateAbility = false;
    //            SkillsController.skillsController.activatingAbility = false;
    //        }
    //    }
    //}

    //Called from combat engine.

    public void Death()
    {
        GameControl.playerCurrentHP = 0;
    }

    //Triggers dictate climbing, interactables, level triggers, and other things.
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 13)
        {
            isClimbable = true;
        }

        if (collider.gameObject.layer == 14)
        {
            summiting = true;
        }

        //Reaching the Goal
        //if (collider.gameObject.layer == 18)
        //{
        //    GameControl.gameControl.endOfLevel = true;
        //    velocity.x = 1 * moveSpeed;
        //    UserInterface uI = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UserInterface>();
        //    uI.EndOfLevel();
        //}

        //Falling off the world
        if (collider.gameObject.layer == 19)
        {
            GameControl.playerCurrentHP = 0;
        }

        //Interactable Objects
        if (collider.gameObject.layer == 21)
        {
            //GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UserInterface>().showInteractableDisplay = true;
        }

        if (collider.tag == "EnemyWeaponCollider")
        {
            //GameObject enemy = collider.transform.parent.gameObject;
            //CombatEngine.combatEngine.AttackingPlayer(enemy.GetComponent<Collider2D>(), enemy.GetComponent<EnemyStats>().maximumDamage);
        }
    }

    //this will be used to gauge interactions...I might need to do these things in the climbable script.
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 13)
        {
            isClimbable = false;
        }

        //Interactable Objects
        if (collider.gameObject.layer == 14)
        {
            summiting = false;
        }
    }

    //called from attacking animation at the begining and end.
    public void IsAttacking()
    {
        //isAttacking = !isAttacking;
    }

    //Resets the ability to attack & calls the combo countdown, called by the animation.
    public void EndOfAttack()
    {
        //attackLaunched = false;
        //CombatEngine.combatEngine.runComboClock = true;
        //CombatEngine.combatEngine.comboCountDown = CombatEngine.combatEngine.comboWindow;
        //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //if (CombatEngine.combatEngine.comboCount < CombatEngine.combatEngine.maxCombos && player.controller.collisions.below)
        //{
        //    CombatEngine.combatEngine.comboCount++;
        //}
        //else
        //{
        //    CombatEngine.combatEngine.comboCount = 1;
        //}
        //weaponCollider.DisableActiveCollider();
    }

    public void PauseAnimators()
    {
        animator.enabled = false;
        //animator.hairAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = false;
        //animator.equipmentAnimator.gameObject.GetComponent<Animator>().enabled = false;
        //animator.weaponAnimator.gameObject.GetComponent<Animator>().enabled = false;
        //animator.backgroundEffectsAnimator.gameObject.GetComponent<Animator>().enabled = false;
    }

    public void UnPauseAnimators()
    {
        animator.enabled = true;
        //animator.hairAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = true;
        //animator.equipmentAnimator.gameObject.GetComponent<Animator>().enabled = true;
        //animator.weaponAnimator.gameObject.GetComponent<Animator>().enabled = true;
        //animator.backgroundEffectsAnimator.gameObject.GetComponent<Animator>().enabled = true;
    }

    //called by the animator.
    public void FullyRevived()
    {
        //deathStanding = false;
    }


    //called from the animations for attacking.
    public void Attack()
    {
        //knockBackForce = EquipmentDatabase.equipmentDatabase.equipment[GameControl.gameControl.profile1Weapon].knockbackForce;
        //CombatEngine.combatEngine.enemyKnockBackDirection = controller.collisions.faceDir;

        ////Wizards(4) and Rangers(3) fire a projectile that calls attack on contact.
        //int projectileNumber = EquipmentDatabase.equipmentDatabase.equipment[GameControl.gameControl.profile1Weapon].equipmentTier - 1;
        //if (GameControl.gameControl.playerClass == 3)
        //{
        //    if (controller.collisions.below)
        //    {
        //        Instantiate(ClassesDatabase.classDatabase.arrows[projectileNumber], transform.position, Quaternion.identity);
        //    }
        //    else
        //    {
        //        Instantiate(ClassesDatabase.classDatabase.arrows[projectileNumber], new Vector3(transform.position.x, transform.position.y + 9), Quaternion.identity);
        //    }
        //}
        //else if (GameControl.gameControl.playerClass == 4)
        //{
        //    if (controller.collisions.below)
        //    {
        //        Instantiate(ClassesDatabase.classDatabase.magicMissles[projectileNumber], transform.position, Quaternion.identity);
        //    }
        //    else
        //    {
        //        Instantiate(ClassesDatabase.classDatabase.magicMissles[projectileNumber], new Vector3(transform.position.x, transform.position.y + 9), Quaternion.identity);
        //    }
        //}
        //else
        //{
        //    weaponCollider.ActivateWeaponCollider(controller.collisions.below);
        //}
    }
}