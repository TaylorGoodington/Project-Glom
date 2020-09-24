public class Utility
{
    public enum GameLayers
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

    public enum GameStates
    {
        Normal,
        First_Execution,
        Helmet_Info,
        Knight_Info
    }

    public enum EnemyMindsets
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

    public enum InputStates
    {
        None,
        Player_Character,
        Menus,
        Dialogue
    }

    public enum ButtonPresses
    {
        None,
        Interact,
        Jump,
        Cast,
        Select,
        Advance
    }

    public enum SoundEffects
    {
        Company_Logo
    }

    public enum MusicTracks
    {
        Level_Zero_Normal
    }

    public enum EventTypes
    {
        Cinematic,
        Dialogue,
        Tutorial
    }
}