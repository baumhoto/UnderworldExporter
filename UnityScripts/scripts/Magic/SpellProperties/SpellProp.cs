﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Properties for spells. Mainly used for projectile spells.
/// </summary>
public class SpellProp  {

	///How much damage or the base value the spell applies.
	public int BaseDamage;
	///How long the spell effect lasts
	public int counter;
	///What damage over time the spell effect has.
	public int DOT;
	///How many interations
	public int noOfCasts=1;  
	///How the spell projectile is spread in a cone.
	public float spread;
	///What force is applied to the projectile.
	public float Force; 
	///What sprite is displayed
	public string ProjectileSprite;
	///What impact image is played on a miss.
	public int impactFrameStart;
	///What impact image is played on a miss.
	public int impactFrameEnd;
	public static UWCharacter playerUW;


		/// <summary>
		/// Init the specified effectId.
		/// </summary>
		/// <param name="effectId">Effect identifier.</param>
	public virtual void init(int effectId)
	{
		//Set spell variables
		//Init the spelleffect applied by the spell.
	}

		/// <summary>
		/// Code to run when it hits anything. eg for explosions	
		/// </summary>
		/// <param name="tf">The transform of the gameobject it hits</param>
	public virtual void onImpact(Transform tf)
	{

	}

	/// <summary>
	/// Actions to take when the projectile hits a specific object.
	/// </summary>
	/// <param name="objInt">Object Interaction of what it hits,</param>
	public virtual void onHit(ObjectInteraction objInt)
	{

	}

	/// <summary>
	/// Special code to fire when the projectile hits the player.
	/// </summary>
	public virtual void onHitPlayer()
	{
			
	}


	/// <summary>
	/// Special code for when the player trips this spell. Used in rune traps (UW2)
	/// </summary>
	public virtual void onImpactPlayer()
	{
			
	}


	/* ************Sample enchantment Code *********************
	public override void onHit (ObjectInteraction objInt)
	{//Sample enchantment poisons the target.
	if (objInt.ItemType==ObjectInteraction.NPC_TYPE)
		{
			playerUW.PlayerMagic.CastEnchantment(playerUW.gameObject,objInt.gameObject,SpellEffect.UW1_Spell_Effect_Poison,Magic.SpellRule_TargetOther);				
		}		
	}
	*/
}
