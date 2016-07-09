using UnityEngine;
using System.Collections;

public class MenuBotonsC : MonoBehaviour {


    //Propi script: menu-opcions
	public bool menu = false;


	//bool options = false;
	bool sound = false;
	bool video = false;
	bool fullscreen = true;
    //bool zoomCanviat = false;
	//bool sfxToggle  = true;
    int masterVol = 4;
	int musicVol  = 6;
    public int so = 4;

	int fieldOfView  = 7;

	int resX;
	int resY;

    float augment = 4f;

	private string currentLevel;

	private void Awake()
	{
		currentLevel = Application.loadedLevelName;


        resX = PlayerPrefs.GetInt("resX", resX);
        resY = PlayerPrefs.GetInt("resY", resY);

        if (resX != 0)
        {
            if (PlayerPrefs.GetString("fullscreen").Equals("Si"))
            {
                Screen.SetResolution(resX, resY, true);
            }
            else
            {
                Screen.SetResolution(resX, resY, false);
            }
        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
        }


        if (PlayerPrefs.HasKey("Master Vol"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Master Vol");
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            this.so = PlayerPrefs.GetInt("Music");
        }

        this.so = 4;

        if (currentLevel.Equals("intro") || currentLevel.Equals("sewers") || currentLevel.Equals("mortUncle"))
        {
            this.so = 0;
            return;
        }

        if (currentLevel.Equals("menu"))
        {
            Debug.Log(PlayerPrefs.GetString("continuar"));
            if (PlayerPrefs.GetString("continuar") == "")
            {
                
                GameObject.Find("Carregar").SetActive(false);
            }

        }

        fieldOfView = PlayerPrefs.GetInt("Zoom");

        if(currentLevel.Equals("level_7"))
        {
            fieldOfView = 4;
        }



        if (fieldOfView != 0)
        {


            CameraController.MinimumZoom = fieldOfView;
            CameraController.MaximumZoom = fieldOfView;

        }
        else
        {
            fieldOfView = 7;
            
            
            CameraController.MinimumZoom = fieldOfView;
            CameraController.MaximumZoom = fieldOfView;
        }


               
	}



	//Mètodes utilitzats per l'editor

	public void NewGame()
	{
        Application.LoadLevel("intro");
	}

    public void Continuar()
    {
        Application.LoadLevel(PlayerPrefs.GetString("continuar"));
    }

	public void QuitGame()
	{
		Application.Quit();
	}

	public void Website()
	{
		Application.OpenURL("http://sergisala.com/powers");
	}




    void OnGUI () {

        if(menu){

		if (GUI.Button( new Rect(Screen.width/2 - 50*4, (Screen.height/2 -60*4), 100*augment, 30*augment), "Audio Settings")){
			
			sound = true;
		}
		
		
		if (GUI.Button( new Rect(Screen.width/2 - 50*4, (Screen.height/2 -60 ), 100*augment, 30*augment), "Video Settings")){
			
			video = true;
		}
        
            /*
        if (GUI.Button( new Rect(Screen.width/2 - 50, Screen.height/2 + 90, 100, 30), "Back")){
				if(!currentLevel.Equals("menu") && !currentLevel.Equals("intro") && !currentLevel.Equals("sewers") && !currentLevel.Equals("mortUncle"))
				{
					GetComponent<GameManager>().Pause();
				}

				menu = false;
        
        	}
            */
		
			if(!currentLevel.Equals("menu"))
			{
				if(GUI.Button( new Rect(Screen.width/2 - 50*4, (Screen.height/2 + 30*4), 100*augment, 30*augment), "Exit game"))
				{

                    PlayerPrefs.SetString("continuar", Application.loadedLevelName);

                    if (!currentLevel.Equals("intro"))
                    {
                        GameObject.Find("GameManagers").GetComponent<DoorLevel>().guardarDades();

                        menu = false;
                        //Application.LoadLevel("menu");
                        Application.Quit();
                    }
                    else
                    {
                        menu = false;
                        //Application.LoadLevel("menu");
                        Application.Quit();
                    }
                }
			}


        }


        if(sound){

		menu = false;

     
        //sfxVol = (float) GUI.HorizontalSlider(new Rect(Screen.width / 2f - 50f, Screen.height / 2f, 100f, 30f), sfxVol, 0.0f, 10.0f);

        //sfxToggle = GUI.Toggle(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 5, 100, 30), sfxToggle, "Sound Effects: ");



        musicVol = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 200, (Screen.height / 2 - 70), 100f*augment , 30f ), musicVol, 0.0f, 10.0f);
        GUI.Label(new Rect((Screen.width / 2 - 200), (Screen.height / 2 - 70), 100*augment , 30 ), "Music: " + musicVol);

        masterVol = (int)GUI.HorizontalSlider(new Rect( (Screen.width / 2 - 200), (Screen.height / 2 ), 100f*augment , 30f ), masterVol, 0.0f, 10.0f);
        GUI.Label(new Rect((Screen.width / 2 - 200), (Screen.height / 2 ), 100 *augment , 30 ), "Master Vol: " + masterVol);

        if (GUI.Button(new Rect( (Screen.width / 2 - 50*4), (Screen.height / 2 + 30*4), 100 * augment, 30 * augment), "Back"))
        {
            AudioListener.volume = masterVol;
            this.so = (musicVol/10);

            PlayerPrefs.SetFloat("Master Vol", AudioListener.volume);
            PlayerPrefs.SetInt("Music", so);
            //so
				menu = true;
        		sound = false;

        	}
        }


        if(video){

		menu = false;


        fieldOfView = (int)GUI.HorizontalSlider(new Rect((Screen.width / 2f - 200), (Screen.height / 2f - 400), 100f * augment, 30f), fieldOfView, 7f, 1f);
        GUI.Label(new Rect((Screen.width / 2 - 200), (Screen.height / 2 - 400), 100 * augment, 30), "Zoom: " + fieldOfView);




        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 330, 100 * augment, 30 * augment), "1080p"))
			{
				Screen.SetResolution(1920, 1080, fullscreen);

				resX = 1920;
				resY = 1080;


			}

            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 100 * augment, 30 * augment), "720p"))
			{
				Screen.SetResolution(1280, 720, fullscreen);

				resX = 1280;
				resY = 720;
			}

			if (GUI.Button(new Rect(Screen.width/2 - 200,  Screen.height/2 -70 , 100*augment, 30*augment), "480p"))
			{
				Screen.SetResolution(640, 480, fullscreen);

				resX = 640;
				resY = 480;
			}

            PlayerPrefs.SetInt("resX", resX);
            PlayerPrefs.SetInt("resY", resY);

            if (fullscreen)
            {
                PlayerPrefs.SetString("fullscreen", "Si");
            }
            else
            {
                PlayerPrefs.SetString("fullscreen", "Si");
            }
            
			fullscreen = GUI.Toggle(new Rect(Screen.width/2 - 200,  Screen.height/2 + 70, 100*augment, 30), fullscreen, "Fullscreen");

			if(resX != 0)
			{
				Screen.SetResolution(resX, resY, fullscreen);
			}


            //+90
            if (GUI.Button(new Rect( (Screen.width / 2 - 200), (Screen.height / 2 + 120), 100 * augment, 30 * augment), "Back"))
            {

            PlayerPrefs.SetInt("Zoom", fieldOfView);
            //Canviar zoom
            fieldOfView = PlayerPrefs.GetInt("Zoom");

            CameraController.MinimumZoom = fieldOfView;
            CameraController.MaximumZoom = fieldOfView;

		menu = true;
        video = false;



    }
  }
}
}
