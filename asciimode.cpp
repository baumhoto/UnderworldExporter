#include <stdio.h>
#include <stdlib.h>
#include <fstream>

#include "main.h"
#include "gameobjects.h"
#include "tilemap.h"
//#include "scripting.h"
#include "asciimode.h"


long printObject(ObjectItem &currObj, int TableFormat)
{
int value = (((currObj.owner & 0x7) <<3) | (currObj.y ));	//for check variables
if (objectMasters[currObj.item_id].isSet ==1)
	{
	if (TableFormat==0)
		{
		printf("Index:%d,Type:%d(%s),TileX=%d,TileY=%d,x=%d,y=%d,z=%d,heading=%d,qual=%d,owner=%d,link=%d,flags=%d,val=%d,angle=(%d,%d,%d)"
		,currObj.index ,currObj.item_id,objectMasters[currObj.item_id].desc,
		currObj.tileX,currObj.tileY,
		currObj.x,currObj.y,currObj.zpos
		,currObj.heading,currObj.quality,currObj.owner, currObj.link,currObj.flags,value,currObj.Angle1,currObj.Angle2,currObj.Angle3);

		}
	else
		{
		printf("%d\t%d\t%20s\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d"
		,currObj.index ,currObj.item_id,objectMasters[currObj.item_id].desc,
		currObj.tileX,currObj.tileY,
		currObj.x,currObj.y,currObj.zpos
		,currObj.heading,currObj.quality,currObj.owner, currObj.link,currObj.flags,value,currObj.Angle1,currObj.Angle2,currObj.Angle3);

		}
	//printf("[id=%d Type=%d(%.20s)(%d,%d,%d)%ddeg qual=%d link(q)=%d owner(s)=%d flags=%d isquant=%d]",currObj.index,currObj.item_id,objectMasters[currObj.item_id].desc,currObj.x ,currObj.y,currObj.zpos, currObj.heading ,currObj.quality,currObj.link,currObj.owner,currObj.flags,currObj.is_quant );
	}
	return currObj.next; 
}

long printObject(ObjectItem &currObj)
{//Prints the specified object and returns it's next object.
//printf("Index:%d,%s-",currObj.index, objectMasters[currObj.item_id].desc);
	printf("%s", UniqueObjectName(currObj));
return currObj.next;
}

void RenderAsciiTile(tile &t)	//,int x, int y, int BlockStart,unsigned char *buffer
{

switch (t.tileType )
	{
case 0:	
	{//solid	
		if (t.Render ==1) 
			{printf("#");}
		else 
			{printf("*");}
		return;
	}
case 1: 
	{//open
		if (t.Render ==1) 
			{printf(".");}
		else 
			{printf(".");}
		return;	
	}
case 2: 
		{//diag se
		if (t.Render ==1) 
			{printf("/");}
		else 
			{printf(" ");}
		return;	
	}
case 3: 
		{//diag sw
		if (t.Render ==1) 
			{printf("\\");}
		else 
			{printf(" ");}
		return;	
	}
case 4: 	
		{//diag ne
		if (t.Render ==1) 
			{printf("\\");}
		else 
			{printf(" ");}
		return;	
	}
case 5: 
	{//diag nw
		if (t.Render ==1) 
			{printf("/");}
		else 
			{printf(" ");}
		return;	
	}
case 6: 
		{//slope n
		if (t.Render ==1) 
			{printf("n");}
		else 
			{printf(" ");}
		return;	
	}
case 7: 	
		{//slope s
		if (t.Render ==1) 
			{printf("x");}
		else 
			{printf(" ");}
		return;	
	}
case 8: 	
		{//slope e
		if (t.Render ==1) 
			{printf("e");}
		else 
			{printf(" ");}
		return;	
	}
case 9: 	
	{//slopew
		if (t.Render ==1) 
			{printf("w");}
		else 
			{printf(" ");}
		return;	
	}
	
case 10:
	{//slope se->nw	valley
		if (t.Render ==1) 
			{printf("a");}
		else 
			{printf(" ");}
		return;	
	}
case 11:
	{//slope sw->ne valley
		if (t.Render ==1) 
			{printf("b");}
		else 
			{printf(" ");}
		return;	
	}
case 12:
	{//slope nw->se valley
		if (t.Render ==1) 
			{printf("c");}
		else 
			{printf(" ");}
		return;	
	}
case 13:
	{//slope ne->sw valley
		if (t.Render ==1) 
			{printf("d");}
		else 
			{printf(" ");}
		return;	
	}
	
case 14:
	{//slope nw->se ridge
		if (t.Render ==1) 
			{printf("f");}
		else 
			{printf(" ");}
		return;	
	}
case 15:
	{//slope ne->sw ridge
		if (t.Render ==1) 
			{printf("g");}
		else 
			{printf(" ");}
		return;	
	}
case 16:
	{//slope se->nw ridge
		if (t.Render ==1) 
			{printf("h");}
		else 
			{printf(" ");}
		return;	
	}
case 17:
	{//slope sw->ne ridge
		if (t.Render ==1) 
			{printf("i");}
		else 
			{printf(" ");}
		return;	
	}


default:
	{
	if (t.Render ==1) 
			{printf("?");}
		else 
			{printf("?");}
		return;	
	}
}
}

