﻿using UnityEngine;
using System.Collections;

public class CritterInfo {

	public const int idle_combat = 0x0;
	public const int attack_bash = 0x1;
	public const int attack_slash = 0x2;
	public const int attack_thrust = 0x3;
	public const int attack_unk4 = 0x4;
	public const int attack_secondary = 0x5;
	public const int attack_unk6 = 0x6;
	public const int walking_towards = 0x7;
	public const int death = 0xc;
	public const int begin_combat = 0xd;
	public const int idle_rear = 0x20;
	public const int idle_rear_right = 0x21;
	public const int idle_right = 0x22;
	public const int idle_front_right = 0x23;
	public const int idle_front = 0x24;
	public const int idle_front_left = 0x25;
	public const int idle_left = 0x26;
	public const int idle_rear_left = 0x27;
	public const int walking_rear = 0x80;
	public const int walking_rear_right = 0x81;
	public const int walking_right = 0x82;
	public const int walking_front_right = 0x83;
	public const int walking_front = 0x84;
	public const int walking_front_left = 0x85;
	public const int walking_left = 0x86;
	public const int walking_rear_left = 0x87;



	public int Item_Id;
	public int FileNo;
	public int AuxPalNo;
	char[] FilePage0;
	char[] FilePage1;
	public Palette pal; //the game pal.
	private Palette auxpal;

	public CritterAnimInfo AnimInfo;

	public CritterInfo(int critter_id, Palette paletteToUse, int palno)
		{
		//string fileO = DecimalToOct(file.ToString());
		string critterIDO = DecimalToOct(critter_id.ToString());
		//FileNo=file;
		AuxPalNo=palno;
		pal=paletteToUse;
		AnimInfo=new CritterAnimInfo();
		int spriteIndex=0;
		for (int pass=0; pass<2;pass++)
			{
			//load in both page files.
			if (pass==0)
				{//CR{CRITTER file ID in octal}PAGE.N{Page}
				DataLoader.ReadStreamFile("c:\\games\\uw1\\crit\\CR" + critterIDO  +"page.n0" + pass,out FilePage0);
				spriteIndex= ReadPageFile(FilePage0,critter_id,pass,spriteIndex);
				}
			else
				{
				DataLoader.ReadStreamFile("c:\\games\\uw1\\crit\\CR" + critterIDO  +"page.n0" + pass,out FilePage1);
				ReadPageFile(FilePage1,critter_id,pass,spriteIndex);
				}

			}
		}


	private int ReadPageFile(char[] PageFile, int XX, int YY ,int spriteIndex)
		{
		int addptr=0;
		int slotbase = (int)DataLoader.getValAtAddress(PageFile,addptr++,8);
		int NoOfSlots =(int)DataLoader.getValAtAddress(PageFile,addptr++,8);
		int[] SlotIndices = new int[NoOfSlots];
		int spriteCounter=0;
		int k=0;
		string XXo = DecimalToOct(XX.ToString());
		string YYo = DecimalToOct(YY.ToString());
		for (int i=0; i<NoOfSlots;i++)
			{
			int val= (int)DataLoader.getValAtAddress(PageFile,addptr++,8);
			if (val!=255)
				{
				SlotIndices[k++] = i;
				}
			}
		int NoOfSegs = (int)DataLoader.getValAtAddress(PageFile, addptr++, 8);
		for (int i = 0; i < NoOfSegs; i++)
			{
			string[] AnimFiles = new string[8];
			string AnimName = PrintAnimName(slotbase + SlotIndices[i]);

			int index=TranslateAnimToIndex(slotbase + SlotIndices[i]);
			AnimInfo.animName[index]=AnimName;
			int ValidCount=0;
			for (int j=0; j<8;j++)
				{
				int val=(int)DataLoader.getValAtAddress(PageFile,addptr++,8);
				if (val!=255)
					{					//AnimFiles[j] = "CR" + XX.ToString("d2") + "PAGE_N" + YY.ToString("d2") + "_" + AuxPalNo + "_" + val;

					AnimInfo.animSequence[index,j]= "CR" + XXo + "PAGE_N" + YYo + "_" + AuxPalNo + "_" + (val + spriteIndex);
					AnimInfo.animIndices[index,j]= (val + spriteIndex);
					Debug.Log(AnimInfo.animSequence[index,j] + " at " + index + " " + j + " points to " + (val + spriteIndex));
					ValidCount++;
					}
				}			
			}

		//Read in the palette
		int NoOfPals = (int)DataLoader.getValAtAddress(PageFile, addptr, 8);//Will skip ahead this far.
		addptr++;
		char[] auxPalVal=new char[32];
		for (int i = 0; i < 32; i++)
			{
			auxPalVal[i] =(char)DataLoader.getValAtAddress(PageFile, (addptr)+(AuxPalNo * 32) + i, 8);
			}

		//Skip past the palettes
		addptr = addptr + NoOfPals * 32;
		int NoOfFrames = (int)DataLoader.getValAtAddress(PageFile, addptr, 8);
		//AnimInfo.animSprites=new Sprite[NoOfFrames];
		addptr=addptr+2;
		int addptr_start = addptr;//Bookmark my positiohn
		int MaxWidth=0;
		int MaxHeight=0;
		int MaxHotSpotX=0;
		int MaxHotSpotY=0;
		for (int pass = 0; pass <= 1; pass++)
			{
			addptr=addptr_start;
			if (pass == 0)
				{//get the max width and height
				for (int i = 0; i < NoOfFrames; i++)
					{
					int frameOffset = (int)DataLoader.getValAtAddress(PageFile, addptr + (i * 2), 16);
					//fprintf(LOGFILE,"\n%d @ %d", i, frameOffset);
					int BitMapWidth = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 0, 8);
					int BitMapHeight = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 1, 8);
					int hotspotx = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 2, 8);
					int hotspoty = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 3, 8);
					if (hotspotx>BitMapWidth) 
						{
						hotspotx = BitMapWidth;
						}
					if (hotspoty>BitMapHeight)
						{
						hotspoty = BitMapHeight;
						}

