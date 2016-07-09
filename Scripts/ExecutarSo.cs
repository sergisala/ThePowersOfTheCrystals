using UnityEngine;
using System.Collections;

public class ExecutarSo : MonoBehaviour {

    private bool dintre = false;

    private AudioSource audio;


    public AudioClip clip;

    private void Awake()
    {
        this.audio = gameObject.GetComponent<AudioSource>();
    }

	  private void OnTriggerEnter2D(Collider2D other)
      {
          if (!dintre)
          {
              audio.PlayOneShot(clip, 0.4f);
              dintre = true;
          }
      }
      private void OnTriggerExit2D(Collider2D other)
      {
          dintre = false;
      }

}