void DumpAscii(int game,tile LevelInfo[64][64],ObjectItem objList[1600],int LevelNo,int mapOnly)
{

	printTileMap(LevelInfo,LevelNo);

	if (mapOnly == 1) {return;}

	printFloorHeights(LevelInfo,LevelNo);

	printFloorTextures(LevelInfo,LevelNo);

	printWallTextures(LevelInfo,LevelNo);

	printDoorPositions(LevelInfo, objList, LevelNo);
	
	//printWaterRegions(LevelInfo,LevelNo);

	printNeighbourCounts(LevelInfo, LevelNo);

	printRoomRegions(LevelInfo,LevelNo);

if (game == SHOCK)
	{
	printCeilingHeights(LevelInfo,LevelNo);

	printSlopeSteepness(LevelInfo,LevelNo);
	printSlopeFlags(LevelInfo,LevelNo);

	printAdjacentFlags(LevelInfo,LevelNo);

	printTextureOffsets(LevelInfo,LevelNo);

	PrintShadeValues(LevelInfo,LevelNo);
	PrintLightStates(LevelInfo,LevelNo);

	//printf("\nPrint out audio logs for :%d\n",LevelNo);	
	//printf("Desc ObjID TileX TileY LogChunk\n");
	//for (y=63; y>=0;y--)
	//	{
	//	for (x=0; x<64;x++)
	//		{
	//		if(LevelInfo[x][y].indexObjectList !=0)
	//			{
	//			long nextShockObj = LevelInfo[x][y].indexObjectList;
	//			while (nextShockObj !=0)
	//				{
	//				if (isLog(objList[nextShockObj])==1)
	//					{
	//					EMAILScript(objectMasters[objList[nextShockObj].item_id].desc
	//										,objList[nextShockObj].tileX
	//										,objList[nextShockObj].tileY
	//										,objList[nextShockObj].index
	//										,objList[nextShockObj].Property1);
	//					//printf("%s %d %d %d %d\n",
	//					//	objectMasters[objList[nextShockObj].item_id].desc 
	//					//	,objList[nextShockObj].index
	//					//	,objList[nextShockObj].tileX
	//					//	,objList[nextShockObj].tileY
	//					//	,objList[nextShockObj].Property1
	//					//	 );
	//					}
	//				nextShockObj=objList[nextShockObj].next ;
	//				}
	//			}
	//		}
	//	}
		
	//return;
	}	
else
{
	PrintLevelEntrances(LevelInfo,objList,LevelNo);
}
	
	PrintObjectsByTile(LevelInfo, objList, LevelNo);



	//printf("\nprint switches and their targets\n");
	//	for (x=0;x<=1024;x++)
	//	{
	//	if (objectMasters[objList[x].item_id].type == BUTTON)
	//		{//check if this what this switches state is.
	//		printf("index:%d-state:%d-",objList[x].index, objList[x].flags);
	//		printf("trigger:%s-", objectMasters[objList[objList[x].link].item_id].desc);
	//		printf("Points:%s-",objectMasters[objList[objList[objList[x].link].link].item_id].desc);
	//		printf("x-%d-y:%d",objList[objList[x].link].quality , objList[objList[x].link].owner);
	//		printf("\n");
	//		}
	//	}
	
	//printf("\nPrint switches and trigger chains\n");
	//	printf("Index\tType\t%-20s\tTileX\tTileY\tx\ty\tz\theading\tqual\towner\tlink\tflags\tValue\n","Description");	
	//	for (y=63; y>=0;y--)
	//	{
	//	for (x=0; x<64;x++)
	//		{
	//		if(LevelInfo[x][y].indexObjectList !=0)	//there is an object in this tile
	//			{
	//			//printf("\nAt tile x=%d, y=%d :",x,y);
	//			long nextObj = LevelInfo[x][y].indexObjectList;
	//			while (nextObj !=0)
	//				{
	//				if ((isTrigger(objList[nextObj]) || (isButton(objList[nextObj])) || (isTrigger(objList[objList[nextObj].link]))))
	//					{
	//					//long nextInChain = nextObj;
	//					printObject(objList[nextObj],1);	//Prints the first object the inital trigger.
	//					long nextInChain = objList[nextObj].link; 
	//					printf("\n{\n");
	//					while (nextInChain !=0)
	//						{
	//						if (isTrigger(objList[nextInChain]) || (isButton(objList[nextInChain])) || (isTrap(objList[nextInChain])) || (isLock(objList[nextInChain])))
	//							{
	//							switch (objectMasters[objList[nextInChain].item_id].type)
	//								{
	//								case A_DELETE_OBJECT_TRAP:	//Need to stop on this due to infinite looks if the trigger object is being deleted.
	//								case LOCK://A lock uses it's link to set the key needed. stop here.
	//									{
	//									printObject(objList[nextInChain],1);printf("\n");
	//									nextInChain=0;
	//									break;
	//									}
	//								default:
	//									{
	//									printObject(objList[nextInChain],1); printf("\n");
	//									nextInChain = objList[nextInChain].link;
	//									}
	//								}
	//							}
	//						else
	//							{
	//							if (nextInChain !=0)
	//							{
	//								printf("break on no trigger,trap or button\n");
	//								printObject(objList[nextInChain],1);	printf("\n");								
	//								nextInChain=0;
	//								}
	//							}

	//						}
	//					printf("}\n\n\n");
	//					}
	//				else if (objList[nextObj].enchantment ==1)
	//					{//enchanted objects.
	//					printObject(objList[nextObj],1);
	//					printf("\n{\n");
	//					//dummy trigger for the spell script
	//						//Index	Type	Description  TileX	TileY	x	y	z	heading	qual	owner	link	flags						
	//					int link=objList[nextObj].link;	//the link changes depending on the object type
	//					switch	(objectMasters[objList[nextObj].item_id].type )
	//						{//TODO:This is probably wrong.
	//						case A_FOUNTAIN:
	//							//no adjustment
	//							link=objList[nextObj].link-512;
	//							break;
	//						default:
	//							link=objList[nextObj].link-512;
	//							if(link <=63){link=link+256;}
	//							else{link=link+144;}
	//							break;
	//						}					
	//					printf("99999\t99999\tenchantment\t-1\t-1\t0\t0\t0\t0\t%d\t%d\t%d\t%d",objList[nextObj].quality,objList[nextObj].owner,link,objList[nextObj].flags );
	//					printf("\n}\n");
	//					}
	//				nextObj=nextObject(objList[nextObj]);
	//				}
	//			}
	//		}
	//	}
		

}

