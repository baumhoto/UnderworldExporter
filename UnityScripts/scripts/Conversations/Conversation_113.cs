﻿using UnityEngine;
using System.Collections;

public class Conversation_113 : Conversation {
	
	//conversation #113
	//String block 0x0e71, name head bandit
	
	
	public override IEnumerator main() {
		SetupConversation (3697);
		privateVariables[1] = 0;
		yield return StartCoroutine(func_029d());
		func_0012();
		yield return 0;
	} // end func
	
	void func_0012() {
		EndConversation();
		privateVariables[0] = 1;
	} // end func
	
	/*void func_0020() {
		
		int locals[1];
		
		if ( (((npc.npc_goal == 5 || npc.npc_goal == 6) || npc.npc_goal == 9) && npc.npc_gtarg == 1 || npc.npc_attitude == 0) ) {
			
			locals[1] = 0;
		} else {
			
			locals[1] = 1;
		} // end if
		
		return locals[1];
	} // end func*/
	
	/*void func_0063() {
		
		npc.npc_gtarg = 1;
		npc.npc_attitude = 1;
		npc.npc_goal = 6;
		func_0012();
	} // end func*/
	
	/*void func_007c() {
		
		npc.npc_goal = 1;
		func_0012();
	} // end func*/
	
	void func_008b() {
		
		npc.npc_gtarg = 1;
		npc.npc_goal = 5;
		npc.npc_attitude = 1;
		func_0012();
	} // end func
	
	/*void func_00a4() {
		
		npc.npc_attitude = 6;
	} // end func*/
	
	void func_00b1(int param1) {
		
		npc.npc_attitude = param1;//param1[0]play_hunger;
		func_0012();
	} // end func
	
	/*void func_00c2() {
		
		npc.npc_attitude = 2;
		func_0012();
	} // end func*/
	
	/*void func_00d1() {
		
		npc.npc_attitude = 1;
		func_0012();
	} // end func*/
	
	/*void func_00e0() {
		
		func_0012();
	} // end func*/
	
	/*void func_00ea() {
		
		param1[1] = game_days;
		param1[2] = game_mins;
	} // end func*/
	
	/*void func_0106() {
		
		int locals[4];
		
		locals[2] = game_days - param2[1];
		locals[3] = game_mins - param2[2];
		if ( locals[3] < 0 ) {
			
			locals[3] = locals[3] + 1440;
			locals[2] = locals[2] - 1;
		} // end if
		
		if ( locals[2] >= param1[1] && locals[3] >= param1[2] ) {
			
			locals[1] = 1;
		} else {
			
			locals[1] = 0;
		} // end if
		
		return locals[1];
	} // end func*/
	
	/*void func_018f() {
		
		param1[1] = game_days - param3[1];
		param1[2] = game_mins - param3[2];
		if ( param1[2] < 0 ) {
			
			param1[2] = param1[2] + 1440;
			param1[1] = param1[1] - 1;
		} // end if
		
		param1[1] = param2[1] - param1[1];
		param1[2] = param2[2] - param1[2];
		if ( param1[2] < 0 ) {
			
			param1[2] = param1[2] + 1440;
			param1[1] = param1[1] - 1;
		} // end if
		
	} // end func*/
	
	/*void func_0243() {
		
		param1[1] = game_days - param2[1];
		param1[2] = game_mins - param2[2];
		if ( param1[2] < 0 ) {
			
			param1[2] = param1[2] + 1440;
			param1[1] = param1[1] - 1;
		} // end if
		
	} // end func*/
	
