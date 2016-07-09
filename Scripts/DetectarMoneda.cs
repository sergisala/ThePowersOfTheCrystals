using UnityEngine;
using System.Collections;

public class DetectarMoneda : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D col)
	{
		//Mirar si es moneda o caixa
		if(col.gameObject.tag.Equals("Coin"))
		{
			Destroy(col.gameObject.GetComponent<Rigidbody2D>());

            //
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Box"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
            }

            //
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Coin"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
            }
		}
	}

}