void printTileMap(tile LevelInfo[64][64], int LevelNo)
{
//Prints the tilemap 
	int x; int y;
	printf("\nNow Printing Tile for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--) //invert for ascii
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			RenderAsciiTile(LevelInfo[x][y]);
			printf("|");//delimiter.
		}
	}

}

void printFloorHeights(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nNow Printing Floor Heights for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%02d|", LevelInfo[x][y].floorHeight);
		}
	}

}

void printFloorTextures(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nNow Printing floor textures for level :%d.(##)", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%d|", LevelInfo[x][y].floorTexture);
		}
	}
}

void printDoorPositions(tile LevelInfo[64][64], ObjectItem objList[1600],int LevelNo)
{
int x; int y;
	printf("\nNow Printing door positions for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			if (LevelInfo[x][y].isDoor == 1)
			{
				printf("%d(%d,%d)|", objList[LevelInfo[x][y].indexObjectList].heading, objList[LevelInfo[x][y].indexObjectList].x, objList[LevelInfo[x][y].indexObjectList].y);
			}
			else
			{
				printf(".|");
			}
		}
	}

}

void printWallTextures(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nNow Printing wall textures for level :%d.(##)", LevelNo);
	printf("\n\tNote that the rendered textures are the adjacent north, south, east and west values which are got from the neighbour tiles.");
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			printf("%d|", LevelInfo[x][y].wallTexture);
		}
	}
}

