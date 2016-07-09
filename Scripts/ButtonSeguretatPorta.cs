using UnityEngine;
using System.Collections;

public class ButtonSeguretatPorta : MonoBehaviour {


	private void Awake() 
    {
        if (PlayerPrefs.GetString("Targeta").Equals("Si"))
        {
            GetComponent<DialogueZone>().Dialogue = new string[] 
            {"Welcome to Crystals Corporation!", 
                "Please, insert your employee card to enter our lab",
                "*You insert the card*"};
        }
	}
}
