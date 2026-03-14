using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static float move;
    public static bool jump;

    public void LeftDown() { move = -1; }
    public void RightDown() { move = 1; }

    public void Release() { move = 0; }

    public void Jump()
    {
        jump = true;
    }
}
