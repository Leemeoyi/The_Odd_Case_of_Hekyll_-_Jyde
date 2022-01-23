using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
	public int potionNum;
	public bool isHeckyll;

	public Sprite drJyde;
	public Sprite heckyll;
	GameObject collidedObj;

	void Update()
	{		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (isHeckyll && potionNum > 0)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = drJyde;
				isHeckyll = false;
				potionNum -= 1;
				Debug.Log("Drink potion. Turning into DrJyde");
				Debug.Log(potionNum);
			}
			else if (!isHeckyll)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = heckyll;
				isHeckyll = true;
				Debug.Log("Turning into Heckyll");
			}
			else
			{
				Debug.Log("Potion finished, " + potionNum);
			}
		}

		if (Input.GetKeyDown(KeyCode.E) && isHeckyll)
		{
			if (collidedObj != null && collidedObj.tag == "folk")
			{
				Debug.Log("Destroy");
					Destroy(collidedObj);
			}
		}

		if (collidedObj != null && collidedObj.tag == "police")
		{
			Debug.Log("Player die");
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		print(message: "Enter collision");
		collidedObj = col.gameObject;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		print(message: "Exit collision");
		collidedObj = null;
	}
}
