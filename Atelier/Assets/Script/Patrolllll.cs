using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Linq.Expressions;

public class Patrolllll : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    //[SerializeField] Transform Chevalier;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 PointA;
    [SerializeField] Vector3 PointB;
    [SerializeField] Vector3 PointC;
    [SerializeField] Vector3 PointD;

    void Start()
    {
        StartCoroutine(patrollll());
        //StartCoroutine(ChevalierDetection());
    }

    IEnumerator patrollll()
    {
        while (true)
        {
            yield return Goto(PointA);
            yield return Goto(PointB);
            yield return Goto(PointC);
            yield return Goto(PointD);
        }
    }

    IEnumerator Goto(Vector3 destination)
    {
        agent.SetDestination(destination);

        while(Vector3.Distance(transform.position, destination) > 1f)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(2f);
    }

    //IEnumerator ChevalierDetection()
    //{
    //    while (true)
    //    {
    //        if(Physics.Raycast(transform.position, (Chevalier.position - transform.position), out RaycastHit hit,10f,layerMask))
    //        {
    //            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
    //            {
    //                print("Chevalier detected");
    //            }
    //            else
    //            {
    //                print("Wall detected");
    //            }
    //        }
    //        else
    //        {
    //            print("Nothing detected");
    //        }
    //        yield return null;

    //    }
    //}
}
