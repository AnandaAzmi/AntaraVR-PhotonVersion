using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingSpot : MonoBehaviour
{
    public bool IsOccupied { get; private set; } = false;

    public bool TryOccupy()
    {
        if (IsOccupied) return false;
        IsOccupied = true;
        return true;
    }
}
