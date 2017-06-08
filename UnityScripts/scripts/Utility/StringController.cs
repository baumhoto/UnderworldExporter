﻿using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

/// <summary>
/// Class for holding game strings and returning formatted strings and text
/// </summary>
public class StringController : UWEBase {

	public static int YouSee=260;

	/// <summary>
	/// The game strings are stored in this hashtable.
	/// </summary>
	/// Hash is in format [block number]_[string number]
	private Hashtable GameStrings;
	private Hashtable EntryCounts;


		/// <summary>
		/// Huffman node structure for decoding strings.pak
		/// </summary>
		struct huffman_node
		{
				public char symbol; // symbol in node
				public int parent; //
				public int left;   // -1 when no node
				public int right;  // 
		};

		struct block_dir
		{
				public long block_no;
				public long address;
				public long NoOfEntries;
				//public long blockEnd;
		} ;

		//public string Path;
	

		/// <summary>
		/// The instance of this class
		/// </summary>
	public static StringController instance;

		void Awake()
		{
			instance=this;
				//Set some default strings that differ between games.
				switch (_RES)
				{
				case GAME_UW2:
						YouSee=276;
						break;
				default:
						YouSee=260;
						break;
				}


				/*if(UWEBase._RES!="UW1")
				{					
					//InitStringController(Application.dataPath + "//..//" + UWEBase._RES + "_strings.txt");
				}
				else
				{
					//use the new method
					//LoadStringsPak(Path);
				}*/
		}



		/// <summary>
		/// Inits the string controller.
		/// </summary>
		/// <returns><c>true</c>, if string controller was inited, <c>false</c> otherwise.</returns>
		/// <param name="StringFilePath">String file path.</param>
	public bool InitStringController(string StringFilePath)
	{
		GameStrings=new Hashtable();
		EntryCounts=new Hashtable();
		return Load(StringFilePath);
	}


		/// <summary>
		/// Loads and decodes the strings.pak file as specificed by the Path.
		/// </summary>
		/// <param name="path">Path.</param>
		public void LoadStringsPak(string path)
		{
				string Result="";
				long address_pointer=0;
				huffman_node[] hman;
				block_dir[] blocks;
				char[] Buffer;
				string Key="";
				GameStrings=new Hashtable();
				EntryCounts=new Hashtable();

				if (DataLoader.ReadStreamFile(path,out Buffer))
				{
						long NoOfNodes=DataLoader.getValAtAddress(Buffer,address_pointer,16);
						int i=0;
						hman = new huffman_node [NoOfNodes];
						address_pointer=address_pointer+2;
						while (i<NoOfNodes)
						{
								hman[i].symbol= System.Convert.ToChar(Buffer[address_pointer+0]);
								hman[i].parent=Buffer[address_pointer+1];
								hman[i].left= Buffer[address_pointer+2];
								hman[i].right= Buffer[address_pointer+3];
								i++;
								address_pointer=address_pointer+4;
						}

						long NoOfStringBlocks=DataLoader.getValAtAddress(Buffer,address_pointer,16);
						blocks=new block_dir[NoOfStringBlocks];
						address_pointer=address_pointer+2;
						i=0;
						while (i<NoOfStringBlocks)
						{
								blocks[i].block_no = DataLoader.getValAtAddress(Buffer,address_pointer,16);
								address_pointer=address_pointer+2;
								blocks[i].address = DataLoader.getValAtAddress(Buffer,address_pointer,32);	
								address_pointer=address_pointer+4;
								blocks[i].NoOfEntries = DataLoader.getValAtAddress(Buffer,blocks[i].address,16);	//look ahead and get no of entries.
								EntryCounts["_" + blocks[i].block_no]=blocks[i].NoOfEntries.ToString();
								i++;
						}
						i=0;
						//
						int Iteration=0;
						while (i<NoOfStringBlocks)
						{
								address_pointer=2 + blocks[i].address + blocks[i].NoOfEntries *2;
								int blnFnd;
								for (int j=0;j< blocks[i].NoOfEntries;j++)
								{
										//Based on abysmal /uwadv implementations.
										blnFnd=0;
										//char c;

										int bit = 0;
										int raw = 0;
										long node=0;

										do {
												node = NoOfNodes - 1; // starting node

												// huffman tree decode loop
												//This was -1 in the original code!
												while (hman[node].left != 255
														&& hman[node].right != 255)
												{

														if (bit == 0) {
																bit = 8;
																raw = Buffer[address_pointer++];	//stream.get<uint8_t>();
														}
														Iteration++;
														//Debug.Log("raw=" + raw + "=" + (raw & 0x80));
														// decide which node is next
														//node = raw & 0x80 ? hman[node].right
														//	: hman[node].left;
														if ((raw & 0x80) ==128)
														{
																node=hman[node].right;
														}
														else
														{
																node=hman[node].left;
														}

														raw <<= 1;
														bit--;
												}

												// have a new symbol
												if ((hman[node].symbol !='|')){
														if (blnFnd==0)
														{
																Key= blocks[i].block_no.ToString("000")+"_"+j.ToString("000") ;
														}						
														Result+=hman[node].symbol;	
														blnFnd = 1;
												}
												else
												{
														if ((Result.Length>0) && (Key.Length>0))
														{
																GameStrings[Key]=Result;
																Result="";
																Key="";
														}
												}
										} while (hman[node].symbol != '|');		
								}
								i++;
						}
						if ((Result.Length>0) && (Key.Length>0))
						{//I still have the very last value to keep.
								GameStrings[Key]=Result;
								Result="";
						}
				}	

		}



