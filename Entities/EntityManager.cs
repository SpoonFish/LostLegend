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
		public List<int> PendingAttacks;
		public int SelectedMonsterIndex;
        public string CurrentBattleMenuCategory;
        public int CurrentAttackingEnemyIndex;

        public bool PauseCooldownTimer;

		public EntityManager(MasterManager master) 
        {
			PendingAttacks = new List<int>();
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
            MonsterEntities = new List<MonsterEntity>() { new MonsterEntity(ContentLoader.Images["crab"], 0) , new MonsterEntity(ContentLoader.Images["crab"], 1) , new MonsterEntity(ContentLoader.Images["crab"], 2) };
            
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

            Player.Attacking = false;
            Player.BeingAttacked = false;
            TurnPassing = false;
            BattleTime = 0;
			foreach (MonsterEntity entity in MonsterEntities)
				entity.Reset() ;

		}

		public Vector2 GetEntityPos(int index)
		{

			Vector2 pos = new Vector2(Measurements.FullScreen.X / 2 - 20, Measurements.EighthScreen.Y + Measurements.FullScreen.X / 4 - 20);

			pos.X -= MonsterEntities.Count * 27;
			pos.X += index * 54 + 27;

            if (index % 2 == 0)
                pos.Y += 15;

			return pos;

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
			if (!EnemyCooldownTimerPassingOnNextFrame)
			{
				PendingAttacks.Clear();
				EnemyCooldownTimerPassing = false;
			}
			if (!TurnPassing)
            {
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
            foreach (MonsterEntity entity in MonsterEntities)
                entity.Update(master);
		}

		public void DamageSelectedMonster()
		{
            Attack attack = Player.SelectedAttack;
            MonsterEntity entity = MonsterEntities[SelectedMonsterIndex];

            entity.BeingAttacked = true;

            float damageDealt = Player.BaseStats.Strength * attack.DamageMult;
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
	}
}
