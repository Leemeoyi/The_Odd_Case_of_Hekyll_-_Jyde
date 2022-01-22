using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
	public int potionNum;
	public bool isHeckyll;

	GameObject collidedObj;

	void Update()
    {		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (isHeckyll && potionNum > 0)
			{
				//drink_potion/becoming_DrJyde [Animation]
				isHeckyll = false;
				potionNum -= 1;
				Debug.Log("Drink potion. Turning into DrJyde");
				Debug.Log(potionNum);
			}
			else if (!isHeckyll)
			{
				//becoming_Heckyll [Animation]
				isHeckyll = true;
				Debug.Log("Turning into Heckyll");
			}
			else
			{
				//should prompt player for the potion finshed
				Debug.Log("Potion finished, " + potionNum);
			}
		}

		if (Input.GetKeyDown(KeyCode.E) && isHeckyll)
		{
			if (collidedObj != null)
			{
				Debug.Log("Destroy");
				//still need to add only can kill citizen and not police.
				//maybe use tag?
				//if(other.gameObject.CompareTag("Citizen"))
				Destroy(collidedObj);
			}
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