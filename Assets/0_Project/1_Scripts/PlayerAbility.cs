using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
	public int potionNum;
	public bool isHeckyll;
	public bool canKill;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (isHeckyll && potionNum > 0)
			{
				//drink_potion/becoming_DrJyde [Animation]
				canKill = false;
				isHeckyll = false;
				potionNum -= 1;
				Debug.Log("Drink potion. Turning into DrJyde");
				Debug.Log(potionNum);
			}
			else if (!isHeckyll)
			{
				//becoming_Heckyll [Animation]
				canKill = true;
				isHeckyll = true;
				Debug.Log("Turning into Heckyll");
			}
			else
			{
				//should prompt player for the potion finshed
				Debug.Log("Potion finished, " + potionNum);
			}
		}

	}
}
