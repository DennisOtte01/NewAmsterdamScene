using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> zombies = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CenterOfGroup();
    }

    void CenterOfGroup()
    {
        float totalX = 0f;
        float totalY = 0f;
        foreach(var zombie in zombies)
        {
            totalX += zombie.transform.position.x;
            totalY += zombie.transform.position.y;
        }
        var centerX = totalX / zombies.Count;
        var centerY = totalY / zombies.Count;
        
        transform.position = new Vector3(centerX, centerY, 0f);
    }
}
