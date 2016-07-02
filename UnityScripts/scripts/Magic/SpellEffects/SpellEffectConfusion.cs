﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Makes the NPC confused
/// </summary>
public class SpellEffectConfusion : SpellEffect {

		/// Backup the original state of the Npc
		public int OriginalState;
		/// Backup the original attitude of the npc
		public int OriginalAttitude;
		public NPC npc;
		public bool WasActive;

		/// <summary>
		/// Applies the effect.
		/// </summary>
		/// Sets the NPC attitude and states
		/// Does not apply if allied or afraid
		public override void ApplyEffect ()
		{
			if ((this.GetComponent<SpellEffectAlly>()==null) && (this.GetComponent<SpellEffectFear>()==null))
			{//Only one or the other.
				npc=this.GetComponent<NPC>();
				if (npc!=null)
				{
					OriginalState= npc.state;
					OriginalAttitude=npc.npc_attitude;
					npc.state=NPC.AI_STATE_IDLERANDOM;	//Temporarily just wander around
					npc.npc_attitude=NPC.AI_ATTITUDE_UPSET;
					WasActive=true;
				}	
			}
			base.ApplyEffect();
		}

		public override void CancelEffect ()
		{
			if(WasActive==true)
				{
					npc.state=OriginalState;
					npc.npc_attitude=OriginalAttitude;		
				}
			base.CancelEffect ();
		}
}