	IEnumerator func_029d() {
		
		//int locals[89];
		int[] locals =new int[90];
		
		if ( npc.npc_attitude != 3 ) {
			
			if ( privateVariables[0] == 0 ) {
				
				yield return StartCoroutine(say( "What art thou doing here?  " ));
			} else {
				
				yield return StartCoroutine(say( "What art thou doing here again?  " ));
			} // end if
			
			yield return StartCoroutine(say( "Leave now before I call the guards." ));
			locals[2] = 4;
			locals[3] = 5;
			locals[4] = 6;
			locals[5] = 0;
			//locals[23] = babl_menu( 0, locals[2] );
			yield return StartCoroutine(babl_menu (0,locals,2));
			locals[23] = PlayerAnswer;
			switch ( locals[23] ) {
				
			case 1:
				Time.timeScale =SlomoTime;
				yield return new WaitForSeconds(WaitTime);
				func_00b1( 21 );
				yield break;
				break;
				
			case 2:
				
				yield return StartCoroutine(func_069a());
				break;
				
			case 3:
				
				yield return StartCoroutine(func_0401());
				break;
			} // end if
			
		} else {
			
			
			
			if ( privateVariables[0] == 0 ) {
				
				yield return StartCoroutine(say( "Hullo, @GS8.  I have been told of thee.  What is it thou doth need?" ));
			} else {
				
				yield return StartCoroutine(say( "Hello again, @GS8.  What can I do for thee?" ));
			} // end if
			
			//locals[1] = !privateVariables[0];
			if (privateVariables[0]==1)
			{
				locals[1]=0;
			}
			else
			{
				locals[1]=1;
			}
			locals[45] = 1;
			locals[24] = 9;
			locals[46] = 1;
			locals[25] = 10;
			locals[47] = locals[1];
			locals[26] = 11;
			locals[48] = privateVariables[0];
			locals[27] = 12;
			locals[28] = 0;
			//locals[66] = babl_fmenu( 0, locals[24], locals[45] );
			yield return StartCoroutine (babl_fmenu(0,locals,24,45));
			locals[66]=PlayerAnswer;
			switch ( locals[66] ) {
				
			case 9:
				
				locals[67] = 2;
				Time.timeScale =SlomoTime;
				yield return new WaitForSeconds(WaitTime);
				func_00b1( locals[67] );
				yield break;
				break;
				
			case 10:
				
				yield return StartCoroutine(func_04b5());
				break;
				
			case 11:
				
				break;
				
			case 12:
				
				break;
				
			} // end switch
			
			yield return StartCoroutine(say( "Which can I do for thee now, @GS8?" ));
			locals[68] = 14;
			locals[69] = 15;
			locals[70] = 0;
			//locals[89] = babl_menu( 0, locals[68] );
			yield return StartCoroutine(babl_menu (0,locals,68));
			locals[89] = PlayerAnswer;
			switch ( locals[89] ) {
				
			case 1:
				
				yield return StartCoroutine(func_0514());
				break;
				
			case 2:
				
				yield return StartCoroutine(func_06c7());
				break;
				
			} // end switch
			Time.timeScale =SlomoTime;
			yield return new WaitForSeconds(WaitTime);
			func_00b1( 21 );
			yield break;
		} // end switch
	} // end func
	
	IEnumerator func_0401() {
		
		//int locals[44];
		int[] locals =new int[45];
		
		yield return StartCoroutine(say( "I have nothing to discuss.  Please deal with my underlings first before thou dost continue to bother me.  Go now." ));
		locals[1] = 17;
		locals[2] = 18;
		locals[3] = 19;
		locals[4] = 0;
		//locals[22] = babl_menu( 0, locals[1] );
		yield return StartCoroutine(babl_menu (0,locals,1));
		locals[22] = PlayerAnswer;
		switch ( locals[22] ) {
			
		case 1:
			Time.timeScale =SlomoTime;
			yield return new WaitForSeconds(WaitTime);
			func_00b1( 21 );
			yield break;
			break;
			
		case 2:
			
			break;
			
		case 3:
			
			yield return StartCoroutine(func_069a());
			break;
			
		} // end switch
		
		yield return StartCoroutine(say( "Thou hast bothered me too much already.  Leave!" ));
		locals[23] = 21;
		locals[24] = 22;
		locals[25] = 23;
		locals[26] = 0;
		//locals[44] = babl_menu( 0, locals[23] );
		yield return StartCoroutine(babl_menu (0,locals,23));
		locals[44] = PlayerAnswer;
		switch ( locals[44] ) {
			
		case 1:
			
			func_00b1( 21 );
			break;
			
		case 2:
			
			yield return StartCoroutine(func_069a());
			break;
			
		case 3:
			
			yield return StartCoroutine(func_069a());
			break;
			
		} // end switch
		
	} // end func
	
