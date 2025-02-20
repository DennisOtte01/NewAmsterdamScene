using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open {get; set;}
    private bool pointReached;
    [SerializeField]
    private Transform openPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open && !pointReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPoint.position, 0.1f);
            if (Mathf.Approximately(transform.position.y, openPoint.position.y))
            {
                pointReached = true;
            }
        }
    }

    public void Open()
    {
        open = true;
    }
}
