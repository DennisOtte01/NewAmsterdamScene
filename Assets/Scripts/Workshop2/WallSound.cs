using UnityEngine;

public class WallSound : MonoBehaviour
{
private AudioSource audioSource;

    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource =gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(sound01);
        }
        else if (other.gameObject.tag == "a")
        {
            audioSource.PlayOneShot(sound02);
        }
        else audioSource.PlayOneShot(sound03);
    }
}


