﻿using UnityEngine;
using System.Collections;

public class Feet : UWEBase {
	//For a game object attached to the player. Detects if the player is in contact with the ground.

	//public TileMap tm;
	public float currY;
	public float fallSpeed;
	int waterLayer;
	int landLayer;
	int lavaLayer;


	void Start()
	{
		waterLayer=LayerMask.NameToLayer("Water");
		landLayer=LayerMask.NameToLayer("MapMesh");
		lavaLayer=LayerMask.NameToLayer("Lava");
	}

	void OnTriggerStay(Collider other) {
		//UWCharacter.Instance.currRegion=other.gameObject.tag;
		if (other.gameObject.layer==landLayer)
		{
			TileMap.OnGround=true;  
		}
		else
		{
			if (other.gameObject.layer==waterLayer)
			{
				if (TileMap.OnWater==false)
				{
					UWCharacter.Instance.aud.clip=GameWorldController.instance.getMus().SoundEffects[MusicController.SOUND_EFFECT_WATER_LAND_1];
					UWCharacter.Instance.aud.Play();
				}
				TileMap.OnWater=true;  
			}
			else 
			{
				if (other.gameObject.layer==lavaLayer)
				{
					TileMap.OnGround=true;  
					TileMap.OnLava=true;
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
	  if (other.gameObject.layer==landLayer)
		{
			TileMap.OnGround=false;  
		}
		else
		{
			if (other.gameObject.layer==waterLayer)
			{
				TileMap.OnWater=false;  
			}
			else 
			{
				if (other.gameObject.layer==lavaLayer)
				{
					TileMap.OnGround=false;
					TileMap.OnLava=false;
				}
			}
		}
	}

	void Update()
	{//http://forum.unity3d.com/threads/fall-damage-question.46101/
		//onGround = TileMap.OnGround;
		//veloY = UWCharacter.Instance.playerMotor.movement.velocity.y;
		if (TileMap.OnGround==false)
		{
			if (UWCharacter.Instance.playerMotor.movement.velocity.y < currY)
			{
				fallSpeed= Mathf.Max(-UWCharacter.Instance.playerMotor.movement.velocity.y, fallSpeed);
				//UWCharacter.Instance.playerMotor.movement.velocity.y;
			}
			else
			{
				fallSpeed=0.0f;
			}
		}
		else
		{			
			if (fallSpeed>0.0f)
			{							
				//Check fall damage.
				GameWorldController.instance.PositionDetect();//check where I am.
				UWCharacter.Instance.onLanding(fallSpeed);
				fallSpeed=0.0f;
			}
		}
		currY =UWCharacter.Instance.playerMotor.movement.velocity.y;
	}
}