	IEnumerator func_04b5() {
		
		//int locals[22];
		int[] locals =new int[23];
		
		yield return StartCoroutine(say( "Do not come here then!  We guard ours to the death!  Leave!" ));
		locals[1] = 25;
		locals[2] = 26;
		locals[3] = 27;
		locals[4] = 0;
		//locals[22] = babl_menu( 0, locals[1] );
		yield return StartCoroutine(babl_menu (0,locals,1));
		locals[22] = PlayerAnswer;
		switch ( locals[22] ) {
			
		case 1:
			
			yield return StartCoroutine(func_069a());
			break;
			
		case 2:
			
			yield return StartCoroutine(func_069a());
			break;
			
		case 3:
			Time.timeScale =SlomoTime;
			yield return new WaitForSeconds(WaitTime);
			func_00b1( 21 );
			yield break;
			break;
			
		} // end switch
		
	} // end func
	
	IEnumerator func_0514() {
		
		//int locals[36];
		int[] locals =new int[37];
		
		yield return StartCoroutine(say( "What information dost thou seek?" ));
	label_051c:;
		
		locals[3] = 29;
		locals[4] = 30;
		locals[5] = 0;
		//locals[24] = babl_menu( 0, locals[3] );
		yield return StartCoroutine(babl_menu (0,locals,3));
		locals[24] = PlayerAnswer;
		switch ( locals[24] ) {
			
		case 1:
			
			break;
			
		case 2:
			Time.timeScale =SlomoTime;
			yield return new WaitForSeconds(WaitTime);
			func_00b1( 21 );
			yield break;
			break;
			
		} // end switch
		
		//locals[2] = babl_ask( 0 );
		yield return StartCoroutine( babl_ask( 0 ));
		locals[25] = 31;
		if ( contains( 2, PlayerTypedAnswer, locals[25] ) ==1 ) {
			
			yield return StartCoroutine(say( "He is kept prisoner by the Green Lizardmen." ));
		} else {
			
			locals[26] = 33;
			//;  // expr. has value on stack!
			locals[27] = 34;
			locals[28] = 35;
			if (  
			    (contains( 2, PlayerTypedAnswer, locals[26] )  == 1) 
			    || (contains( 2, PlayerTypedAnswer, locals[27]) == 1)
			    || (contains( 2, PlayerTypedAnswer, locals[28]) == 1)
			    ) 
			{
				
				yield return StartCoroutine(say( "All I've heard is that the Gray Lizardmen know more." ));
			} else {
				
				locals[29] = 37;
				locals[30] = 38;
				//;  // expr. has value on stack!
				if  ((contains( 2, PlayerTypedAnswer, locals[29] ) == 1)|| (contains( 2, PlayerTypedAnswer, locals[30] )==1) ) {
					
					yield return StartCoroutine(say( "The crazy one to the north has stolen many types of lights, including our candles!" ));
				} else {
					
					locals[31] = 40;
					locals[32] = 41;
					//;  // expr. has value on stack!
					if (
						(contains( 2, PlayerTypedAnswer, locals[31])==1)
						|| (contains( 2, PlayerTypedAnswer, locals[32] )==1)
						) {
						
						yield return StartCoroutine(say( "A great one, was Sir Cabirus.  After he died, things all went haywire. The races of the Abyss split off into many small groups like our own.  Dealing with the others can be a touchy task. " ));
					} else {
						
						locals[33] = 43;
						if ( contains( 2, PlayerTypedAnswer, locals[33] ) ==1) {
							
							yield return StartCoroutine(say( "The Eight Talismans?  I do not know much, save the Gray Lizardmen know all about the Sword of Justice." ));
						} else {
							
							locals[34] = 45;
							if ( contains( 2, PlayerTypedAnswer, locals[34] ) ==1) {
								
								yield return StartCoroutine(say( "They are the rulers of this level.  There are Green ones, Red ones, and the mysterious Gray Ones, who are called the Quiet Ones.  They even speak a little of our tongue." ));
							} else {
								
								locals[1] = random( 1, locals[35] );
								locals[35] = 4;
								locals[36] = locals[1];
								if ( 1 == locals[36] ) {
									
									yield return StartCoroutine(say( "I am sorry I cannot help thee there." ));
								} else {
									
									if ( 2 == locals[36] ) {
										
										yield return StartCoroutine(say( "What does this mean, '@SS2'?" ));
									} else {
										
										if ( 3 == locals[36] ) {
											
											yield return StartCoroutine(say( "I do not know." ));
										} else {
											
											if ( 4 == locals[36] ) {
												
												yield return StartCoroutine(say( "Sorry.  I don't understand." ));
											} // end if
											
										} // end if
										
									} // end if
									
								} // end if
								
							} // end if
							
						} // end if
						
					} // end if
					
				} // end if
				
			} // end if
			
		} // end if
		
		yield return StartCoroutine(say( "Can I tell thee anything else?" ));
		goto label_051c;
		
	} // end func
	
