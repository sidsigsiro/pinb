using UnityEngine;

public class ArrowKeyData
{
    public readonly KeyCode key;
    public readonly Vector2 dir;
    public int framePressed;

    public ArrowKeyData(KeyCode key, Vector2 dir)
    {
        this.key = key;
        this.dir = dir;
        framePressed = 0;
    }
}