void printAdjacentFlags(tile LevelInfo[64][64], int LevelNo)
{
int x;int y;
	printf("\nNow Printing adjacent flags for level :%d.(##)", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			printf("%d|", LevelInfo[x][y].UseAdjacentTextures);
		}
	}
}

void printTextureOffsets(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nNow printing texture offsets :%d.(##)", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			printf("%d|", LevelInfo[x][y].shockTextureOffset);
		}
	}
}

void printCeilingHeights(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nPrint out ceiling height by tile for level :%d\n", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%02d|", LevelInfo[x][y].ceilingHeight);
		}
	}
}

void printSlopeSteepness(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nPrint out slope steepness by tile for level :%d\n", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%02d|", LevelInfo[x][y].shockSteep);
		}
	}
}

void printSlopeFlags(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;

	printf("\nPrint out slope flags by tile for level :%d\n", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%02d|", LevelInfo[x][y].shockSlopeFlag);
		}
	}
}

void PrintObjectsByTile(tile LevelInfo[64][64], ObjectItem objList[1600], int LevelNo)
{
int x; int y;
	printf("\nPrint out objects by Tile for level :%d\n", LevelNo);

	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{

			if (LevelInfo[x][y].indexObjectList != 0)
			{
				//printf("\nAt tile x=%d, y=%d :",x,y);
				long nextShockObj = printObject(objList[LevelInfo[x][y].indexObjectList]);
				while (nextShockObj != 0)
				{
					printf("-");
					nextShockObj = printObject(objList[nextShockObj]);
					
				}
			}
			else
			{
				printf("[]");
			}
			printf("*");
		}
	}

}

void PrintShadeValues(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nPrint out shade value by tile for level :%d\n", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%d-%d|", LevelInfo[x][y].shockShadeUpper, LevelInfo[x][y].shockShadeLower);
		}
	}
}

void PrintLightStates(tile LevelInfo[64][64], int LevelNo)
{
int x; int y;
	printf("\nPrint out light state by tile for level :%d\n", LevelNo);
	for (y = 63; y >= 0; y--)
	{
		printf("\n");
		for (x = 0; x<64; x++)
		{
			printf("%d-%d|", (LevelInfo[x][y].SHOCKSTATE[3] >> 4 & 0x0F), (LevelInfo[x][y].SHOCKSTATE[3] & 0x0F));
		}
	}
}

void PrintLevelEntrances(tile LevelInfo[64][64], ObjectItem objList[1600], int LevelNo)
{
	printf("\nLevel Entrances\n");		//go through the objects and find teleport traps with a zpos !=0
	printf("LevelNo\tTileX\tTileY\n");
	for (int i = 0; i<1024; i++)
	{
		if ((isTrigger(objList[i])) && (objectMasters[objList[objList[i].link].item_id].type == A_TELEPORT_TRAP))
		if ((objList[objList[i].link].zpos != 0))	//Trap goes to another level
		{
			printf("%d\t%d\t%d\n", objList[objList[i].link].zpos - 1, objList[objList[i].link].quality, objList[objList[i].link].owner);
		}
	}
}

