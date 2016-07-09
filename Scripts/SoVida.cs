using UnityEngine;
using System.Collections;

public class SoVida : MonoBehaviour {

    public AudioClip clip;

    private AudioSource source;

    private void Awake()
    {
        this.source = GetComponent<AudioSource>();
    }

    private void des()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            source.PlayOneShot(clip, 0.4f);
            Destroy(GetComponent<BoxCollider2D>());
            Invoke("des", 1);
        }
    }
}
