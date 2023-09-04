using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class Attack
	{
		public float RangeRadius;
		public AttackAnimation Animation;
		public float DamageMult;
		public float HpHeal;
		public float HpSteal;
		public float HpFlatHeal;

		public float MpHeal;
		public float MpFlatHeal;

		public float SelfDamage;
		public float MpCost;
		public bool NeedsTarget;

		public float Cooldown;
		public float Chargeup;

		public Attack(float radius, AttackAnimation animation, float dmgMult, float mpCost, float cooldown, float chargeup =0, float hpHeal=0, float hpSteal=0, bool needsTarget=true, float hpFlatHeal=0, float mpHeal=0, float mpFlatHeal=0, float selfDamage = 0)
		{
			Cooldown = cooldown;
			Chargeup = chargeup;
			MpCost = mpCost;
			RangeRadius = radius;
			Animation = animation;
			DamageMult = dmgMult;
			HpHeal = hpHeal;
			HpSteal = hpSteal;
			MpHeal = mpHeal;
			SelfDamage = selfDamage;
			NeedsTarget = needsTarget;
			HpFlatHeal = hpFlatHeal;
			MpFlatHeal= mpFlatHeal;
		}
	}
}