void PrintUWObjects(ObjectItem objList[1600])
{
//Prints the object debug info for UW.
	for (int x = 0; x < 1024; x++)
		{
		//if (objList[x].InUseFlag == 1)
		//{
		UniqueObjectName(objList[x]);
		printf("\n\nIndex: %d", objList[x].index);
		printf("\tName: %s", UniqueObjectName(objList[x]));
		if(objectMasters[objList[x].item_id].isSet==1)
			{
			printf("\n\tObject Type : %d %s", objList[x].item_id, objectMasters[objList[x].item_id].desc);
			}
		else
			{
			printf("\n\tObject Type : %d %s", objList[x].item_id, "Unknown or bugged");
			}
		printf("\n\tLocation Tile(%d,%d) Position(%d,%d,%d) Heading (%d)", objList[x].tileX, objList[x].tileY, objList[x].x, objList[x].y, objList[x].zpos, objList[x].heading);
		printf("\n\nIn use %d", objList[x].InUseFlag);
		printf("\n\tFlags: %d", objList[x].flags);
		printf("\n\t\tEnchantment: %d", objList[x].enchantment);
		printf("\n\t\tDoordir : %d", objList[x].doordir);
		printf("\n\t\tInvis : %d", objList[x].invis);
		printf("\n\t\tIs Quant : %d", objList[x].is_quant);

		printf("\n\tQuality : %d", objList[x].quality);
		printf("\n\tNext : %d", objList[x].next);

		printf("\n\tOwner : %d", objList[x].owner);
		if (objList[x].is_quant == 1)
		{
			if (objList[x].link > 512)
			{
				printf("\n\tSpecial Property) : %d", objList[x].link);
				printf("\tless 512 is %d : ", objList[x].link - 512);
			}
			else
			{
				printf("\n\tQuantity : %d", objList[x].link);
			}

		}
		else
		{
			printf("\n\tSpecial Link : %d", objList[x].link);
		}

		if (objList[x].npc_whoami >0)
			{
			printf("\n\tNPC Who Am I : %d ", objList[x].npc_whoami);
			printf("\n\tNPC Attitude : %d ", objList[x].npc_attitude);
			}

		}
	//}
}

void printWaterRegions(tile LevelInfo[64][64], int LevelNo)
{
	//Prints the tilemap 
	int x; int y;
	printf("\nNow Printing Water Regions for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--) //invert for ascii
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			printf("%02d-",LevelInfo[x][y].roomRegion);
		}
	}

}

void printNeighbourCounts(tile LevelInfo[64][64], int LevelNo)
{
	int x; int y;
	printf("\nNow Printing Neighbour counts for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--) //invert for ascii
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			printf("%d-", LevelInfo[x][y].noOfNeighbours);
		}
	}
}


void printRoomRegions(tile LevelInfo[64][64], int LevelNo)
{
	int x; int y;
	printf("\nNow Printing Room  regions for level :%d.", LevelNo);
	for (y = 63; y >= 0; y--) //invert for ascii
	{
		printf("\n");
		for (x = 0; x <= 63; x++)
		{
			if (LevelInfo[x][y].tileType == TILE_SOLID)
				{
				printf("S%03d-", LevelInfo[x][y].roomRegion);
				}

				else if (LevelInfo[x][y].isWater == 1)
					{
						printf("W%03d-", LevelInfo[x][y].roomRegion);
					}
				else if (LevelInfo[x][y].isCorridor == 1)
					{
						printf("C%03d-", LevelInfo[x][y].roomRegion);
					}
				else if (LevelInfo[x][y].isDoor == 1)
				{
					printf("D%03d-", LevelInfo[x][y].roomRegion);
				}
				else
					{
						printf("R%03d-", LevelInfo[x][y].roomRegion);
					}
		}
	}
}