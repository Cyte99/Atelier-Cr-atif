using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Patrolllll : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform Chevalier;

    [Header("Patrol")]
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float pointReachDistance = 1f;
    [SerializeField] private float waitAtPoint = 2f;

    [Header("Detection")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float halfAngle = 45f;
    [SerializeField] private float eyeHeight = 1.6f;
    [SerializeField] private float loseSightTime = 1.5f;   // time before returning to patrol

    private bool chasing = false;
    private float lastSeenTime = -999f;
    private int patrolIndex = 0;

    [Header("Game Over")]
    [SerializeField] private float killDistance = 1.5f;
    [SerializeField] private GameObject missionFailedPanel;
    private bool missionFailed = false;


    private void Awake()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(BrainLoop());
    }

    IEnumerator BrainLoop()
    {
        while (true)
        {
            bool seesPlayer = CanSeeChevalier();

            if (seesPlayer)
            {
                chasing = true;
                lastSeenTime = Time.time;
            }

            if (chasing)
            {
                if (Chevalier != null)
                {
                    agent.SetDestination(Chevalier.position);

                    float distance = Vector3.Distance(transform.position, Chevalier.position);
                    if (distance <= killDistance && !missionFailed)
                    {
                        TriggerMissionFailed();
                    }
                }

                if (Time.time - lastSeenTime > loseSightTime)
                {
                    chasing = false;
                }

                yield return null;
            }
            else
            {
                if (patrolPoints == null || patrolPoints.Count == 0)
                {
                    yield return null;
                    continue;
                }

                Vector3 dest = patrolPoints[patrolIndex].position;
                agent.SetDestination(dest);

                while (!chasing && Vector3.Distance(transform.position, dest) > pointReachDistance)
                {
                    if (CanSeeChevalier())
                    {
                        chasing = true;
                        lastSeenTime = Time.time;
                        break;
                    }
                    yield return null;
                }

                if (!chasing)
                {
                    yield return new WaitForSeconds(waitAtPoint);
                    patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
                }
            }
        }
    }

    private bool CanSeeChevalier()
    {
        if (Chevalier == null) return false;

        Vector3 origin = transform.position + Vector3.up * eyeHeight;
        Vector3 target = Chevalier.position + Vector3.up * 1.0f;
        Vector3 dir = (target - origin).normalized;

        // Forward based on movement direction
        Vector3 forward = agent.desiredVelocity.sqrMagnitude > 0.01f
            ? agent.desiredVelocity.normalized
            : transform.forward;

        // FOV cone check
        float angle = Vector3.Angle(forward, dir);
        if (angle > halfAngle) return false;

        // Debug rays 
        Debug.DrawRay(origin, forward * 2f, Color.cyan);
        Debug.DrawRay(origin, dir * viewDistance, Color.yellow);

        // Line of sight check
        if (Physics.Raycast(origin, dir, out RaycastHit hit, viewDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            return hit.transform == Chevalier || hit.transform.IsChildOf(Chevalier);
        }

        return false;
    }

    private void TriggerMissionFailed()
    {
        missionFailed = true;

        Debug.Log("MISSION FAILED");

        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(true);

        agent.isStopped = true;

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}

