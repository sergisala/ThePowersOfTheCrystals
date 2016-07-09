using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using UnitySampleAssets.CrossPlatformInput;

[XmlRoot("DoorLevel")]
public class DoorLevel : MonoBehaviour {

    public bool anar_;
	public string LevelName;
	public bool anarALevel = false;

	public float tempsEspera = 1F;
	public float soImpact = 1F;

	public AudioClip impact;


	private AudioSource audio;


	private bool jugadorDintre = false;
	private bool sonant = false;

	private string currentLevelName;

    private void OnApplicationQuit()
    {
        if (!Application.loadedLevelName.Equals("menu"))
        {
            PlayerPrefs.SetString("continuar", Application.loadedLevelName);
            PlayerPrefs.SetInt("score", GameObject.Find("GameManagers").GetComponent<GameManager>().Points);
            guardarDades();
        }
    }


	private void Update()
	{
        /*
        if (currentLevelName.Equals("level_5") ||
            currentLevelName.Equals("level_6"))
        {
            if (jugadorDintre && !sonant)
            {
                //if (soImpact != null)
                //{
                    audio.PlayOneShot(impact, soImpact);
                //}

                sonant = true;
                Invoke("goLevel", 0.1f);

            }
        }
        */
        if (CrossPlatformInputManager.GetButtonDown("Interact"))
		{
			if(jugadorDintre && !sonant)
			{
                if (currentLevelName.Equals("level_5"))
                {
                    if (PlayerPrefs.GetString("Targeta").Equals("Si"))
                    {
                        audio.PlayOneShot(impact, soImpact);

                        sonant = true;
                        Invoke("goLevel", tempsEspera);
                    }
                }
                else
                {
                    audio.PlayOneShot(impact, soImpact);

                    sonant = true;
                    Invoke("goLevel", tempsEspera);
                }



			}
		}
	}