	/// <summary>
	/// Gets the string at the specified numbers
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="BlockNo">Block no.</param>
	/// <param name="StringNo">String no.</param>
	public string GetString(int BlockNo, int StringNo)
	{//output a string at the specified block and string no.
				string result= (string)GameStrings[BlockNo.ToString("000") + "_" + StringNo.ToString("000")];
				if (result!=null)
				{
						return result;
				}
				else
				{
						return "";
				}
	}


	/// <summary>
	/// Gets the object noun for a passed object
	/// </summary>
	/// <returns>The object noun U.</returns>
	/// <param name="objInt">Object int.</param>
	public string GetObjectNounUW(ObjectInteraction objInt)
	{//The single noun	
		return GetObjectNounUW(objInt.item_id);
	}

	/// <summary>
	/// Gets the object noun for the specified item id.
	/// </summary>
	/// <returns>The object noun U.</returns>
	/// <param name="item_id">Item identifier.</param>
	public string GetObjectNounUW(int item_id)
	{		
		string output = GetString (4,item_id);
		if (output.Contains("&"))
		{
			output= output.Split ('&')[0];
		}			
		//Remove the article
		output =output.Replace("a_","");
		output =output.Replace("an_","");
		output =output.Replace("some_","");
		return output;
	}


		/// <summary>
		/// Gets the formatted object name for the specified object. Takes into account quantity.
		/// </summary>
		/// <returns>The formatted object name U.</returns>
		/// <param name="objInt">Object int.</param>
	public string GetFormattedObjectNameUW(ObjectInteraction objInt)
	{//Eventually this will return things like proper quants etc.
		string output = GetString (4,objInt.item_id);

		if ((objInt.isQuant() ==true) && (objInt.isEnchanted()==false))
		{
			if (output.Contains("&"))
			{
				if (objInt.link>1)
				{//Plural description
					output= objInt.link + " " + output.Split ('&')[1];		
				}
				else
				{
					output= output.Split ('&')[0];
				}
			}
		else
			{
				if (objInt.link>1)
				{//Plurals
					output = output.Replace("a_",objInt.link + "_");
					output = output.Replace("an_",objInt.link + "_");
					output = output.Replace("some_",objInt.link + "_");
					output = output + "s";
				}
			}		
		}
		else
			{
			if (output.Contains("&"))
			    {
				output= output.Split ('&')[0];
				}				
			}
		output =output.Replace("_"," ");
		return GetString(1,YouSee) + output;
	}


	/// <summary>
	/// Gets the formatted object name for the passed object and a specified quantity.
	/// </summary>
	/// <returns>The formatted object name U.</returns>
	/// <param name="objInt">Object int.</param>
	/// <param name="Quantity">Quantity that I want to display</param>
	public string GetFormattedObjectNameUW(ObjectInteraction objInt,int Quantity)
	{//Eventually this will return things like proper quants etc.

		string output = GetString (4,objInt.item_id);
		
		if ((objInt.isQuant() ==true) && (objInt.isEnchanted()==false))
		{
			if (output.Contains("&"))
			{//string is split into a singular and plural
				if ((objInt.link>1) && (Quantity>1))
				{//Plural description
					output= objInt.link + " " + output.Split ('&')[1];		
				}
				else
				{
					output= output.Split ('&')[0];
				}
			}
			else
			{
				output = output.Replace("a_",Quantity + "_");
				output = output.Replace("an_",Quantity + "_");
				output = output.Replace("some_",Quantity + "_");
			}
		}
		else
		{
			if (output.Contains("&"))
			{
				output= output.Split ('&')[0];
			}			
		}		
		
		output =output.Replace("_"," ");
		return GetString(1,YouSee) + output;

	}

