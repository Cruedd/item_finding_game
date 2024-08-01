using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public enum EnemyState
{
    Idle,
    OnPatrol,
    Chasing,
}
public class EnemyMovement2 : MonoBehaviour
{
    [Header("Enemy State")]
    [SerializeField] EnemyState currentState;

    [Header("Idle Variables")]
    [SerializeField] float minWaitTime, maxWaitTime;

    [Header("OnPatrol Variables")]
    [SerializeField] float walkingSpeed;


    [Header("Chasing Variables")]
    [SerializeField] float chasingSpeed;
    [SerializeField] float chasingDetectRange;
    [SerializeField] float chasingTime;
    [SerializeField] Transform player;

    [Header("Raycast Interact")]
    [SerializeField] Transform head;
    [SerializeField] float hitRange;
    RaycastHit hit;

    [Header("Destinations")]
    public List<Transform> destinations;

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator aiAnim;

    public FieldOfView fov;


    void Start()
    {
        
        StartCoroutine(RaycastWithDelayEnum(() =>
        {
            if (Physics.Raycast(head.position, head.forward, out hit, hitRange))
            {
                if (!hit.transform) return;
                DoorOpen doorOpen = hit.transform.GetComponent<DoorOpen>();
                if (doorOpen != null && !doorOpen.opened)
                {
                    doorOpen.Interact(null);
                }

            }
        }, 1f));


        if (agent == null)
        {
            Debug.LogError("NavMeshAgent 'ai' is not assigned!");
        }

        if (destinations == null || destinations.Count == 0)
        {
            Debug.LogError("Transform List 'destinations' is empty or not assigned!");
        }

        if (aiAnim == null)
        {
            Debug.LogError("Animator 'aiAnim' is not assigned!");
        }


      
    }

    void Update()
    {
        DetectPlayer();

        if(CurrentState() == EnemyState.Chasing)
        {
            OnChasing();
        }
        else if(CurrentState() == EnemyState.Idle)
        {
            OnIdle();
        }else if(CurrentState() == EnemyState.OnPatrol)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                ChangeState(EnemyState.Idle);
            }
        }
    

        aiAnim.SetFloat("Velocity", agent.velocity.magnitude);
    }

    #region Idle Functions
    void OnIdle()
    {
        if (idleCoroutine != null) return;
        idleCoroutine = StartCoroutine(IdleEnum());
    }

    Coroutine idleCoroutine;
    IEnumerator IdleEnum()
    {
        float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(randomWaitTime);

        SetDestination(GetRandomDestination(),walkingSpeed);
        ChangeState(EnemyState.OnPatrol);
        idleCoroutine = null;
    }

    #endregion

    #region Chasing Functions

    void OnChasing()
    {
        if (player == null) return;
        SetDestination(player.position,chasingSpeed);

        if(agent.remainingDistance <= chasingDetectRange)
        {
            Debug.Log("Player algýlandý!");
        }

        if(!HasPlayerOnTheRange() && chasingCoroutine == null)
        {
            chasingCoroutine = StartCoroutine(ChasingEnum());
        }
    }
    Coroutine chasingCoroutine;
    IEnumerator ChasingEnum()
    {
        yield return new WaitForSeconds(chasingTime);
        if (!HasPlayerOnTheRange())
        {
            ChangeState(EnemyState.OnPatrol);
        }
        chasingCoroutine = null;
    }
    void DetectPlayer()
    {
        if (HasPlayerOnTheRange() && CurrentState() != EnemyState.Chasing)
        {
            player = fov.visibleTargets[0];
            ChangeState(EnemyState.Chasing);
        }
    }
    #endregion


    #region State Functions
    void ChangeState(EnemyState state)
    {
        currentState = state;
    }

    EnemyState CurrentState()
    {
        return currentState;
    }
    #endregion

    #region Enum With Delay
    IEnumerator RaycastWithDelayEnum(Action action, float delay)
    {
        while (true)
        {
            action.Invoke();
            yield return new WaitForSeconds(delay);
        }
    }
    #endregion

    #region Agent Destination Functions

    void SetDestination(Vector3 pos, float speed)
    {
        agent.SetDestination(pos);
        agent.speed = speed;
    }

    Vector3 GetRandomDestination()
    {
        int randomDest = UnityEngine.Random.Range(0,destinations.Count);
        return destinations[randomDest].position;
    }
    #endregion

    bool HasPlayerOnTheRange()
    {
        return fov.visibleTargets.Count > 0;
    }

    Vector3 GetRandomPos(Vector3 startPos, float radius)
    {
        Vector3 dir = UnityEngine.Random.insideUnitSphere * radius;
        dir += startPos;
        NavMeshHit hit;
        Vector3 finalPos= Vector3.zero;
        if(NavMesh.SamplePosition(dir,out hit,radius, 1))
        {
            finalPos = startPos;
        }
        return finalPos;
    }
    void OnDrawGizmos()
    {

    }

}