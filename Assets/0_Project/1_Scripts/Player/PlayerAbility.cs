using System;
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
			if (playerCore.IsHeckyll && playerCore.potionNum > 0 && !playerCore.JustUsedPotion)
			{
				sr.sprite = drJyde;
				playerCore.TransformingToJyde();
			}
		}

		if (!playerCore.IsHeckyll)
		{
			playerCore.TransformationTimer -= Time.deltaTime;

			if (playerCore.TransformationTimer < 0)
			{
				sr.sprite = heckyll;
				playerCore.TransformingToHeckyll();
			}
		}
		
	}

}
