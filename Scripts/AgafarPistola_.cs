using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AgafarPistola_ : MonoBehaviour {


    public AudioClip clip;

    private AudioSource audios;


    private bool playing = false;

    private void Awake()
    {
        this.audios = GetComponent<AudioSource>();

        if(Application.loadedLevelName.Equals("level_6"))
        {
            return;
        }

        if (PlayerPrefs.GetString("PistolaAgafada").Equals("Si"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Desactivar()
    {
        playing = false;
        gameObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (playing)
            {
                return;
            }
            if (Application.loadedLevelName.Equals("level_3_casa"))
            {
                PlayerPrefs.SetString("PistolaAgafada", "Si");
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().Permissions.ShootEnabled = true;
            }
            else if (Application.loadedLevelName.Equals("level_6"))
            {
                if (gameObject.name.Equals("Targeta"))
                {
                    PlayerPrefs.SetString("Targeta", "Si");
                }

            }

            //Reproduir so
            audios.PlayOneShot(clip, 0.4f);
            playing = true;
            //Desactivar gameObject

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Invoke("Desactivar",1);
        }
    }
}
