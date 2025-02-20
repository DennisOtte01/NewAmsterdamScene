using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] 
    private GameObject door;

    private Door _door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _door = door.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        _door.Open();
    }
}
