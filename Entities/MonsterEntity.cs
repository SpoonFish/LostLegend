using Javax.Crypto;
using LostLegend.Entities.Parts;
using LostLegend.Graphics;
using LostLegend.Master;
using LostLegend.Statics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities
{
	class MonsterEntity
	{
		public int IndexPosition;
		public Vector2 Position;
		public StatsHolder BaseStats;
		public bool Attacking;
		public bool BeingAttacked;
		private double AttackTime;
		public Vector2 PositionBeforeAttack;
		public float MaxCooldown;
		public float Cooldown;
		public float PrevCooldown;
		public Attack Attack;
		private float PartialCooldown;

		public AnimatedTexture Texture;
		private bool HasExecutedAttack;

		public MonsterEntity(AnimatedTexture texture, int indexPosition)
		{
			HasExecutedAttack = false;
			Attack = AttackInfo.AttackDict["lol_slash"];
			AttackTime = 0;
			Attacking = false;
			BeingAttacked = false;
			Texture = texture;
			IndexPosition = indexPosition;
			PositionBeforeAttack = new Vector2();
			Position = new Vector2();
			BaseStats = new StatsHolder(50, 50, 50, 50, 5, 5);
			MaxCooldown = Attack.Cooldown; 
			Cooldown = Attack.Cooldown;
			PrevCooldown = Attack.Cooldown;
			PartialCooldown = 0;
		}
		public void Update(MasterManager master)
		{
			if (master.entityManager.Player.SelectedAttack == null)
				return;
			if (master.entityManager.EnemyCooldownTimerPassing)
			{
				Cooldown = PrevCooldown - (float)(-master.entityManager.EnemyCooldownTimer/1+1) * master.entityManager.Player.SelectedAttack.Cooldown + PartialCooldown;
				if (Cooldown < 0.07 && master.entityManager.CurrentAttackingEnemyIndex == -1)
				{
					PartialCooldown += PrevCooldown;
					Cooldown = MaxCooldown;
					PrevCooldown = MaxCooldown;
					Attacking = true;
					master.entityManager.PendingAttacks.Add(IndexPosition);
					master.entityManager.CurrentAttackingEnemyIndex = IndexPosition;
					master.entityManager.PauseCooldownTimer = true;
				}
			}
			else
			{
				PartialCooldown = 0;
				PrevCooldown = Cooldown;
			}

			if (PositionBeforeAttack == Vector2.Zero)
			{
				PositionBeforeAttack = master.entityManager.GetEntityPos(IndexPosition);
				Position = master.entityManager.GetEntityPos(IndexPosition);
			}

			if (BeingAttacked)
			{
				AttackTime += master.timePassed;
				Position = master.entityManager.Player.SelectedAttack.Animation.GetMovement(AttackTime, PositionBeforeAttack, master.entityManager.Player.PositionBeforeAttack, false, true);

				if (AttackTime > master.entityManager.Player.SelectedAttack.Animation.Duration * 2)
				{
					Position = PositionBeforeAttack;
					master.entityManager.EnemyCooldownTimer = 1;
					BeingAttacked = false;
					AttackTime = 0;
				}
				else
				if (AttackTime > master.entityManager.Player.SelectedAttack.Animation.Duration)
				{
					Position = master.entityManager.Player.SelectedAttack.Animation.GetMovement(AttackTime - master.entityManager.Player.SelectedAttack.Animation.Duration, PositionBeforeAttack, master.entityManager.Player.PositionBeforeAttack, true, true);
				
				}
			}


			if (Attacking && master.entityManager.CurrentAttackingEnemyIndex == IndexPosition)
			{
				AttackTime += master.timePassed;
				Position = Attack.Animation.GetMovement(AttackTime, PositionBeforeAttack, master.entityManager.Player.Position);

				if (AttackTime > Attack.Animation.Duration * 2)
				{
					AttackTime = 0;
					Position = PositionBeforeAttack;
					Attacking = false;
					master.entityManager.CurrentAttackingEnemyIndex = -1;
					HasExecutedAttack = false;
					master.entityManager.PauseCooldownTimer = false;
				}
				else
				if (AttackTime > Attack.Animation.Duration)
				{
					Position = Attack.Animation.GetMovement(AttackTime - Attack.Animation.Duration, PositionBeforeAttack, master.entityManager.Player.Position, true);
					if (Attack.NeedsTarget && !HasExecutedAttack)
						master.entityManager.DamagePlayer(BaseStats, Attack);
					HasExecutedAttack = true;
				}
			}
		}

		public void Reset()
		{
			Attacking = false;
			BeingAttacked = false;
		}
	}
}
