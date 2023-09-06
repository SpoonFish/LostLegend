using Javax.Crypto;
using LostLegend.Entities.Parts;
using LostLegend.Graphics;
using LostLegend.Master;
using LostLegend.Statics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
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
		public StatusEffectHolder Effects;
		public bool Attacking;
		public bool BeingAttacked;
		private double AttackTime;
		public Vector2 PositionBeforeAttack;
		public float MaxCooldown;
		public float Cooldown;
		public float PrevCooldown;
		public float DisplayOpacity;
		public Attack Attack;
		private float PartialCooldown;
		private bool Dying;
		private bool DroppedLoot;

		public AnimatedTexture Texture;
		private bool HasExecutedAttack;
		public string Name;

		public MonsterEntity(AnimatedTexture texture, string name, int indexPosition, StatsHolder baseStats, Attack attack, bool dead = false)
		{
			Name = name;
			Effects = null;
			DisplayOpacity = 1;
			HasExecutedAttack = false;
			Attack = attack;
			AttackTime = 0;
			Attacking = false;
			BeingAttacked = false;
			DroppedLoot = false;
			Texture = texture;
			if (dead)
				DisplayOpacity = 0;
			IndexPosition = indexPosition;
			PositionBeforeAttack = new Vector2();
			Position = new Vector2();
			BaseStats = baseStats;
			float col = new Random().NextSingle(2f, 4);
			MaxCooldown = col;
			Cooldown = col;
			Dying = false;
			PrevCooldown = col;
			PartialCooldown = 0;
		}
		public void Update(MasterManager master)
		{
			if (DisplayOpacity <= 0)
				return;

			if (Dying)
			{
				AttackTime += master.timePassed;
				DisplayOpacity = Math.Max(0, (-(float)AttackTime + 1) / 1);
				if (AttackTime > 0.5f && !DroppedLoot)
				{

					master.entityManager.AddLoot(MonsterInfo.LootTableDict[Name], Position);
					DroppedLoot = true;
				}
				if (AttackTime > 1)
				{
					master.entityManager.SelectedMonsterIndex = -1;
					master.entityManager.CurrentAttackingEnemyIndex = -1;
					master.entityManager.PauseCooldownTimer = false;
					master.entityManager.KillMonsterByIndex(IndexPosition);
					return;
				}
			}
			if (master.entityManager.Player.SelectedAttack == null)
				return;
			if (master.entityManager.EnemyCooldownTimerPassing)
			{
				if (BaseStats.Hp <= 0 && master.entityManager.CurrentAttackingEnemyIndex == -1)
				{

					Dying = true;
					AttackTime = 0;
					master.entityManager.CurrentAttackingEnemyIndex = IndexPosition;
					master.entityManager.PauseCooldownTimer = true;
				}
				Cooldown = PrevCooldown - (float)(-master.entityManager.EnemyCooldownTimer/1+1) * master.entityManager.Player.SelectedAttack.Cooldown + PartialCooldown;
				if (Cooldown < 0.07 && master.entityManager.CurrentAttackingEnemyIndex == -1)
				{
					PartialCooldown += PrevCooldown;
					Cooldown = MaxCooldown;
					PrevCooldown = MaxCooldown;
					Attacking = true;
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
