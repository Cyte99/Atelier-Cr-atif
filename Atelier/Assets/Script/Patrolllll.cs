using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolllll : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Chevalier;
    [SerializeField] LayerMask layerMask;
    [SerializeField] public List<Transform> patrolPoints;

    void Start()
    {
        StartCoroutine(Patrollll());
        StartCoroutine(ChevalierDetection());
    }

    IEnumerator Patrollll()
    {
        while (true)
        {
            for (int i = 0; i < patrolPoints.Count; i++)
            {
                yield return Goto(patrolPoints[i].position);
            }
        }
    }

    IEnumerator Goto(Vector3 destination)
    {
        agent.SetDestination(destination);

        while (Vector3.Distance(transform.position, destination) > 1f)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(2f);
    }

    IEnumerator ChevalierDetection()
    {
        float viewDistance = 10f;
        float halfAngle = 15f;
        while (true)
        {
            if (Physics.Raycast(transform.position, (Chevalier.position - transform.position), out RaycastHit hit, 10f, layerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
                {
                    print("Chevalier detected");
                }
                else
                {
                    print("Wall detected");
                }
            }
            else
            {
                print("Nothing detected");
            }
            yield return null;

        }
    }
}
