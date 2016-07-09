using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;


public class DiapositivaIntro : MonoBehaviour {

    public string text1, text2, text3, text4, text5, text6, text7;

    public AudioClip audio1, audio2, audio3, audio4;
    public AudioClip soExtra, soExtra2;

    public Sprite spr;

    public float vol = 0.4f;

    private AudioSource source;
    private Text _text;

    private void Awake()
    {
        this.source = GetComponent<AudioSource>();
        this._text = GetComponent<Text>();
    }

    private void Start()
    {
        if (Application.loadedLevelName.Equals("intro"))
        {
            PlayerPrefs.DeleteAll();
            //borrar carpeta xmls i claus playerprefs
           
                System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    if(file.Name.Equals("level_1.xml") || file.Name.Equals("level_2.xml") || file.Name.Equals("level_3.xml") || file.Name.Equals("level_3_casa.xml") ||
                        file.Name.Equals("level_3_casa_secondroom.xml") || file.Name.Equals("level_4.xml") || file.Name.Equals("level_5.xml") || file.Name.Equals("level_6.xml") ||
                        file.Name.Equals("level_7.xml") || file.Name.Equals("level_8.xml") || file.Name.Equals("menu.xml") || file.Name.Equals("intro.xml") || file.Name.Equals("sewers.xml") ||
                        file.Name.Equals("mortUncle.xml") || file.Name.Equals("final.xml"))
                    {
                        file.Delete();
                    }
                    
                }


            


            source.PlayOneShot(audio1, vol);

            Invoke("canviText1_Intro", 5);
            Invoke("canviText2_Intro", 16 + 5);
            Invoke("canviText3_Intro", 16 + 16 + 5 - 3);
            Invoke("canviText4_Intro", 16 + 16 + 16 + 5 - 3);

            Invoke("iniciarJoc", 16 + 16 + 16 + 8);    
        }
        else if (Application.loadedLevelName.Equals("sewers"))
        {
            source.PlayOneShot(audio1, vol);
            source.PlayOneShot(soExtra, vol - 0.3f);
            Invoke("anarALevel5", 14);
        }
        else if(Application.loadedLevelName.Equals("mortUncle"))
        {
            //so extra - cop trencar paret per robot
            source.PlayOneShot(soExtra, vol);
            //invokes
            Invoke("canviText1_Final", 6);
            Invoke("canviText2_Final", 6+24);
            Invoke("canviText3_Final", 6+24+5);

            Invoke("canviText4_Final", 6+24+5+5);
            Invoke("canviText4_2Final", 6 + 24 + 5 + 5+7);
            Invoke("canviText4_3Final", 6 + 24 + 5 + 5+12);
            Invoke("canviText4_4Final", 6 + 24 + 5 + 5 + 33);
            Invoke("canviText4_5Final", 6 + 24 + 5 + 5 + 36);
            Invoke("canviText4_6Final", 6 + 24 + 5 + 5 + 41);


            //acabar joc
            Invoke("anarAFinal", 6 + 24 + 6 + 5 + 46);
            
        }
    }

    //Level_final
    //*************************************************************
    private void canviText1_Final()
    {
        source.PlayOneShot(audio1, vol);
    }

    private void canviText2_Final()
    {
        source.PlayOneShot(audio2, vol);
        _text.text = text1;
    
    }

    private void canviText3_Final()
    {
        source.PlayOneShot(audio3, vol);

    }


    private void canviText4_Final()
    {
        source.PlayOneShot(audio4, vol);
        _text.text = text2;

        GameObject.Find("Image").GetComponent<Image>().sprite = spr;
        source.PlayOneShot(soExtra2, vol - 0.3f);
    }

    private void canviText4_2Final()
    {
        _text.text = text3;
    }

    private void canviText4_3Final()
    {
        _text.text = text4;
    }

    private void canviText4_4Final()
    {
        _text.text = text5;
    }

    private void canviText4_5Final()
    {
        _text.text = text6;
    }

    private void canviText4_6Final()
    {
        _text.text = text7;
    }


    //Final
    private void anarAFinal()
    {
        source.Stop();
        Application.LoadLevel("final");
    }

    //Sewers
    private void anarALevel5()
    {
        source.Stop();
        Application.LoadLevel("level_5");
    }

    //Intro
    //*****************************************************************
    private void iniciarJoc()
    {
        source.Stop();


        
        Application.LoadLevel("level_1");


    }

    private void canviText1_Intro()
    {
        _text.text = text1;
        source.PlayOneShot(audio2, vol-0.2f);
    }

    private void canviText2_Intro()
    {
        _text.text = text2;
    }

    private void canviText3_Intro()
    {
        _text.text = text3;
        source.PlayOneShot(audio3, vol-0.25f);
        //So extra i canvi d imatge
        GameObject.Find("Image").GetComponent<Image>().sprite = spr;
        source.PlayOneShot(soExtra, vol-0.3f);
        
    }

    private void canviText4_Intro()
    {
        _text.text = text4;
        source.PlayOneShot(audio4, vol);
    }
}
