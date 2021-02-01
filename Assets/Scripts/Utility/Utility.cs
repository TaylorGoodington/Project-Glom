public class Utility
{
    public enum GameLayer
    {
        levelBounds = 8,
        ground = 9,
        platforms = 10,
        enemy = 11,
        player = 12,
        ladder = 13,
        ladderTop = 14,
        hazzard = 15
    }

    public enum EnemyMindset
    {
        Standing,
        Patroling,
        Chasing,
        Investigating,
        Attack_Prep,
        Attacking,
        Attack_Recovery,
        Fleeing,
        Dying,
        Dead
    }

    public enum InputState
    {
        Null,
        None,
        Player_Character,
        Menus,
        Dialogue
    }

    public enum ButtonPress
    {
        None,
        Interact,
        Jump,
        Cast,
        Select,
        Advance
    }

    public enum SoundEffect
    {
        Null,
        Company_Logo,
        Player_Death
    }

    public enum MusicTrack
    {
        Null,
        Level_Zero_Normal,
        Level_One_Normal,
        Player_Death
    }

    public enum Level
    {
        Company_Logo,
        Level_Zero,
        Level_One
    }

    public enum OffensiveSpell
    {
        Blast
    }

    public enum OffensiveSpellVariant
    {
        None,
        Burst,
        Charge,
        Lance
    }

    public enum MovementState
    {
        Standing,
        Running,
        Falling,
        Climbing
    }

    public enum ActionState
    {
        None,
        Attacking,
        Jumping,
        Casting,
        Grabbing_Ladder
    }

    public enum ReactionState
    {
        None,
        Summiting,
        Flinching,
        KnockedBack,
        Dying
    }

    public enum AnimationState
    {
        Standing,
        StandCasting,
        Running,
        RunCasting,
        Jumping,
        AerialCasting,
        Climbing,
        Summiting,
        Attacking,
        Casting,
        AbilityMoving,
        Flinching,
        KnockedBack,
        Dying
    }
}