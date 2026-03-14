using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitPool : MonoBehaviour
{
    public static FruitPool Instance;

    public GameObject fruitPrefab;
    public int poolSize = 100;

    private Queue<GameObject> pool = new Queue<GameObject>();
    public int totalScoreInLevel;

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab, transform);
            fruit.SetActive(false);
            pool.Enqueue(fruit);
        }
    }

    public GameObject Get(Vector3 position, FruitType type)
    {
        GameObject fruit = pool.Dequeue();
        fruit.transform.position = position;

        FruitCollect fc = fruit.GetComponent<FruitCollect>();
        fc.SetType(type);

        fruit.SetActive(true);

        if (FruitManager.Instance != null)
        {
            int score = fc.GetScoreValue();
            FruitManager.Instance.RegisterFruitScore(score);
        }

        pool.Enqueue(fruit);
        return fruit;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level"))
        {
            totalScoreInLevel = 0;
        }
    }
}
