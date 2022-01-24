using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore), typeof(Rigidbody2D))]
public class PlayerAbility : MonoBehaviour
{
	public Sprite drJyde;
	public Sprite heckyll;

	SpriteRenderer sr;
	PlayerCore playerCore;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		playerCore = GetComponent<PlayerCore>();
	}

	void Update()
	{		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (playerCore.IsHeckyll && playerCore.potionNum > 0)
			{
				sr.sprite = drJyde;
				playerCore.IsHeckyll = false;
				playerCore.potionNum--;
			}
			else if (!playerCore.IsHeckyll)
			{
				sr.sprite = heckyll;
				playerCore.IsHeckyll = true;
			}
			else
			{
				Debug.Log("Potion finished, " + playerCore.potionNum);
			}
		}
	}
}