	private void Awake()
	{

        if (PlayerPrefs.GetString("PistolaAgafada").Equals("Si"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>().Permissions.ShootEnabled = true;

        }

        int punts = PlayerPrefs.GetInt("score");
        GameManager.Instance.SetPoints(punts);

		this.audio = GetComponent<AudioSource> ();
		currentLevelName = Application.loadedLevelName;

        if (currentLevelName.Equals("level_6"))
        {
            if (PlayerPrefs.GetString("Targeta").Equals("Si"))
            {
                GameObject.Find("BlueVortex").SetActive(false);
            }
        }

        if(currentLevelName.Equals("level_3"))
        {
            if (PlayerPrefs.GetString("PistolaAgafada") != "Si")
            {
                GameObject.Find("GateToNextLevelDreta").transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject.Find("DialogueZone 2 (5)").transform.position = new Vector3(0, 0, 0);
            }

        }

        if (currentLevelName.Equals("level_8"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(0.3733732f * 2f, 0.3733732f * 2f);
        }
        else if(currentLevelName.Equals("level_3_casa") ||
            currentLevelName.Equals("level_3_casa_secondroom") )
        {
                    GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(0.3733732f * 1.75f, 0.3733732f * 1.75f);
        }

		cargarSave ();
	}

    private void cargarSave()
	{
        string path = Path.Combine(Application.persistentDataPath, Application.loadedLevelName + ".xml");

        if (File.Exists(path) && new FileInfo(path).Length != 0)
        {
            Dades dades = Dades.LoadFromFile(path);


            List<string> box_list_ = new List<string>();
            List<string> coin_list_ = new List<string>();
            List<string> coin_NO_FALL_list_ = new List<string>();
            List<string> mob_list_ = new List<string>();

            foreach (GameObject Box in GameObject.FindGameObjectsWithTag("Box"))
            {
                box_list_.Add(Box.name);
            }
            foreach (GameObject Coin in GameObject.FindGameObjectsWithTag("Coin"))
            {
                coin_list_.Add(Coin.name);
            }

            foreach (GameObject Coin_NO_FALL in GameObject.FindGameObjectsWithTag("Coin_NO_FALL"))
            {
                coin_NO_FALL_list_.Add(Coin_NO_FALL.name);
            }
            foreach (GameObject Mob in GameObject.FindGameObjectsWithTag("Mob"))
            {
                mob_list_.Add(Mob.name);
            }


            foreach (string box in box_list_)
            {
                if (!dades.Box.Contains(box))
                {
                    //set active false gameobject
                    GameObject gco = GameObject.Find(box);
                    gco.SetActive(false);
                }
            }


            foreach (string moneda in dades.Coin)
            {
                foreach (string coin in coin_list_)
                {
                    if (!dades.Coin.Contains(coin))
                    {
                        //set active false gameobject
                        GameObject gco = GameObject.Find(coin);
                        gco.SetActive(false);
                    }
                }
            }

            foreach (string coin_NO_FALL in coin_NO_FALL_list_)
            {
                if (!dades.Coin_NO_FALL.Contains(coin_NO_FALL))
                {
                    //set active false gameobject
                    GameObject gco = GameObject.Find(coin_NO_FALL);
                    gco.SetActive(false);
                }
            }



            foreach (string mob in mob_list_)
            {
                if (!dades.Mob.Contains(mob))
                {
                    //set active false gameobject
                    GameObject gco = GameObject.Find(mob);
                    gco.SetActive(false);
                }
            }
        }




	}

	private void OnTriggerEnter2D(Collider2D col) 
	{
		//Mirar si el que ha entrat en trigger es el jugador
		if (col.GetComponent<CharacterBehavior> () == null)
			return;

		//Indicar que el jugador esta davant de la porta
		jugadorDintre = true;

        if (anar_)
        {
            int i = GameManager.Instance.Points;
            PlayerPrefs.SetInt("score", i);
            if (currentLevelName.Equals("level_1"))
            {
                PlayerPrefs.SetString("Dreta1", "Si");
            }
            else if(currentLevelName.Equals("level_2"))
            {
                if (gameObject.name.Equals("GateToAnteriorLevel (1)"))
                {
                    PlayerPrefs.SetString("Esquerra2", "Si");    
                }
                else if (gameObject.name.Equals("GateToNextLevel (2)"))
                {
                    PlayerPrefs.SetString("Dreta2", "Si");
                }   

            }
            else if (currentLevelName.Equals("level_3"))
            {
                /*
                if (gameObject.name.Equals("porta"))
                {
                    PlayerPrefs.SetString("EntratCasa3", "Si");
                }
                    */
                /*else*/
                if (gameObject.name.Equals("GateToNextLevelEsquerra"))
                {
                    PlayerPrefs.SetString("EntratEsquerra3", "Si");
                }
                else if (gameObject.name.Equals("GateToNextLevelDreta"))
                {
                    PlayerPrefs.SetString("EntratDreta3", "Si");
                }
            }

                /*
            else if (currentLevelName.Equals("level_3_casa"))
            {
                if (gameObject.name.Equals("LevelStartEsq"))
                {
                    PlayerPrefs.SetString("EsquerraEntra3", "Si");           
                }
                else if (gameObject.name.Equals("dretaa"))
                {
                    PlayerPrefs.SetString("dretaa3", "Si");
                }
            }
                */
            else if (currentLevelName.Equals("level_4"))
            {
                if (gameObject.name.Equals("GateToNextLevel (2)"))
                {
                    PlayerPrefs.SetString("EsquerraEntra4", "Si");
                }
                else if (gameObject.name.Equals("GateToNextLevel (3)"))
                {
                    PlayerPrefs.SetString("dretaa4", "Si");                    
                }
            }
            else if (currentLevelName.Equals("level_5"))
            {
                if (gameObject.name.Equals("door_0_mod (2)"))
                {
                    PlayerPrefs.SetString("Baixat", "No");
                }
            }
            else if (currentLevelName.Equals("level_6"))
            {
                if (gameObject.name.Equals("door_0_mod (2)"))
                {
                    PlayerPrefs.SetString("Baixat", "Si");
                }
            }
            guardarDades();
            Application.LoadLevel(LevelName);
        }


	}

	private void OnTriggerExit2D(Collider2D col)
	{
        //Mirar si el que ha entrat en trigger es el jugador
        if (col.GetComponent<CharacterBehavior>() == null)
            return;

		jugadorDintre = false;
	}

	private void goLevel()
	{

		sonant = false;

		//if (anarALevel == true) 
		//{
			if(LevelName != null && LevelName != "")
			{
                if (currentLevelName.Equals("level_3_casa"))
                {

                    if (gameObject.name.Equals("col (2)"))
                    {
                        PlayerPrefs.SetString("EsquerraEntra3", "Si");           
                    }
                    else if (gameObject.name.Equals("dretaa"))
                    {
                        PlayerPrefs.SetString("dretaa3", "Si");
                    }
            
                }
                else if (currentLevelName.Equals("level_3"))
                {
                    if (gameObject.name.Equals("porta"))
                    {
                        PlayerPrefs.SetString("EntratCasa3", "Si");
                    }
                }
                else if (currentLevelName.Equals("level_3_casa_secondroom"))
                { 
                    if(gameObject.name.Equals("portaMonedes_3_casa"))
                    {
                        PlayerPrefs.SetString("dretaa3", "Si");
                    }
                }


                    guardarDades();
					Application.LoadLevel(LevelName);
			}
		//}

	}

    //Guardar la partida
	public void guardarDades()
	{

		
			Dades d = new Dades();


			List<string> box_list = new List<string>();
			List<string> coin_list = new List<string>();
			List<string> coin_NO_FALL_list = new List<string>();
			List<string> mob_list = new List<string>();


			foreach(GameObject Box in GameObject.FindGameObjectsWithTag("Box"))
			{
				box_list.Add(Box.name);
			}
			foreach(GameObject Coin in GameObject.FindGameObjectsWithTag("Coin"))
			{
				coin_list.Add(Coin.name);
			}

			foreach(GameObject Coin_NO_FALL in GameObject.FindGameObjectsWithTag("Coin_NO_FALL"))
			{
				coin_NO_FALL_list.Add(Coin_NO_FALL.name);
			}
			foreach(GameObject Mob in GameObject.FindGameObjectsWithTag("Mob"))
			{
				mob_list.Add(Mob.name);
			}


			d.Box = box_list;
			d.Coin = coin_list;
			d.Coin_NO_FALL = coin_NO_FALL_list;
			d.Mob = mob_list;

            string path_ = Path.Combine(Application.persistentDataPath, Application.loadedLevelName + ".xml");

           
            
			d.Save(path_);
            //d.Save("saves/"+Application.loadedLevelName+".xml");
		

	}

	
}


public class Dades
{

	public List<string> Box {get;set;}

	public List<string> Coin {get;set;}

	public List<string> Coin_NO_FALL {get;set;}

	public List<string> Mob { get; set;}
	 

	public void Save(string fileName)
	{
			using (var stream = new FileStream(fileName, FileMode.Create)) 
			{
				var XML = new XmlSerializer(typeof(Dades));

				XML.Serialize(stream, this);
			}
	}

	public static Dades LoadFromFile(string fileName)
	{
		using (var stream = new FileStream(fileName, FileMode.Open)) 
		{
			var XML = new XmlSerializer(typeof(Dades));
			return (Dades) XML.Deserialize(stream);
		}
	}


}

