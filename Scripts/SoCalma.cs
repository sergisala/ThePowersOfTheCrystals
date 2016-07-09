using UnityEngine;
using System.Collections;

public class SoCalma : MonoBehaviour {

    public AudioClip clip;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.PlayOneShot(clip, 0.4f);
    }

    public void goMenu()
    {
        Application.LoadLevel("menu");
    }
}
