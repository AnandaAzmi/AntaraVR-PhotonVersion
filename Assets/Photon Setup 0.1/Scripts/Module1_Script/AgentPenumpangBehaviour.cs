using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AgentPenumpangBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    [Tooltip("Kecepatan rotasi saat karakter berjalan.")]
    public float rotationSpeed = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  // kita atur rotasi manual
        agent.updateUpAxis = true;
        AssignDestination();
    }
    void Update()
    {
        // Jika agent sedang bergerak
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            // Ambil arah dari velocity agent dan normalisasi
            Vector3 direction = agent.velocity.normalized;

            // Rotasikan hanya pada sumbu Y agar tidak nunduk/naik-turun
            Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
            if (flatDirection.sqrMagnitude > 0.001f)
            {
                // Menentukan rotasi berdasarkan arah gerak NPC
                Quaternion lookRotation = Quaternion.LookRotation(flatDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
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
