using UnityEngine;

public class SelectArrowFloat : MonoBehaviour
{
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos +
            new Vector3(0, Mathf.Sin(Time.time * 5f) * 0.2f, 0);
    }
}
