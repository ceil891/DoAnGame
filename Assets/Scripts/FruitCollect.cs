using UnityEngine;

public class FruitCollect : MonoBehaviour
{
    public FruitType fruitType;

    [System.Serializable]
    public class FruitVisual
    {
        public FruitType type;
        public Sprite sprite;
        public GameObject collectEffect;
        public AudioClip sound;
        public RuntimeAnimatorController animator;
    }

    public FruitVisual[] visuals;

    private SpriteRenderer sr;
    private Animator anim;
    private bool collected;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        collected = false;
    }

    public void SetType(FruitType type)
    {
        fruitType = type;

        foreach (var v in visuals)
        {
            if (v.type == type)
            {
                sr.sprite = v.sprite;

                if (anim != null && v.animator != null)
                    anim.runtimeAnimatorController = v.animator;

                currentEffect = v.collectEffect;
                pickupSound = v.sound;
                return;
            }
        }
    }

    GameObject currentEffect;
    AudioClip pickupSound;

    public int GetScoreValue()
    {
        return fruitType switch
        {
            FruitType.Apple => 1,
            FruitType.Banana => 2,
            FruitType.Orange => 3,
            FruitType.Kiwi => 4,
            FruitType.Melon => 5,
            FruitType.Strawberry => 6,
            FruitType.Pineapple => 7,
            FruitType.Cherries => 8,
            _ => 0
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        FruitManager.Instance.AddScore(GetScoreValue());

        if (currentEffect != null)
            Instantiate(currentEffect, transform.position, Quaternion.identity);

        if (pickupSound != null && Camera.main != null)
            Camera.main.GetComponent<AudioSource>()?.PlayOneShot(pickupSound);

        gameObject.SetActive(false);
    }
}
