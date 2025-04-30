using UnityEngine;

public class ChairSpot : MonoBehaviour
{
    public bool IsOccupied { get; private set; } = false;

    public bool TryOccupy()
    {
        if (IsOccupied) return false;
        IsOccupied = true;
        return true;
    }
}
