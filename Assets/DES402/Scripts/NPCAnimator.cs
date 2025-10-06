using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private Sprite stand;
    [SerializeField] private Sprite idle;

    [SerializeField] private float swapInterval = 0.7f;

    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isIdle = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stand;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > swapInterval)
        {
            timer = 0f;
            isIdle = !isIdle;
            spriteRenderer.sprite = isIdle ? idle : stand;
        }
    }
}
