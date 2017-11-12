﻿using UnityEngine;
using System.Collections;

public class a_move_trigger : trigger_base {
/*
A trigger that fires when the player character enters it
*/
	bool playerStartedInTrigger=false;
	
	protected override void Start ()
	{
				
		base.Start ();
		if ( (TileMap.visitTileX==objInt().tileX) && (TileMap.visitTileY==objInt().tileY))
		{//TODO:do this based on trigger box
			playerStartedInTrigger=true;
		}
		BoxCollider box=this.gameObject.AddComponent<BoxCollider>();
		//box.transform.position = this.gameObject.transform.position;
		//box.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		box.size = new Vector3(1.2f, 1.2f, 1.2f);

		box.isTrigger=true;
		Collider[] colliders=Physics.OverlapBox(this.transform.position, box.size);
		for (int i=0; i<=colliders.GetUpperBound(0);i++)
		{
			if( (colliders[i].gameObject.GetComponent<UWCharacter>()!=null) ||  (colliders[i].gameObject.GetComponent<Feet>()!=null) )
			{
				playerStartedInTrigger=true;
				break;
			}
		}

		//Check if this trap points to a teleport trap that goes to another level
		if ( (objInt().tileX<TileMap.TileMapSizeX) && (objInt().tileY<TileMap.TileMapSizeY))
		{
			if (objInt().link!=0)
			{
				GameObject triggerObj = ObjectLoader.getGameObjectAt(objInt().link);
				if (triggerObj!=null)
				{
					if (triggerObj.GetComponent<a_teleport_trap>() !=null)
					{
						if (triggerObj.GetComponent<a_teleport_trap>().objInt().zpos!=0)
						{
							//if (_RES==GAME_UW1)
							//{
							GameWorldController.instance.currentAutoMap().MarkTileDisplayType(objInt().tileX, objInt().tileY, AutoMap.DisplayTypeStair);
							//}						
						}
					}
				}	
			}	
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (playerStartedInTrigger!=true)
		{
			if (((other.name==UWCharacter.Instance.name) || (other.name=="Feet")) && (!GameWorldController.EditorMode))
			{
				Debug.Log(this.name);
				Activate ();
			}	
		}
		else
		{
			playerStartedInTrigger=false;
		}
	}

	public override bool Activate ()
	{
		if (_RES==GAME_UW2)
		{//Check for moongates in this tile to support qbert in UW2.
			if(GameWorldController.instance.LevelNo==68)
			{
				ObjectLoaderInfo[] objList=GameWorldController.instance.CurrentObjectList().objInfo;
				for (int i = 0; i < 1024;i++)
				{//Make sure triggers, traps and special items are created.
					if (objList[i]!=null)
					{
						if (GameWorldController.instance.objectMaster.type[objList[i].item_id] == ObjectInteraction.MOONGATE)
						{
							if ((objList[i].tileX == objInt().tileX) && (objList[i].tileY == objInt().tileY))
							{
								if (objList[i].instance.invis==1)
								{//No teleporting on an invisible moon gate in this level.
										return false;
								}
								else
								{//Okay to teleport
										break;
								}
							}
						}
					}
				}
			}
		}
		return base.Activate ();
	}
}