					if (BitMapWidth > MaxWidth)
						{
						MaxWidth = BitMapWidth;
						}
					if (BitMapHeight > MaxHeight)
						{
						MaxHeight = BitMapHeight;
						}

					if (hotspotx > MaxHotSpotX)
						{
						MaxHotSpotX = hotspotx;
						}
					if (hotspoty > MaxHotSpotY)
						{
						MaxHotSpotY = hotspoty;
						}
					}
				}
			else
				{//Extract
				if (MaxHotSpotX * 2 > MaxWidth)
					{//Try and center the hot spot in the image.
					MaxWidth = MaxHotSpotX * 2;
					}
				char[] outputImg;
				outputImg = new char[MaxWidth*MaxHeight*2];
				for (int i = 0; i < NoOfFrames; i++)
					{
					int frameOffset = (int)DataLoader.getValAtAddress(PageFile, addptr + (i * 2), 16);
					int BitMapWidth = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 0, 8);
					int BitMapHeight = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 1, 8);
					int hotspotx = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 2, 8);
					int hotspoty = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 3, 8);
					int compression = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 4, 8);
					int datalen = (int)DataLoader.getValAtAddress(PageFile, frameOffset + 5, 16);

					//Adjust the hotspots from the biggest point back to the image corners
					int cornerX; int cornerY;
					cornerX= MaxHotSpotX-hotspotx;
					cornerY = MaxHotSpotY - hotspoty;
					if (cornerX<=0)
						{
						cornerX = 0;
						}
					else
						{
						cornerX = cornerX - 1;
						}
					if (cornerY<=0)
						{
						cornerY = 0;
						}

					//Extract the image
					char[] srcImg;
					srcImg = new char[BitMapWidth*BitMapHeight*2];
					// Sprite.Create(Image(grBuffer,imageOffset, BitMapWidth, BitMapHeight,"name_goes_here",palLoader.Palettes[pal],true),new Rect(0f,0f,BitMapWidth,BitMapHeight),new Vector2 (0.5f,0.5f));
					ArtLoader.ua_image_decode_rle(PageFile,srcImg, compression == 6 ? 5 : 4, datalen, BitMapWidth*BitMapHeight, frameOffset + 7, auxPalVal);
					Texture2D imgData= ArtLoader.Image(srcImg,0,BitMapWidth,BitMapHeight,"namehere",pal,true);
					AnimInfo.animSprites[spriteIndex+ i]= Sprite.Create(imgData,new Rect(0f,0f,BitMapWidth,BitMapHeight),new Vector2 (0.5f,0.5f));
					spriteCounter++;
					}

				}//endextract
			}
		return spriteCounter;
		}
	



	public static string PrintAnimName(int animNo)
		{
		switch(animNo)
			{
			case 0x0:
			return "idle_combat";
			case 0x1:
			return "attack_bash";
			case 0x2:
			return "attack_slash";
			case 0x3:
			return "attack_thrust";
			case 0x4:
			return "attack_unk4";
			case 0x5:
			return "attack_secondary";
			case 0x6:
			return "attack_unk6";
			case 0x7:
			return "walking_towards";
			case 0xc:
			return "death";
			case 0xd:
			return "begin_combat";
			case 0x20:
			return "idle_rear";
			case 0x21:
			return "idle_rear_right";
			case 0x22:
			return "idle_right";
			case 0x23:
			return "idle_front_right";
			case 0x24:
			return "idle_front";
			case 0x25:
			return "idle_front_left"; 
			case 0x26:
			return "idle_left"; 
			case 0x27:
			return "idle_rear_left"; 
			case 0x80:
			return "walking_rear"; 
			case 0x81:
			return "walking_rear_right";
			case 0x82:
			return "walking_right"; 
			case 0x83:
			return "walking_front_right";
			case 0x84:
			return "walking_front"; 
			case 0x85:
			return "walking_front_left"; 
			case 0x86:
			return "walking_left"; 
			case 0x87:
			return "walking_rear_left"; 
			default:
			return "unknown_anim"; 
			}	
		}

	public static int TranslateIndexToAnim(int animIndex)
		{
		switch (animIndex)
			{
			case 0 :
			return idle_combat;
			case 1 : 
			return attack_bash;
			case 2 : 
			return attack_slash;
			case 3 : 
			return attack_thrust;
			case 4 : 
			return attack_unk4;
			case 5 : 
			return attack_secondary;
			case 6 : 
			return attack_unk6;
			case 7 : 
			return walking_towards;
			case 8 : 
			return death;
			case 9 : 
			return begin_combat;
			case 10 :
			return idle_rear;
			case 11 : 
			return idle_rear_right;
			case 12 : 
			return idle_right;
			case 13 : 
			return idle_front_right;
			case 14 : 
			return idle_front;
			case 15 : 
			return idle_front_left;
			case 16 : 
			return idle_left;
			case 17 : 
			return idle_rear_left;
			case 18 : 
			return walking_rear;
			case 19 : 
			return walking_rear_right;
			case 20 : 
			return walking_right;
			case 21 :
			return walking_front_right;
			case 22 : 
			return walking_front;
			case 23 : 
			return walking_front_left;
			case 24 : 
			return walking_left;
			case 25 : 
			return walking_rear_left;
			default:return 0;
			}
		}
	 

	public static int TranslateAnimToIndex(int animNo)
		{
		switch(animNo)
			{

			case idle_combat : 
			return 0;
			case attack_bash : 
			return 1;
			case attack_slash : 
			return 2;
			case attack_thrust : 
			return 3;
			case attack_unk4 : 
			return 4;
			case attack_secondary : 
			return 5;
			case attack_unk6 : 
			return 6;
			case walking_towards : 
			return 7;
			case death : 
			return 8;
			case begin_combat : 
			return 9;
			case idle_rear : 
			return 10;
			case idle_rear_right : 
			return 11;
			case idle_right : 
			return 12;
			case idle_front_right : 
			return 13;
			case idle_front : 
			return 14;
			case idle_front_left :
			return 15;
			case idle_left : 
			return 16;
			case idle_rear_left : 
			return 17;
			case walking_rear : 
			return 18;
			case walking_rear_right : 
			return 19;
			case walking_right : 
			return 20;
			case walking_front_right : 
			return 21;
			case walking_front : 
			return 22;
			case walking_front_left :
			return 23;
			case walking_left : 
			return 24;
			case walking_rear_left : 
			return 25;
			default:return 0;
			}
		}

	public string DecimalToOct(string data)
		{
		if (data=="0")
			{return "00";}
		string result = string.Empty;
		int rem = 0;

				int num = int.Parse(data);
				while (num > 0)
					{
					rem = num % 8;
					num = num / 8;
					result = rem.ToString() + result;
					}
		if (result.Length==1)
			{
			result  = "0" + result;
			}
		return result;
		}


	public CritterInfo(int critter_id, Palette paletteToUse, int palno,char[] assocData, char[] PGMP, char[] cran)
		{
		int ExtractPageNo = 0;
		string critterIDO = DecimalToOct(critter_id.ToString());
		AnimInfo=new CritterAnimInfo();
		int spriteIndex=0;
		for (int i = 0; i < 8; i++)
			{
			if ((int)DataLoader.getValAtAddress(PGMP, critter_id * 8 + i, 8) != 255)//Checks if a critter exists at this index in the page file.
				{
				string ExtractPageNoO = DecimalToOct (ExtractPageNo.ToString());
				//sprintf_s(fileCrit, 255, "%s\\CR%02o.%02d", CritPath, CritterID, ExtractPageNo);//Create a file name to extract from
				//sprintf_s(OutFileName, 255, "CR%02o_%02d_%d", CritterID, ExtractPageNo, auxPalNo);//Get a name to extract to
				//extractCrittersUW2(fileAssoc, fileCrit, PaletteFile, auxPalNo, 64, UW_GRAPHICS_GR, game, 0, OutFileName, useTGA, ACTUALLY_EXTRACT_FILES);
				string fileCrit= "c:\\games\\uw2\\crit\\CR" + critterIDO + "." + ExtractPageNoO;
				spriteIndex=ReadUW2PageFileData(assocData,palno,fileCrit,AnimInfo,spriteIndex,paletteToUse);
				ExtractPageNo++;
				}
			}
		int cranAdd = (critter_id * 512);
		for (int Animation = 0; Animation<8; Animation++)//The the animation slot
			{
			bool NoAngle=isAnimUnAngled(Animation);
			int NoOfValid=0;

			for (int Angle = 0; Angle<8; Angle++)//Each animation has every possible angle.
				{
				if ((NoAngle==false) || (Angle==4))
					{
					int UW2animIndex= GetUW2Anim(Animation, Angle);
					int animIndex= TranslateAnimToIndex(UW2animIndex);
					AnimInfo.animName[animIndex] = PrintAnimName(UW2animIndex);
					int ValidEntries = (int)DataLoader.getValAtAddress(cran, cranAdd + (Animation * 64) + (Angle * 8) + (7), 8);//Get how many valid frames are in the animation
					for (int FrameNo = 0; FrameNo < 8; FrameNo++)
						{
						int currFrame = (int)DataLoader.getValAtAddress(cran, cranAdd + (Animation * 64) + (Angle * 8) + (FrameNo), 8);
						if (FrameNo<ValidEntries)
							{
							AnimInfo.animIndices[animIndex, FrameNo]=currFrame;
							}
						else
							{
							AnimInfo.animIndices[animIndex,FrameNo]=-1;
							}
						}
					}
				}
			}
		}


	int ReadUW2PageFileData(char[] assocFile, int PaletteNo, string fileCrit, CritterAnimInfo critanim, int spriteIndex, Palette paletteToUse)
		{
		//Debug.Log(fileCrit + " starting at  "  + spriteIndex);
		Palette pal= paletteToUse;
		char[]critterFile;
		//char[] auxpalval=new char[32];
		Palette[] auxpal = new Palette[32];
		int auxPalNo = PaletteNo;
		int AddressPointer;


		//pal = new palette[256];
		//getPalette(PaletteFile, pal, 0);//always palette 0?

		DataLoader.ReadStreamFile(fileCrit,out critterFile);


		//UW2 uses a different method
		//Starting at offset 0x80
		//fprintf(LOGFILE, "\n\t//%s - palette = %d", fileCrit, auxPalNo);
		//auxPalNo=2;
		AddressPointer = 0;//auxPalNo * 32;

		char[] auxPalVal=new char[32];
		for (int j = 0; j < 32; j++)
			{
			auxPalVal[j] =(char)DataLoader.getValAtAddress(critterFile, (AddressPointer)+(AuxPalNo * 32) + j, 8);
			}
		
		int i = 0;
		int MaxWidth = 0;
		int MaxHeight = 0;
		int MaxHotSpotX = 0;
		int MaxHotSpotY = 0;

		for (int pass = 0; pass <= 1;pass++)
			{
			if (pass == 0)
				{//First pass is getting max image sizes
				for (int index = 128; index < 640; index = index + 2)
					{
					int frameOffset = (int)DataLoader.getValAtAddress(critterFile, index, 16);
					if (frameOffset != 0)
						{
						int BitMapWidth = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 0, 8);
						int BitMapHeight = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 1, 8);
						int hotspotx = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 2, 8);
						int hotspoty = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 3, 8);
							if (hotspotx>BitMapWidth)	{hotspotx = BitMapWidth;}
							if (hotspoty>BitMapHeight)	{hotspoty = BitMapHeight;}
							if (BitMapWidth > MaxWidth){MaxWidth = BitMapWidth;}
							if (BitMapHeight > MaxHeight){MaxHeight = BitMapHeight;}
							if (hotspotx > MaxHotSpotX){MaxHotSpotX = hotspotx;}
							if (hotspoty > MaxHotSpotY){MaxHotSpotY = hotspoty;}
						}//End frameoffsetr first pass
					}//End for loop first pass
				}//End first pass
			else
				{//Extract images
				if (MaxHotSpotX * 2 > MaxWidth)
					{//Try and center the hot spot in the image.
					MaxWidth = MaxHotSpotX * 2;
					}
				char[] outputImg;
				outputImg = new char[MaxWidth*MaxHeight * 2];
				for (int index = 128; index < 640; index = index + 2)
					{
					int frameOffset = (int)DataLoader.getValAtAddress(critterFile, index, 16);
					if (frameOffset != 0)
						{
						int BitMapWidth = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 0, 8);
						int BitMapHeight = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 1, 8);
						int hotspotx = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 2, 8);
						int hotspoty = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 3, 8);
						int compression = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 4, 8);
						int datalen = (int)DataLoader.getValAtAddress(critterFile, frameOffset + 5, 16);
						//Adjust the hotspots from the biggest point back to the image corners
						int cornerX; int cornerY;
						cornerX = MaxHotSpotX - hotspotx;
						cornerY = MaxHotSpotY - hotspoty;
							if (cornerX <= 0){cornerX = 0;}
							else{cornerX = cornerX - 1;}
							if (cornerY <= 0){cornerY = 0;}

						if (true)
							{
							//Merge the image into a new big image at the hotspot coordinates.;
							char[] srcImg;

							srcImg = new char[BitMapWidth*BitMapHeight * 2];
							ArtLoader.ua_image_decode_rle(critterFile, srcImg, compression == 6 ? 5 : 4, datalen, BitMapWidth*BitMapHeight, frameOffset + 7, auxPalVal);
							cornerY = MaxHeight - cornerY;//y is from the top left corner

							int ColCounter = 0; int RowCounter = 0;
							bool ImgStarted = false;
							for (int y = 0; y < MaxHeight; y++)
								{
								for (int x = 0; x < MaxWidth; x++)
									{
									if ((cornerX + ColCounter == x) && (MaxHeight - cornerY + RowCounter == y) && (ColCounter<BitMapWidth) && (RowCounter<BitMapHeight))
										{//the pixel from the source image is here 
										ImgStarted = true;
										outputImg[x + (y*MaxWidth)] = srcImg[ColCounter + (RowCounter*BitMapWidth)];
										ColCounter++;
										}
									else
										{
										outputImg[x + (y*MaxWidth)] = (char)0;//alpha
										}
									}
								if (ImgStarted == true)
									{//New Row on the src image
									RowCounter++;
									ColCounter = 0;
									}
								}
							//Set the heights for output
							BitMapWidth = MaxWidth;
							BitMapHeight = MaxHeight;

							//Debug.Log(fileCrit + " "  + spriteIndex);

							Texture2D imgData= ArtLoader.Image(outputImg,0,BitMapWidth,BitMapHeight,"namehere",pal,true);
							AnimInfo.animSprites[spriteIndex++]= Sprite.Create(imgData,new Rect(0f,0f,BitMapWidth,BitMapHeight),new Vector2 (0.5f,0.5f));
							}
						}//end extrac frameoffset
					}//End for loop extract
				}//End extract images
			
			}
		//Debug.Log(fileCrit + " returning  "  + spriteIndex);
		return spriteIndex;


		}


	/// <summary>
	/// Ises the animation unangled.
	/// </summary>
	/// <returns><c>true</c>, if animation unangled was ised, <c>false</c> otherwise.</returns>
	/// <param name="animationNo">Animation no.</param>
	bool isAnimUnAngled(int animationNo)
		{
		switch (animationNo)
			{
			case 0x2:
			case 0x3: 
			case 0x4:
			case 0x5: 
			case 0x6:
			case 0x7:
			case 0xd: 
				return true; 
			default:
				return false;
			}	
		}

	/// <summary>
	/// Translates a uw2 animation index into a uw1 version
	/// </summary>
	/// <returns>The U w2 animation.</returns>
	/// <param name="animation">Animation.</param>
	/// <param name="angle">Angle.</param>
	int GetUW2Anim(int animation, int angle)
		{
			{
			switch (animation)
				{
				case 0x0://idlecombat
					{
					return 0x20+angle;
					}
				case 0x1://walking
					{
					return 0x80+angle;
					}
				case 0x2://Attacks
				case 0x3:
				case 0x4:
					return animation -1;
				case 0x5://secondary attack
				case 0x6://unknown attack
					return animation;
				case 0x7://death
					return 0xc;
				default:
					return animation;
				}
			}
		}

}
