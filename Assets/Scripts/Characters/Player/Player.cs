using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    #region Variables
    [Tooltip("This field is used to specify which layers block the attacking and abilities raycasts.")]
    public LayerMask attackingLayer;

    public float heightReached = 0;
    private float initialHeight;

    public int initialMaxJumpDistance;
    private int maxJumpDistance;
    private int maxJumpHeight;
    public int initialMinJumpDistance;
    private int minJumpDistance;
    private int minJumpHeight;
    //public float timeToJumpApex;

    public float InitialMoveSpeed;
    public float moveSpeed;

    public float InitialClimbSpeed;
    public float climbSpeed;
    
    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .1f;

    private bool isClimbable;
    private bool summiting;
    private bool casting;

    private int faceDirection = 1;

    [HideInInspector] public int knockBackForce;
    [HideInInspector] public float climbingUpPosition;

    private PlayerAnimationController animator;
    private Controller2D controller;

    private float gravity = -10f;
    public float jumpVelocity;
    private float minJumpVelocity;
    public Vector2 velocity;
    private Vector2 input;
    private float velocityXSmoothing;

    public Dictionary<int, float> cooldownList;
    #endregion

    void Start()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<PlayerAnimationController>();
        casting = false;
        initialHeight = transform.position.y;

        //gravity = -(2 * maxJumpDistance) / Mathf.Pow(timeToJumpApex, 2);

        ResetCharacterPhysics();
        ResetState();
    }

    void Update()
    {
        if (!controller.collisions.below && controller.reactionState == ReactionState.Dying)
        {
            velocity.y += gravity;
            controller.Move(velocity, input);
        }
    }

    private void FixedUpdate()
    {
        DetermineStateResult();
        controller.Move(velocity * Time.fixedDeltaTime, input);
    }

    public void ResetState()
    {
        controller.reactionState = ReactionState.None;
        controller.movementState = MovementState.Standing;
        controller.actionState = ActionState.None;
    }

    public void ReceiveMovementInput(Vector2 input)
    {
        ProcessMovementInput(input);
    }

    public void ReceiveButtonInput(ButtonPress buttonPress)
    {
        ProcessButtonPress(buttonPress);
    }

    public void ReceiveReactionInput(ReactionState reactionState)
    {
        ProcessReactionInput(reactionState);
    }

    private void ProcessButtonPress(ButtonPress buttonPress)
    {
        //Casting
        if (buttonPress == ButtonPress.Cast)
        {
            if (controller.movementState != MovementState.Climbing && CanCast())
            {
                controller.actionState = ActionState.Casting;
            }
        }

        //Jumping
        else if (buttonPress == ButtonPress.Jump)
        {
            if (controller.movementState != MovementState.Falling)
            {
                controller.actionState = ActionState.Jumping;
                minJumpHeight = Mathf.RoundToInt(transform.position.y) + minJumpDistance;
                maxJumpHeight = Mathf.RoundToInt(transform.position.y) + maxJumpDistance;
            }
        }

        //Interacting
        else if (buttonPress == ButtonPress.Interact)
        {
            if (isClimbable)
            {
                controller.actionState = ActionState.Grabbing_Ladder;
            }
        }

        //None
        else
        {
            controller.actionState = ActionState.None;
        }
    }

    private void ProcessMovementInput(Vector2 input)
    {
        this.input = input;

        if (input.x == 1)
        {
            faceDirection = 1;
        }
        else if (input.x == -1)
        {
            faceDirection = -1;
        }

        if (controller.collisions.below)
        {
            if (input.x == 0)
            {
                controller.movementState = MovementState.Standing;
            }
            else
            {
                controller.movementState = MovementState.Running;
            }
        }
        else
        {
            if (controller.movementState != MovementState.Climbing)
            {
                controller.movementState = MovementState.Falling;
            }
            else
            {
                controller.movementState = MovementState.Climbing;
            }
        }
    }

    private void ProcessReactionInput(ReactionState reactionState)
    {
        if (reactionState == ReactionState.Summiting && controller.movementState == MovementState.Climbing)
        {
            controller.reactionState = ReactionState.Summiting;
        }
        else if (reactionState == ReactionState.Dying)
        {
            controller.reactionState = ReactionState.Dying;
        }
        else if (reactionState == ReactionState.Flinching)
        {
            //Not needed at the moment
        }
        else if (reactionState == ReactionState.KnockedBack)
        {
            //Not needed at the moment
        }
        else
        {
            controller.reactionState = ReactionState.None;
        }

        DetermineStateResult();
    }

    private void DetermineStateResult()
    {
        CancelInvoke("PauseAnimators");
        UnPauseAnimators();

        #region Reaction States
        //Dying
        if (controller.reactionState == ReactionState.Dying)
        {
            controller.movementState = MovementState.Standing;
            controller.actionState = ActionState.None;
            animator.animationState = Utility.AnimationState.Dying;
        }

        //Summiting
        else if (controller.reactionState == ReactionState.Summiting)
        {
            controller.movementState = MovementState.Standing;
            controller.actionState = ActionState.None;
            animator.animationState = Utility.AnimationState.Dying;
            climbingUpPosition = transform.position.y + 10;
        }

        //Flinching

        //Knockback
        #endregion

        #region Action States
        //Casting
        else if (controller.actionState == ActionState.Casting)
        {
            PlayerSpellControl.Instance.CastSpell();

            if (controller.movementState == MovementState.Standing)
            {
                //ask if we are channeling

            }
            else if (controller.movementState == MovementState.Running)
            {
                animator.animationState = Utility.AnimationState.RunCasting;
            }
            else if (controller.movementState == MovementState.Falling)
            {
                animator.animationState = Utility.AnimationState.AerialCasting;
            }
        }

        //Jumping
        else if (controller.actionState == ActionState.Jumping)
        {
            animator.animationState = Utility.AnimationState.Jumping;
            //pushing the button will have keep action state as jumping until minimum jump height is reached
            //holding down the button will allow jump height to build until max is reached

            velocity.y = jumpVelocity;
            controller.actionState = ActionState.None;

            if (transform.position.y >= maxJumpHeight)
            {

            }

            if (controller.collisions.above)
            {
                velocity.y = 0;
            }
        }

        //Grabbing Ladder
        else if (controller.actionState == ActionState.Grabbing_Ladder)
        {
            animator.animationState = Utility.AnimationState.Climbing;
        }
        #endregion

        #region Movement States
        //Running
        else if (controller.movementState == MovementState.Running)
        {
            velocity.y = 0;
            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            animator.animationState = Utility.AnimationState.Running;
        }

        //Standing
        else if (controller.movementState == MovementState.Standing)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y = 0;
            velocity.y += gravity;
            animator.animationState = Utility.AnimationState.Standing;
        }

        //Falling
        else if (controller.movementState == MovementState.Falling)
        {
            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            UpdateHeightReached();
            animator.animationState = Utility.AnimationState.Jumping;
        }

        //Climbing
        else if (controller.movementState == MovementState.Climbing)
        {
            velocity.y *= climbSpeed;
            velocity.x *= climbSpeed;
            //PlayAnimation();

            if (velocity == Vector2.zero)
            {
                Invoke("PauseAnimators", 0.1f);
            }
            else
            {
                UnPauseAnimators();
            }

            animator.animationState = Utility.AnimationState.Climbing;
        }
        #endregion

        PlayAnimation();
    }

    private bool CanCast()
    {
        //asks player spell controller if casting is possible by checking what spell conditions are currently equipped/active and cooldown lists
        //return PlayerSpellControl.Instance.CanCast(controller.characterState);
        return true;
    }

    private ActionState SpellActionState()
    {
        return PlayerSpellControl.Instance.RetrieveActionState();
    }

    private void UpdateHeightReached()
    {
        float currentHeight = transform.position.y - initialHeight;

        if (currentHeight > heightReached)
        {
            heightReached = currentHeight;
        }
    }

    public void AdjustCharacterPhysics(float maxJumpModifier, float minJumpModifier, float moveSpeedModifier, float climbSpeedModifier)
    {
        maxJumpDistance = Mathf.RoundToInt((1 + maxJumpModifier) * maxJumpDistance);
        minJumpDistance = Mathf.RoundToInt((1 + minJumpModifier) * minJumpDistance);
        moveSpeed *= moveSpeedModifier;
        climbSpeed *= climbSpeedModifier;

        jumpVelocity = Mathf.Sqrt(-2 * gravity * maxJumpDistance);
        //minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpDistance);
    }

    private void ResetCharacterPhysics ()
    {
        maxJumpDistance = initialMaxJumpDistance;
        minJumpDistance = initialMinJumpDistance;
        moveSpeed = InitialMoveSpeed;
        climbSpeed = InitialClimbSpeed;

        //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //jumpVelocity = (maxJumpDistance / timeToJumpApex) / 32;
        jumpVelocity = Mathf.Sqrt(-2 * gravity * maxJumpDistance);
        //minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpDistance);
    }

    private void ExecuteSpellPoperties()
    {
        cooldownList.Add(GameControl.Instance.selectedSpellId, PlayerSpellControl.Instance.spells[GameControl.Instance.selectedSpellId].cooldown);
        animator.PlaySpellAnimation(GameControl.Instance.selectedSpellId);

        //Blast
        if (GameControl.Instance.selectedSpellId == 0)
        {
            //move projectile instantiation from player animation controller.
        }

        //Aura
        else if (GameControl.Instance.selectedSpellId == 1)
        {

        }

        //Poof
        else if (GameControl.Instance.selectedSpellId == 2)
        {
            PoofSpell();
        }
    }

    private void PoofSpell()
    {
        Vector2 input = InputController.Instance.CollectPlayerDirectionalInput();
        float closestDistance = 161;
        float rayLength = 160;
        Vector2 bottomLeft = new Vector2(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.min.y + .5f);
        Vector2 bottomRight = new Vector2(GetComponent<Collider2D>().bounds.max.x, GetComponent<Collider2D>().bounds.min.y + .5f);
        Vector2 topLeft = new Vector2(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.max.y - .5f);
        float horizontalRaySpacing = (GetComponent<Collider2D>().bounds.size.y - 1) / 3;
        float verticalRaySpacing = (GetComponent<Collider2D>().bounds.size.x - 1) / 3;

        if (input.y != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 rayOrigin = (input.y == -1) ? bottomLeft : topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i + input.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * input.y, rayLength, 1 << 9);

                if (hit)
                {
                    if (hit.distance < closestDistance)
                    {
                        closestDistance = hit.distance;
                    }
                }
            }

            float newPositionY;

            if (input.y == 1)
            {
                newPositionY = transform.position.y + closestDistance;
            }
            else
            {
                newPositionY = transform.position.y - closestDistance;
            }

            Vector2 poofDestination = new Vector2(transform.position.x, newPositionY);
            transform.position = poofDestination;
            UpdateHeightReached();
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 rayOrigin = (faceDirection == -1) ? bottomLeft : bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * faceDirection, rayLength, controller.collisionMask);

                if (hit)
                {
                    // this section is for moving through the side of platforms that can be fallen through.
                     if (hit.collider.tag == "Through")
                        {
                            if (input.y != 0)
                            {
                                continue;
                            }
                        }

                    //this section is for moving through the side of platforms that can't be fallen through.
                    if (hit.collider.tag == "3Sides")
                    {
                        if (input.y != 0)
                        {
                            continue;
                        }
                    }

                    if (hit.distance == 0)
                    {
                        continue;
                    }

                    if (hit.distance < closestDistance)
                    {
                        closestDistance = hit.distance;
                    }
                }
            }

            float newPositionX;

            if (faceDirection == 1)
            {
                newPositionX = transform.position.x + closestDistance;
            }
            else
            {
                newPositionX = transform.position.x - closestDistance;
            }

            Vector2 poofDestination = new Vector2(newPositionX, transform.position.y);
            transform.position = poofDestination;
        }
    }

    private void PlayAnimation ()
    {
        string state = animator.animationState.ToString();
        animator.PlayAnimation(state, faceDirection);
    }

    public void Die()
    {
        ReceiveReactionInput(ReactionState.Dying);
        GameControl.Instance.ProcessEndOfTowerCurrency(heightReached);
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
            //Summiting
            ReceiveReactionInput(ReactionState.Summiting);
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
            GameControl.Instance.player_currentHP = 0;
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

    public void PauseAnimators()
    {
        animator.enabled = false;
        animator.spellAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.scarAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.castingAnimator.gameObject.GetComponent<Animator>().enabled = false;
    }

    public void UnPauseAnimators()
    {
        animator.enabled = true;
        animator.spellAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.scarAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.castingAnimator.gameObject.GetComponent<Animator>().enabled = true;
    }

    public void UpdateCoolDownList()
    {
        for (int i = 0; i < cooldownList.Count; i++)
        {
            var key = cooldownList.ElementAt(i);
            int itemKey = key.Key;
            if (cooldownList[itemKey] <= 0)
            {
                cooldownList.Remove(itemKey);
            }
            else
            {
                cooldownList[itemKey] -= Time.deltaTime;
            }
        }
    }
}