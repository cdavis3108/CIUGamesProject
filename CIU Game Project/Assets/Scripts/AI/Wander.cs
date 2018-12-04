using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour {

    public Vector3 target;
    public float waitTimer = 5f;
    private float timer;
    private bool waiting = true;
    public float closeEnough = 2f;
    public float range = 5f;
    NavMeshAgent agent;
    

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
        timer = Random.Range(0,6);
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target);

        if (distanceToTarget <= closeEnough && !waiting)
        {
            waiting = true;
            timer = 0;
        }

        if(waiting && timer >= waitTimer)
        {
            waiting = false;
            SelectLocation();
        }
	}

    void SelectLocation()
    {
        bool lookingForTarget = true;
        float loops = 0;
        while (lookingForTarget)
        {
            Vector3 newTarget = Random.insideUnitSphere * range;
            newTarget += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newTarget, out hit, range, 1))
            {
                target = hit.position;
                lookingForTarget = false;
            }
            ++loops;
            if (loops > 5)
            {
                Debug.Log("Cannot find location");
                lookingForTarget = false;
            }
        }
        agent.SetDestination(target);
        Debug.DrawLine(transform.position, target);
    }
}
