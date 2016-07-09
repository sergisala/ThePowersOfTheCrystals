using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class ButtonSeguretat : MonoBehaviour {

    public Sprite noiaDespertaSprite;
    public Sprite botoVerd;

    public AudioClip so1;
    public AudioClip so2;
    public AudioClip so3;
    public AudioClip so4;

    public GameObject zonag;


    private AudioSource audio;

    public static bool actuat = false;

    private bool jugadorDintre = false;

    private CharacterBehavior beq;
    private CorgiController cc;

    private string currentLevelName;

    private GameObject rectang;

    private CameraController _sceneCamera;
    private GameObject noia;
    private GameObject musica_level;

    private void Awake()
    {
        zonag = GameObject.Find("zonadialogo");
        audio = gameObject.GetComponent<AudioSource>();
        beq = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>();
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CorgiController>();
        _sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        currentLevelName = Application.loadedLevelName;

        
        if(currentLevelName.Equals("level_7"))
        {
            zonag.SetActive(false);
            musica_level = GameObject.Find("BgMusicUniformMotionVictory");

            rectang = GameObject.Find("RECT");
            noia = GameObject.Find("Noia");

            rectang.SetActive(false);
        }
    }


    //Metodes cridats en temps determinats en els Invoke (son efectes i sons)
    //************************************************************************************************************************
    private void terratremol2()
    {
        _sceneCamera.Shake(new Vector3(0.05f, 4f, 1.2f));
        audio.PlayOneShot(so1, 0.3f);
    }

    private void terratremol3()
    {
        _sceneCamera.Shake(new Vector3(0.05f, 4f, 1.2f));
        audio.PlayOneShot(so1, 0.3f);
    }

    private void rectangAp()
    {
        rectang.SetActive(true);
        rectang.transform.position = new Vector3(-147.3f, -571.9f, 0f);

        audio.PlayOneShot(so3, 0.6f);
    }

    private void colorTrans1()
    {
        rectang.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        noia.transform.position = GameObject.Find("posNoia").transform.position;
        noia.GetComponent<SpriteRenderer>().sprite = noiaDespertaSprite;

        noia.GetComponent<SpriteRenderer>().sortingLayerName = "Platforms";

        audio.PlayOneShot(so2, 0.6f);
    }

    private void colorTrans2()
    {
        rectang.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        audio.PlayOneShot(so4, 0.4f);
    }

    private void iniciarDialeg()
    {
        musica_level.GetComponent<BackgroundMusic>()._source.Play();

        actuat = true;
        zonag.SetActive(true);
        

    }
   

	private void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            if (jugadorDintre && !actuat)
            {
                if (CharacterBehavior._isFacingRight)
                {
                    FlipCharacter(GameObject.FindGameObjectWithTag("Player"));
                }
                //Posar el sprite del boto activat al boto
                gameObject.GetComponent<SpriteRenderer>().sprite = botoVerd;

                //Pausar la musica de fons

                musica_level.GetComponent<BackgroundMusic>()._source.Pause();

              

                //Anular el lliure moviment del personatge
                //************************************************************************************************************************
                beq.BehaviorParameters.MovementSpeed = 0;
                beq.BehaviorParameters.RunSpeed = 0;
                beq.BehaviorParameters.WalkSpeed = 0;
                
                //velocitat = 0
                cc.set_speed(new Vector2(0,0));

                beq.BehaviorParameters.JumpRestrictions = CharacterBehaviorParameters.JumpBehavior.CantJump; //treure amb canJumpAnywhere
                beq.BehaviorState.CanMoveFreely = false;
                beq.BehaviorState.CanJump = false;
                beq.BehaviorState.CanMelee = false;
                beq.BehaviorState.CanShoot = false;
                beq.BehaviorState.CanDash = false; //* innecesari al siempre estar fals, pero necesari per debug



                //Cridar metodes per fer el terratremol, sons, efectes,...
                //**********************************************************************************************************************

                //fer el primer terratremol directament
                _sceneCamera.Shake(new Vector3(0.05f, 4f, 1.2f));
                audio.PlayOneShot(so1, 0.3f);


                Invoke("terratremol2", 5f);
                Invoke("terratremol3", 10f);
                Invoke("rectangAp", 12f);
                Invoke("colorTrans1", 16f);
                Invoke("colorTrans2", 17f);
                Invoke("iniciarDialeg", 21); //Dialeg entre noi i noia...




                //Acabar l'escena
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Mirar si el que ha entrat en trigger es el jugador
        if (col.GetComponent<CharacterBehavior>() == null)
            return;
        jugadorDintre = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //Mirar si el que ha entrat en trigger es el jugador
        if (col.GetComponent<CharacterBehavior>() == null)
            return;
        jugadorDintre = false;
    }

    private void FlipCharacter(GameObject g)
    {
        //Girem el jugador horitzontalment
        g.transform.localScale = new Vector3(-g.transform.localScale.x, g.transform.localScale.y, g.transform.localScale.z);
        CharacterBehavior._isFacingRight = g.transform.localScale.x > 0;

        //?
    }
}
