using UnityEngine;
using System.Collections;

public class WalkFix : MonoBehaviour
{
    public float tempsClimbing = 0.5f;
    public float tempsJumpGun = 0.5f;
    public float ForsaVerticalSword = -5;
    public float tempsEspasa = 1f;
    public float tempsEsperaShootStatic = 0.1f;
   
    public static float tempsAcabarAnimAGun = 0.1f;



    private Animator _animator;
    private CorgiController cc;
    private GameObject jugador;


    private static bool jaPlayejatClimbing = false;

    private bool firingCanviat = false;


    private void Awake()
    {
        this._animator = gameObject.GetComponent<Animator>();
        this.cc = gameObject.GetComponent<CorgiController>();

        //Questio d'optimitzacio
        if (Application.loadedLevelName.Equals("level_1"))
        {
            _animator.Play("shoot_static");
            _animator.Play("sword-melee");
        }
    }



    private void Update()
    {
        if (PlayerPrefs.GetString("PistolaAgafada").Equals("Si"))
        {
            if (CharacterShoot.cambiarAnimAGun)
            {
                FixSword();
                FixIdleGun();
                FixClimbingGun();
            }
            else
            {
                FixSword();
                FixIdle();
                FixClimbing();
            }
        }
        else
        {
            FixSword();
            FixIdle();
            FixClimbing();
        }
    }


    private void FixSword()
    {
            if (_animator.GetBool("MeleeAttacking"))
            {
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee"))
                {
                    _animator.Play("sword-melee");
                }
                
                else
                {
                    if (_animator.GetBool("Grounded"))
                    {
                        cc.AddVerticalForce(ForsaVerticalSword); //OK API CG
                    }

                    //ara fer poder saltar i espasa a la vegada -abans la anim ja no anava-
                }
                

            }
        
    }




    private void FixIdleGun()
    {
        if (_animator.GetBool("Grounded") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee"))
        {
            if (_animator.GetFloat("Speed") < 0.1f )
            {
                _animator.Play("shoot_static");

            }
            else
            {
                _animator.Play("shoot_run");
            }
        }

        if (_animator.GetBool("Grounded") == false && !_animator.GetCurrentAnimatorStateInfo(0).IsName("shoot_run") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee"))
        {
            _animator.Play("shoot_run");
        }

    }
    private void FixIdle()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            if (_animator.GetBool("Grounded") == true)
            {
                if (_animator.GetFloat("Speed") < 0.1f)
                {
                    _animator.Play("idle1LEG");
                }
                else
                {
                    _animator.Play("run");
                }

            }
                /*
            else
            {
                if (_animator.GetBool("MeleeAttacking"))
                {
                    _animator.Play("sword-melee");
                }
            }
                 * */
        }
        if (_animator.GetFloat("Speed") < 0.1f && _animator.GetBool("Grounded") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("shoot_static"))
        {
            _animator.Play("idle1LEG");
        }
    }

    private void FixClimbing()
    {
        if (_animator.GetBool("LadderClimbing") == true)
        {
            if (!jaPlayejatClimbing)
            {
                if (_animator.GetFloat("LadderClimbingSpeed") > 0.1f)
                {
                    jaPlayejatClimbing = true;
                    _animator.Play("ladderClimbing");
                    Invoke("jaPlayClimbing", tempsClimbing);
                }
                else
                {
                    _animator.Play("ladderClimbing_stop");
                }
            }
        }
        else
        {
            if (_animator.GetBool("Grounded") == false /*&& !_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee")*/)
            {
                if (_animator.GetBool("MeleeAttacking"))
                {
                    if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee"))
                    {
                        _animator.Play("sword-melee");
                    }
                }
                else
                {
                    _animator.Play("jump");    
                }
            }
        }
    }

    private void FixClimbingGun()
    {
        if (_animator.GetBool("LadderClimbing") == true)
        {
            if (!jaPlayejatClimbing)
            {
                if (_animator.GetFloat("LadderClimbingSpeed") > 0.1f /*&& !_animator.GetCurrentAnimatorStateInfo(0).IsName("sword-melee")*/)
                {
                    jaPlayejatClimbing = true;
                    _animator.Play("ladderClimbing");
                    Invoke("jaPlayClimbing", tempsClimbing);
                }
                else
                {
                    _animator.Play("ladderClimbing_stop");
                }
            }
        }
    }

    private void jaPlayClimbing()
    {
        jaPlayejatClimbing = false;
    }

}