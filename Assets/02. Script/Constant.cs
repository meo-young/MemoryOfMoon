using UnityEngine;

public static class Constant
{
    [Header("# 페이드 관련 수치")]
    public static float     FADE_SCREEN_DURATION = 1f;          // 전체화면 페이드 시간
    public static float     FADE_SCREEN_AFTER_DELAY = 1f;       // 전체화면 페이드 후 딜레이
    public static float     FADE_SPRITE_DURATION = 0.3f;        // 스프라이트 페이드 시간

    [Header("# 씬 관련 수치")]
    public static string    SCENE_TITLE = "Title";
    public static string    SCENE_MANUAL = "Manual";
    public static string    SCENE_PROLOG = "Prolog";
    public static string    SCENE_CHAPTER1 = "Mansion";
    public static string    SCENE_CHAPTER2 = "Chapter2";

    [Header("# 독백 관련 수치")]
    public static float     MONOLOGUE_TYPING_TIME = 0.05f;      // 독백 출력 시간

    [Header("# 오디오 관련 수치")]
    public static string    AUDIO_ADDRESS_LOCKED_DOOR = "Locked_Door";
    public static string    AUDIO_ADDRESS_OPEN_DOOR = "Open_Door";
    public static string    AUDIO_ADDRESS_CLOSE_DOOR = "Close_Door";
}