	/// <summary>
	/// Gets the formatted object name with a description of its qualty. (EG smell fish.)
	/// </summary>
	/// <returns>The formatted object name U.</returns>
	/// <param name="objInt">Object int.</param>
	/// <param name="QualityString">Quality string.</param>
	public string GetFormattedObjectNameUW(ObjectInteraction objInt, string QualityString)
	{//Eventually this will return things like proper quants etc.
		string output = GetString (4,objInt.item_id);
		if (output==null)
		{
			output="";
		}
		if ((objInt.isQuant() ==true) && (output.Contains("&")) && (objInt.isEnchanted()==false) )
		{
			if (objInt.link>1)
			{//Plural description
				output= objInt.link + " " + output.Split ('&')[1];		
			}
			else
			{
				output= output.Split ('&')[0];
			}
		}
		else
		{
			if (output.Contains("&"))
			{
				output= output.Split ('&')[0];
			}
		}

		string isThisAVowel=QualityString.Substring(0,1).ToUpper();
		if (
			(isThisAVowel == "A")
			||
			(isThisAVowel == "E")
			||
			(isThisAVowel == "I")			
			||
			(isThisAVowel == "O")			
			||
			(isThisAVowel == "U")
			)
		{
			if (output.Contains("a_"))
			{
				output = output.Replace("a_","an " + QualityString + " ");
			}
			else
			{
				if (output.Contains("_"))
				{
					output =output.Replace("_", " " + QualityString + " ");	
				}
				else
				{
					output = QualityString + " " + output + " ";	
				}				
			}
		}
		else
		{
			if (output.Contains("an_"))
			{
				output = output.Replace("an_","a " +QualityString + " ");					
			}
			else
			{
				if (output.Contains("_"))
				{
					output =output.Replace("_", " " + QualityString + " ");
				}
				else
				{
					output = QualityString + " " + output + " ";		
				}			
			}			
		}
		//output =output.Replace("_", " " + QualityString + " ");
		return GetString(1,YouSee) + output;
	}

	//// <summary>
	/// Gets the simple object name without quantity
	/// </summary>
	/// <returns>The simple object name U.</returns>
	/// <param name="item_id">Item identifier.</param>
	
	public string GetSimpleObjectNameUW(int item_id)
	{//Without quants.
		string output = GetString (4,item_id);
		if (output==null)
		{
				return "";
		}
		if (output.Contains("&"))
		{
			output= output.Split ('&')[0];
		}

		return (output.Replace ("_"," "));
	}

	/// <summary>
	/// Gets the simple object name for the pass object.
	/// </summary>
	/// <returns>The simple object name U.</returns>
	/// <param name="objInt">Object int.</param>
	public string GetSimpleObjectNameUW(ObjectInteraction objInt)
	{//Without quants.
		return GetSimpleObjectNameUW(objInt.item_id);
	}

	/// <summary>
	/// Gets the description for the texture looked at.
	/// </summary>
	/// <returns>The description.</returns>
	/// <param name="index">Index.</param>
	public string TextureDescription(int index)
	{//TODO:fix floor and wall naming
		return (GetString(1,YouSee)  + GetString (10,index));
	}


		/// <summary>
		/// Load the strings in from an external txt file. Parses the values into the hashtable.
		/// </summary>
		/// <param name="fileName">File name.</param>
	private bool Load(string fileName)
	{
		string line;
		StreamReader fileReader = new StreamReader(fileName, Encoding.Default);
		string PreviousKey="";
		string PreviousValue="";
		using (fileReader)
		{
			// While there's lines left in the text file, do this:
			do
			{
				line = fileReader.ReadLine();
				if (line != null)
				{
					if (line.IndexOf("~")>=0)
					{
						string[] entries = line.Split('~');
						if (entries.Length > 0)
						{
							GameStrings[entries[1] + "_" + entries[2]] = entries[3];
							PreviousValue=entries[3];
							PreviousKey=entries[1] + "_" + entries[2];

						}
					}
				else
					{//possible new line character. Append text to previous entry.
						GameStrings[PreviousKey]=PreviousValue + "\n" + line;		
						PreviousValue= PreviousValue + "\n" + line;	
						//Debug.Log (PreviousKey+"="+PreviousValue);
					}
				}
			}
			while (line != null);
			fileReader.Close();
			return true;
		}
	}


	/// <summary>
	/// Gets the name of the texture of the wall/floor looked at.
	/// </summary>
	/// <returns>The texture name.</returns>
	/// <param name="index">Index.</param>
	public string GetTextureName(int index)
	{
		switch (_RES)
		{
		case GAME_UW2:

				return GetString(10,index);//There is something odd here. Some textures in the file don't match this

		default:
			{
				if (index<210)
				{//Return a wall texture.
					return GetString(10,index);
				}
				else
				{//return a floor texture in reverse order.
					return GetString(10, 510-index+210);
				}
			}
		}
	}

		/// <summary>
		/// Adds a string to string block and return a memory location for it.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="BlockNo">Block no.</param>
		/// <param name="NewString">New string.</param>
	public int AddString(int BlockNo, string NewString)
	{
		string NoOfEntries = (string)EntryCounts["_" + BlockNo];
		int Count = int.Parse(NoOfEntries);
		Count++;
		GameStrings[BlockNo.ToString("000") + "_" + Count.ToString("000")] = NewString;	
		EntryCounts["_" + BlockNo]= Count.ToString();

		Debug.Log("New string :" + GameStrings[BlockNo.ToString("000") + "_" + Count.ToString("000")] );
		return Count;
	}
	
}