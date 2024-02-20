using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool isBlocked;

    public void SetBlocked(bool blocked)
    {
        isBlocked = blocked;
    }

    public bool IsBlocked()
    {
        return isBlocked;
    }
}
