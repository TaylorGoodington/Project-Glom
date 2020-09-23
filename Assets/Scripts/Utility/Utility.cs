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

    public enum GameState
    {
        Normal,
        First_Execution,
        Helmet_Info,
        Knight_Info
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

    public enum SoundEffects
    {
        Company_Logo
    }
}