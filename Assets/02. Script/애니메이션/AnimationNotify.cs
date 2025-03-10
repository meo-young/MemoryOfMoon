using UnityEngine;

public enum PlayerType
{
    Player,
    NPC
}

public enum TerrainType
{
    Grass,
    Concrete,
    Water
}

public class AnimationNotify : MonoBehaviour
{
    [SerializeField] private PlayerType playerType; // NPC인지 플레이어인지 구분분
    private LayerMask terrainLayer; // 지형 레이어 마스크

    void Awake()
    {
        terrainLayer = LayerMask.GetMask("Terrain");
    }

    public void PlayFootstepSound()
    {
        // 현재 지형 타입 확인
        TerrainType currentTerrain = GetCurrentTerrainType();

        // 지형 타입에 따른 SFX 선택
        string sfxName = GetFootstepSoundName(currentTerrain);

        // SFX 재생
        if (System.Enum.TryParse<SFX>(sfxName, true, out SFX sfx))
        {
            SoundManager.instance.PlaySFX(sfx, transform.position);
        }
        else
        {
            Debug.LogWarning($"[AnimationNotify] '{sfxName}'은(는) 유효하지 않은 SFX 이름입니다.");
        }
    }

    private TerrainType GetCurrentTerrainType()
    {
        // 2D 레이캐스트 사용
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + Vector3.up, 
            Vector2.down, 
            2f, 
            terrainLayer
        );

        // 지형 타입 확인
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Grass"))
                return TerrainType.Grass;
            else if (hit.collider.CompareTag("Concrete"))
                return TerrainType.Concrete;
            else if (hit.collider.CompareTag("Water"))
                return TerrainType.Water;
        }
    
        // 기본값 반환
        return TerrainType.Concrete;
    }

    private string GetFootstepSoundName(TerrainType terrainType)
    {
        string prefix = playerType == PlayerType.Player ? "PLAYER" : "NPC";
        switch (terrainType)
        {
            case TerrainType.Grass:
                return $"{prefix}_GRASS_WALK";
            case TerrainType.Concrete:
                return $"{prefix}_CONCRETE_WALK";
            case TerrainType.Water:
                return $"{prefix}_GRASS_WALK";
            default:
                return $"{prefix}_GRASS_WALK";
        }
    }

    public void PlaySound(string sfxName)
    {
        if (System.Enum.TryParse<SFX>(sfxName, true, out SFX sfx))
        {
            SoundManager.instance.PlaySFX(sfx, transform.position);
        }
        else
        {
            Debug.LogWarning($"[AnimationNotify] '{sfxName}'은(는) 유효하지 않은 SFX 이름입니다.");
        }
    }
}