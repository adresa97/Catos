using UnityEngine;

public class Vestidor : MonoBehaviour
{
    [Header("Sprites de sombreros")]
    public Sprite[] cap;

    [Header("Sprites de camisetas")]
    public Sprite[] tshirt;

    public Sprite GetCapByIndex(int index)
    {
        return cap[index];
    }

    public Sprite GetShirtByIndex(int index)
    {
        return tshirt[index];
    }
}
