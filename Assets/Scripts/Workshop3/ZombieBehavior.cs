using UnityEngine;

public class zombiemovement : MonoBehaviour
{
    private string state; // GROUPING, HUNTING and PATROLLING
    private UnityEngine.AI.NavMeshAgent agent;

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
        float bobbing = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y + bobbing + 0.25f, transform.position.z);
        
        DetectPlayer();

        // switch (state)
        // {
        //     case "HUNTING": HuntingDo();
        //         break;
        //     case "IDLE": IdleDo();
        //         break;
        //     case "GROUPING": GroupingDo();
        //         break;
        // }
        if (state == "HUNTING")
        {
            HuntingDo();
        }
        
    }
    
    public void HuntingEntry()
    {
        Debug.Log("hunting");
        agent.speed = huntingSpeed;
    }
    
    public void HuntingExit()
    {
        Debug.Log("stopped hunting");
    }
    
    void HuntingDo() {
        agent.destination = player.position;
    }

    void IdleEntry()
    {
        agent.speed = patrolSpeed;
    }

    void IdleDo()
    {
        
    }

    void IdleExit()
    {
        
    }

    void GroupingEntry()
    {
        
    }

    void GroupingDo()
    {
        
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

            if(state == "HUNTING")
            {
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
        }
    }
}
