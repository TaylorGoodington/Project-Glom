using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    #region Variables
    //[Tooltip("This field is used to specify which layers block the attacking and abilities raycasts.")]
    //public LayerMask attackingLayer;

    public float heightReached = 0;
    private float initialHeight;

    //velocity is in reference to being able to jump 16 pixels with min and 16 more with max
    [SerializeField] private int initialMinJumpVelocity = 122;
    private int minJumpVelocity;
    private float maxJumpHeight;
    private int jumpPixelDifferential = 18;

    [SerializeField] private float InitialMoveSpeed = 100;
    [HideInInspector] public float moveSpeed;

    [SerializeField] private float InitialClimbSpeed = 100;
    [HideInInspector] public float climbSpeed;

    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .1f;

    private bool isClimbable;
    private bool summiting;
    private bool casting;

    [HideInInspector] public int knockBackForce;
    [HideInInspector] public float climbingUpPosition;

    private PlayerAnimationController animator;
    private Controller2D controller;

    private float gravity = -10f;
    private float velocityXSmoothing;

    private Vector2 velocity;
    private Vector2 input;

    private int faceDirection = 1;
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

        if (input.x != 0)
        {
            faceDirection = (int)input.x;
        }

        if (controller.collisions.below)
        {
            if (controller.castingState == CastingState.Channel)
            {
                controller.movementState = MovementState.Standing;
            }
            else if (velocity.y > 1)
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
                controller.castingState = RetrieveCastingState();
                PlayerSpellControl.Instance.CastSpell();
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
        }

        //Standing
        else if (controller.movementState == MovementState.Standing)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y = gravity;
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
        }

        //Falling
        else if (controller.movementState == MovementState.Falling)
        {
            velocity.y += gravity;
            velocity.x = Mathf.SmoothDamp(velocity.x, input.x * moveSpeed, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
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

        //Casting States
        else if (controller.castingState == CastingState.Channel)
        {
            state = Utility.AnimationState.Channeling.ToString();
        }
        else if (controller.castingState == CastingState.Instant)
        {
            if (controller.movementState == MovementState.Standing)
            {
                state = Utility.AnimationState.StandCasting.ToString();
            }
            else if (controller.movementState == MovementState.Running)
            {
                state = Utility.AnimationState.RunCasting.ToString();
            }
            else
            {
                state = Utility.AnimationState.AerialCasting.ToString();
            }
        }

        //Movement States
        else
        {
            if (controller.movementState == MovementState.Falling)
            {
                state = MovementState.Jumping.ToString();
            }
            else
            {
                state = controller.movementState.ToString();
            }
        }

        animator.PlayAnimation(state, (int)input.x, controller.castingState);
    }

    private bool CanCast()
    {
        //check if we can cast based on the current reaction states, action states, and movement states
        if (controller.movementState == MovementState.Climbing)
        {
            return false;
        }

        if (controller.castingState == CastingState.None)
        {
            //condition for if states align with channeling
            //if (true)
            //{

            //}
            //else
            //{
                return PlayerSpellControl.Instance.CanCast();
            //}
        }

        if (controller.castingState == CastingState.Channel)
        {
            return true;
        }

        return false;
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
    
    public int FaceDirection()
    {
        return faceDirection;
    }

    public void CastComplete()
    {
        controller.castingState = CastingState.None;
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
            GameControl.Instance.PlayerHasDied();
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
        animator.backgroundEffectsAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.scarAnimator.gameObject.GetComponent<Animator>().enabled = false;
        animator.castingAnimator.gameObject.GetComponent<Animator>().enabled = false;
    }

    public void UnPauseAnimators()
    {
        animator.enabled = true;
        animator.backgroundEffectsAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.bodyAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.scarAnimator.gameObject.GetComponent<Animator>().enabled = true;
        animator.castingAnimator.gameObject.GetComponent<Animator>().enabled = true;
    }
}