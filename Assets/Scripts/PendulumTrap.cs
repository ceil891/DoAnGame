using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    public float maxAngle = 35f;
    public float speed = 2f;

    private float startAngle;

    void Start()
    {
        startAngle = transform.localEulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * maxAngle;
        transform.localRotation = Quaternion.Euler(0, 0, startAngle - angle);
    }
}
