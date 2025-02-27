using System;
using UnityEngine;

public class zombiemovement : MonoBehaviour
{
    private string state = "IDLE"; // GROUPING, HUNTING and PATROLLING
    private UnityEngine.AI.NavMeshAgent agent;
    
    [SerializeField] 
    private GameObject groupManager;
    private Transform groupCenter;
    [SerializeField] 
    private float groupDistance = 10f;
    
    [SerializeField] 
    private Transform player;
    [SerializeField]
    private float patrolSpeed = 1.5f;
    [SerializeField]
    private float huntingSpeed = 2f;
    [SerializeField]
    private float bobbingSpeed = 2f;
    [SerializeField]
    private float bobbingHeight = 5f;
    
    [SerializeField]
    private float huntingTime;
    private float currentHuntingTime;
    
    [SerializeField]
    private float detectionRange = 10f;
    [SerializeField]
    private float fieldOfView = 45f;
    [SerializeField]
    private LayerMask obstacleMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        agent.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        groupCenter = groupManager.GetComponent<Transform>();
        
        DetectPlayer();

        switch (state)
        {
            case "HUNTING": HuntingDo();
                break;
            case "IDLE": IdleDo();
                break;
            case "GROUPING": GroupingDo();
                break;
        }
        
    }
    
    public void HuntingEntry()
    {
        agent.speed = huntingSpeed;
        currentHuntingTime = 0;
    }
    
    public void HuntingExit()
    {
    }
    
    void HuntingDo() {
        agent.destination = player.position;
        if (Vector3.Distance(transform.position, groupCenter.position) > groupDistance)
        {
            HuntingExit();
            GroupingEntry();
            state = "GROUPING";
        }
        
        if(huntingTime > currentHuntingTime)
        {
            currentHuntingTime += Time.deltaTime;
        } else
        {
            HuntingExit();
            IdleEntry();
            state = "IDLE";
        }
    }

    void IdleEntry()
    {
        agent.speed = patrolSpeed;
    }

    void IdleDo()
    {
        float bobbing = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y + bobbing + 0.25f, transform.position.z);
    }

    void IdleExit()
    {
        
    }

    void GroupingEntry()
    {
        agent.speed = patrolSpeed;
    }

    void GroupingDo()
    {
        agent.destination = groupCenter.position;
        if (Vector3.Distance(transform.position, groupCenter.position) < groupDistance)
        {
            GroupingExit();
            IdleEntry();
            state = "IDLE";
        }
    }

    void GroupingExit()
    {
        
    }
    
    void DetectPlayer(){
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= fieldOfView && state != "HUNTING" && state != "GROUPING")
            {
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.green);
                    HuntingEntry();
                    currentHuntingTime = 0;
                    state = "HUNTING";
                }
                else
                {
                    Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.yellow);
            }
        }
    }

    private void OnCollisionEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
