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
        Null,
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
        Null,
        Company_Logo,
        Player_Death
    }

    public enum MusicTracks
    {
        Null,
        Level_Zero_Normal,
        Level_One_Normal,
        Player_Death
    }

    public enum Levels
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
}