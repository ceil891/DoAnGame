using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public int characterIndex;
    public MenuManager menuManager;

    void OnMouseDown()
    {
        menuManager.SelectCharacter(characterIndex);
    }

    void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.15f;
    }

    void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }
}