	IEnumerator func_069a() {
		
		//int locals[3];
		int[] locals =new int[4];
		
		yield return StartCoroutine(say( "Defend thyself!" ));
		locals[1] = 13;
		locals[2] = 0;
		locals[3] = 10;
		set_race_attitude( 3, locals[3], locals[2], locals[1] );
		Time.timeScale =SlomoTime;
		yield return new WaitForSeconds(WaitTime);
		func_008b();
		yield break;
	} // end func
	
	IEnumerator func_06c7() {
		
		//int locals[44];
		int[] locals =new int[45];
		
		setup_to_barter( 0 );
		while ( privateVariables[1]  == 0) {
			
			locals[1] = 53;
			locals[2] = 54;
			locals[3] = 55;
			locals[4] = 56;
			locals[5] = 0;
			//locals[22] = babl_menu( 0, locals[1] );
			yield return StartCoroutine(babl_menu (0,locals,1));
			locals[22] = PlayerAnswer;
			switch ( locals[22] ) {
				
			case 1:
				
				yield return StartCoroutine(func_0778());
				break;
				
			case 2:
				
				yield return StartCoroutine(func_07d2());
				break;
				
			case 3:
				
				yield return StartCoroutine(do_judgement( 0 ));
				break;
				
			case 4:
				
				do_decline( 0 );
				privateVariables[1] = 1;
				break;
				
			} // end switch
			
		} // while
		
		locals[23] = 57;
		locals[24] = 0;
		//locals[44] = babl_menu( 0, locals[23] );
		yield return StartCoroutine(babl_menu (0,locals,23));
		locals[44] = PlayerAnswer;
		if ( locals[44] == 1 ) {
			
			privateVariables[1] = 1;
		} // end if
		
	} // end func
	
	IEnumerator func_0778() {
		
		//int locals[15];
		int[] locals =new int[16];
		
		locals[0] = -1;
		locals[6] = -1;
		locals[11] = 58;
		locals[12] = 59;
		locals[13] = 60;
		locals[14] = 61;
		locals[15] = 62;
		//if ( do_offer( 7, locals[15], locals[14], locals[13], locals[12], locals[11], locals[6], locals[1] ) ) {
		yield return StartCoroutine (do_offer( 5, locals[15], locals[14], locals[13], locals[12], locals[11], locals[6],locals[1]) );
		if (PlayerAnswer==1)	{
			
			privateVariables[1] = 1;
		} // end if
		
	} // end func
	
	IEnumerator func_07d2() {
		
		//int locals[24];
		int[] locals =new int[25];
		
		yield return StartCoroutine(say( "Dost thou intend to rob me?" ));
		locals[1] = 64;
		locals[2] = 65;
		locals[3] = 0;
		//locals[22] = babl_menu( 0, locals[1] );
		yield return StartCoroutine(babl_menu (0,locals,1));
		locals[22] = PlayerAnswer;
		switch ( locals[22] ) {
			
		case 1:
			
			break;
			
		case 2:
			yield break;
			//return;
			
			break;
			
		} // end switch
		
		locals[23] = 66;
		locals[24] = 67;
		//if ( do_demand( 2, locals[24], locals[23] ) ) {
		yield return StartCoroutine (do_demand( 2, locals[24], locals[23] ));
		if (PlayerAnswer==1){
			
			privateVariables[1] = 1;
		} else {
			
			Time.timeScale =SlomoTime;
			yield return new WaitForSeconds(WaitTime);
			func_008b();
			yield break;
		} // end if
		
	} // end func
	
	
	
}