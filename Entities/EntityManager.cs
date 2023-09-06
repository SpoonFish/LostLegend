using Android.Media.TV;
using LostLegend.Entities.Parts;
using LostLegend.Graphics.GUI;
using LostLegend.Master;
using LostLegend.Statics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities
{
	class EntityManager
	{
		public double EnemyCooldownTimer;
		private bool EnemyCooldownTimerPassingOnNextFrame;
		public bool EnemyCooldownTimerPassing;
		public double BattleTime;
		public bool TurnPassing;
		public PlayerEntity Player;
		public List<MonsterEntity> MonsterEntities;
		public List<RisingText> RisingTexts;
		public List<FadingImage> DroppedItems;
		public List<string> ItemsToLoot;
		public int SelectedMonsterIndex;
		public string CurrentBattleMenuCategory;
		public int CurrentAttackingEnemyIndex;

		public bool BattleInProgress;
		public bool PauseCooldownTimer;

		public EntityManager(MasterManager master)
		{
			BattleInProgress = true;
			DroppedItems = new List<FadingImage>();
			ItemsToLoot = new List<string>();
			CurrentAttackingEnemyIndex = -1;
			PauseCooldownTimer = false;
			EnemyCooldownTimer = 0;
			EnemyCooldownTimerPassingOnNextFrame = false;
			EnemyCooldownTimerPassing = false;
			BattleTime = 0;
			RisingTexts = new List<RisingText>();
			TurnPassing = false;
			CurrentBattleMenuCategory = "attacks";
			SelectedMonsterIndex = -1;
			MonsterEntities = new List<MonsterEntity>() { };
			for (int i = 0; i < 4; i++)
				MonsterEntities.Add(MonsterInfo.GetMonster("crab", i));

		}
		public void LoadPlayer(MasterManager master)
		{
			Player = new PlayerEntity(master);
		}

		public void TriggerTurnCycle()
		{

			if (SelectedMonsterIndex == -1 && (Player.SelectedAttack != null && !Player.SelectedAttack.NeedsTarget))
				return;
			TurnPassing = true;
			Player.Attack();

		}
		public void ResetBattle()
		{

			ItemsToLoot.Clear();
			DroppedItems.Clear();
			Player.Attacking = false;
			Player.BeingAttacked = false;
			TurnPassing = false;
			BattleTime = 0;
			foreach (MonsterEntity entity in MonsterEntities)
				entity.Reset();

		}

		public Vector2 GetEntityPos(int index)
		{

			Vector2 pos = new Vector2(Measurements.FullScreen.X / 2 - 20, Measurements.EighthScreen.Y + Measurements.FullScreen.X / 4 - 20);

			if (index < 5)
			{

				pos.X -= Math.Min(5, MonsterEntities.Count) * 27;
				pos.X += index * 54 + 27;

				if (index % 2 == 0)
					pos.Y += 15;
			}
			else
			{

				pos.X -= Math.Min(5, MonsterEntities.Count - 5) * 27;
				pos.X += (index - 5) * 54 + 27;
				pos.Y += 70;

				if (index % 2 == 1)
					pos.Y += 15;
			}


			return pos;

		}

		public void KillMonsterByIndex(int index)
		{
			MonsterEntities[index] = MonsterInfo.GetMonster("dead", index);
		}

		public bool AnyEntityAttacking()
		{
			foreach (MonsterEntity entity in MonsterEntities)
				if (entity.Attacking || entity.BeingAttacked)
					return true;
			return false;
		}
		public void Update(MasterManager master)
		{

			if (BattleInProgress)
			{

				if (IsPlayerDead())
				{
					master.entityManager.BattleInProgress = false;
					ItemsToLoot.Clear();
					DroppedItems.Clear();
				}
				else if (IsAllMonstersDead())
				{
					master.entityManager.BattleInProgress = false;
					foreach (string item in ItemsToLoot)
					{
						Player.Inventory.AddItem(item);
					}
					ItemsToLoot.Clear();
					DroppedItems.Clear();
				}
			}
			if (!EnemyCooldownTimerPassingOnNextFrame)
			{
				EnemyCooldownTimerPassing = false;
			}
			if (!TurnPassing)
			{
				Player.Position = Player.PositionBeforeAttack;
				return;
			}

			if (EnemyCooldownTimer > 0 && !PauseCooldownTimer)
			{
				EnemyCooldownTimerPassing = true;
				EnemyCooldownTimerPassingOnNextFrame = true;
				EnemyCooldownTimer -= master.timePassed;
				if (EnemyCooldownTimer < 0)
				{
					EnemyCooldownTimerPassingOnNextFrame = false;
					EnemyCooldownTimer = 0;
				}

			}
			else if (!Player.Attacking && !Player.BeingAttacked && !AnyEntityAttacking() && !PauseCooldownTimer)
				TurnPassing = false;
			Player.Update(master);
			for (int i = MonsterEntities.Count - 1; i >= 0; i--)
			{
				MonsterEntity entity = MonsterEntities[i];
				entity.Update(master);
			}
		}

		private bool IsAllMonstersDead()
		{

			foreach (MonsterEntity entity in MonsterEntities)
				if (entity.Name != "dead")
					return false;
			return true;
		}

		public void DamageSelectedMonster()
		{
            Attack attack = Player.SelectedAttack;
            MonsterEntity entity = MonsterEntities[SelectedMonsterIndex];

            entity.BeingAttacked = true;

            float damageDealt = (Player.BaseStats.Strength + Player.Equipment.GetTotalStats(Player).Strength)* attack.DamageMult;
            float hpHealed = 0;
            float mpHealed = 0;

			entity.BaseStats.Hp -= damageDealt;

            hpHealed += Player.BaseStats.MaxHp * attack.HpHeal;
			mpHealed += Player.BaseStats.MaxMp * attack.MpHeal;

			hpHealed += attack.HpFlatHeal;
			mpHealed += attack.MpFlatHeal;

            hpHealed += damageDealt * attack.HpSteal;

            Player.BaseStats.Hp += hpHealed;
            Player.BaseStats.Mp += mpHealed;

            RisingTexts.Add(new RisingText(entity.Position, $"{(int)damageDealt}", "red", 1));
		}

		public void DamagePlayer(StatsHolder baseStats, Attack attack)
		{

			Player.BeingAttacked = true;

			float damageDealt = baseStats.Strength * attack.DamageMult;
			float hpHealed = 0;
			float mpHealed = 0;

			Player.BaseStats.Hp -= damageDealt;

			hpHealed += Player.BaseStats.MaxHp * attack.HpHeal;
			mpHealed += Player.BaseStats.MaxMp * attack.MpHeal;

			hpHealed += attack.HpFlatHeal;
			mpHealed += attack.MpFlatHeal;

			hpHealed += damageDealt * attack.HpSteal;

			baseStats.Hp += hpHealed;
			baseStats.Mp += mpHealed;

			RisingTexts.Add(new RisingText(Player.Position, $"{(int)damageDealt}", "red", 1));
		}

		public MonsterEntity GetAttackingMonster()
		{
			foreach (MonsterEntity entity in MonsterEntities)
			{
				if (entity.IndexPosition == CurrentAttackingEnemyIndex) 
				{ 
					return entity;
				}
			}
			return null;
		}

		public bool IsPlayerDead()
		{
			return Player.BaseStats.Hp < 0;
		}

		public void AddLoot(LootTable lootTable, Vector2 position)
		{
			Random rnd = new Random();
			if (lootTable.ExclusiveOutcome)
			{
				LootTableEntry entry = lootTable.Possibilities[rnd.Next(0, lootTable.Possibilities.Count - 1)];
				for (int i = entry.AmountMin; i <= entry.AmountMax; i++)
				{ 
					ItemsToLoot.Add(entry.Item);
					DroppedItems.Add(new FadingImage(new ImagePanel(position + new Vector2(rnd.Next(-3, 13), rnd.Next(-3, 13)), ContentLoader.Images[entry.Item.Replace(' ', '_')], new Vector2(20, 20)), "in", 0.5));
				}
			
			}
			else
			{
				float chance = rnd.NextSingle();

				foreach (LootTableEntry entry in lootTable.Possibilities)
				{
					if (chance < entry.Chance)
						for (int i = entry.AmountMin; i <= entry.AmountMax; i++)
						{
							ItemsToLoot.Add(entry.Item);
							DroppedItems.Add(new FadingImage(new ImagePanel(position + new Vector2(rnd.Next(-3, 13), rnd.Next(-3, 13)), ContentLoader.Images[entry.Item.Replace(' ', '_')], new Vector2(20, 20)), "in", 0.5));
						}

				}
			}
		}

	}
}
