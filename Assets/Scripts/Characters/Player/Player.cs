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

    //velocity is in reference to being able to jump 16 pixels with min and 16 more with max
    public int initialMinJumpVelocity = 122;
    public int minJumpVelocity;
    private float maxJumpHeight;
    private int jumpPixelDifferential = 18;

    public float InitialMoveSpeed = 100;
    [HideInInspector] public float moveSpeed;

    public float InitialClimbSpeed;
    [HideInInspector] public float climbSpeed;
    
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
    private float velocityXSmoothing;

    public Vector2 velocity;
    private Vector2 input;

    public Dictionary<int, float> cooldownList;
    #endregion

    void Start()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<PlayerAnimationController>();
        casting = false;
        initialHeight = transform.position.y;

        ResetCharacterPhysics();
    }

    private void FixedUpdate()
    {
        DetermineStateResult();
        controller.Move(velocity * Time.fixedDeltaTime, input);
    }

    public void AdjustCharacterPhysics(float minJumpModifier, float moveSpeedModifier, float climbSpeedModifier)
    {
        minJumpVelocity = Mathf.RoundToInt(minJumpVelocity * minJumpModifier);
        moveSpeed *= moveSpeedModifier;
        climbSpeed *= climbSpeedModifier;
    }

    private void ResetCharacterPhysics()
    {
        minJumpVelocity = initialMinJumpVelocity;
        moveSpeed = InitialMoveSpeed;
        climbSpeed = InitialClimbSpeed;
    }

    public void ReceiveAndProcessMovementInput(Vector2 input)
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
            if (velocity.y > 1)
            {
                controller.movementState = MovementState.Jumping;
            }
            else
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
        }
        else
        {
            if (velocity.y < 0 && controller.movementState != MovementState.Climbing)
            {
                controller.movementState = MovementState.Falling;
            }
        }
    }

    public void ReceiveAndProcessButtonInput(ButtonPress buttonPress)
    {
        //Casting
        if (buttonPress == ButtonPress.Cast)
        {
            if (CanCast())
            {
                PlayerSpellControl.Instance.CastSpell();
                controller.castingState = RetrieveCastingState();
            }
        }

        //Jumping
        else if (buttonPress == ButtonPress.Jump_Start)
        {
            if (controller.movementState != MovementState.Falling)
            {
                velocity.y = minJumpVelocity;
                maxJumpHeight = transform.position.y + jumpPixelDifferential;
            }
        }
        else if (buttonPress == ButtonPress.Jump_End && controller.movementState == MovementState.Jumping)
        {
            controller.movementState = MovementState.Falling;
        }

        //Interacting
        else if (buttonPress == ButtonPress.Interact)
        {
            if (isClimbable && controller.movementState != MovementState.Climbing)
            {
                controller.movementState = MovementState.Climbing;
            }
        }
    }

    public void ReceiveAndProcessReactionInput(ReactionState reactionState)
    {
        if (reactionState == ReactionState.Summiting && controller.movementState == MovementState.Climbing)
        {
            climbingUpPosition = transform.position.y + 10;
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
    }

    private void DetermineStateResult()
    {
        CancelInvoke("PauseAnimators");
        UnPauseAnimators();

        if (controller.reactionState != ReactionState.None)
        {
            velocity = Vector2.zero;
        }

        if (controller.castingState == CastingState.Channel)
        {
            velocity = Vector2.zero;
        }

        //Running
        if (controller.movementState == MovementState.Running)
        {
            velocity.y = 0;
            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            //animator.animationState = Utility.AnimationState.Running;
        }

        //Standing
        else if (controller.movementState == MovementState.Standing)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y = gravity;
            //animator.animationState = Utility.AnimationState.Standing;
        }

        //Jumping
        else if (controller.movementState == MovementState.Jumping)
        {
            //once the player has gone up the difference in pixels stop countering gravity.
            if (transform.position.y < maxJumpHeight)
            {
                velocity.y -= gravity;
            }

            if (controller.collisions.above)
            {
                velocity.y = 0;
            }

            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            UpdateHeightReached();
            //animator.animationState = Utility.AnimationState.Jumping;
        }

        //Falling
        else if (controller.movementState == MovementState.Falling)
        {
            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            //animator.animationState = Utility.AnimationState.Jumping;
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

            //animator.animationState = Utility.AnimationState.Climbing;
        }

        PlayAnimation();
    }
    private void PlayAnimation()
    {
        string state;

        //Reaction States
        if (controller.reactionState != ReactionState.None)
        {
            state = controller.reactionState.ToString();
        }

        else if (controller.castingState == CastingState.Channel)
        {
            state = Utility.AnimationState.Channeling.ToString();
        }
        else
        {
            state = controller.movementState.ToString();
        }

        animator.PlayAnimation(state, faceDirection);
    }

    private bool CanCast()
    {
        //check if we can cast based on the current reaction states, action states, and movement states

        //asks player spell controller if casting is possible by checking what spell conditions are currently equipped/active and cooldown lists

        return PlayerSpellControl.Instance.CanCast();
    }

    private CastingState RetrieveCastingState()
    {
        return PlayerSpellControl.Instance.RetrieveCastingState();
    }

    private void UpdateHeightReached()
    {
        float currentHeight = transform.position.y - initialHeight;

        if (currentHeight > heightReached)
        {
            heightReached = currentHeight;
        }
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
    
    public void Die()
    {
        ReceiveAndProcessReactionInput(ReactionState.Dying);
        GameControl.Instance.ProcessEndOfTowerCurrency(heightReached);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 13)
        {
            isClimbable = true;
        }

        if (collider.gameObject.layer == 14)
        {
            //Summiting
            ReceiveAndProcessReactionInput(ReactionState.Summiting);
        }

        //Falling off the world
        if (collider.gameObject.layer == 19)
        {
            GameControl.Instance.player_currentHP = 0;
        }
    }

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