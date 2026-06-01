using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteBlue, spriteRed;

    [SerializeField] private Transform parent;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        if (animator == null)
        {
            //animator = GetComponent<Animator>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (playerData == null)
        {
            playerData = parent.GetComponent<PlayerData>();
        }
    }

    void Update()
    {
        SwitchSpriteForTeam(playerData.MyTeam);
    }

    void SwitchSpriteForTeam(Team team)
    {
        Vector3 forward = parent.forward;

        Sprite[] selectedSprites = team == Team.Blue ? spriteBlue : spriteRed;
        if (selectedSprites.Length > 0)
        {
            if (Mathf.Abs(forward.z) > Mathf.Abs(forward.x))
            {
                spriteRenderer.sprite = forward.z < 0 ? selectedSprites[0] : selectedSprites[1];
            }
            else
            {
                spriteRenderer.sprite = forward.x > 0 ? selectedSprites[2] : selectedSprites[3];
            }

        }
    }


}
