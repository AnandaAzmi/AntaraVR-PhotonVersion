using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AgentPenumpangBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AssignDestination();
    }

    void AssignDestination()
    {
        // Ambil kursi kosong dari SpotManager
        var emptyChairs = SpotManager.chairs.Where(c => !c.IsOccupied).ToList();
        if (emptyChairs.Count > 0)
        {
            var target = emptyChairs[Random.Range(0, emptyChairs.Count)];
            if (target.TryOccupy())
            {
                agent.SetDestination(target.transform.position);
                return;
            }
        }

        // Jika kursi habis, cari titik berdiri
        var emptyStandingSpots = SpotManager.standings.Where(s => !s.IsOccupied).ToList();
        if (emptyStandingSpots.Count > 0)
        {
            var target = emptyStandingSpots[Random.Range(0, emptyStandingSpots.Count)];
            if (target.TryOccupy())
            {
                agent.SetDestination(target.transform.position);
                return;
            }
        }

        // Kalau dua-duanya penuh, diam di tempat
    }
}
