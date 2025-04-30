using System.Collections.Generic;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    public static List<ChairSpot> chairs = new List<ChairSpot>();
    public static List<StandingSpot> standings = new List<StandingSpot>();

    void Awake()
    {
        chairs.Clear(); // Bersihkan dulu kalau scene di-reload
        standings.Clear();

        chairs.AddRange(FindObjectsOfType<ChairSpot>());
        standings.AddRange(FindObjectsOfType<StandingSpot>());
    }
}
