using LostLegend.Master;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LostLegend.Entities.InventoryStuff;
using System.Threading.Tasks;
using LostLegend.Entities.Parts;
using Microsoft.Xna.Framework;

namespace LostLegend.Entities
{
    class PlayerEntity
    {
		public bool Attacking;
        public StatsHolder BaseStats;
		public StatusEffectHolder Effects;
        public Inventory Inventory;
        public EquipmentHolder Equipment;
        public MapInfo.MapAreaIDs CurrentArea;
		public Vector2 PositionBeforeAttack;
		public Vector2 Position;
		public Attack SelectedAttack;
		private string CurrentAnimation;
		private bool HasExecutedAttack;
		public bool BeingAttacked;
		private double AttackTime;

		public PlayerEntity(MasterManager master)
        {
			HasExecutedAttack = false;
			CurrentAnimation = "";
			AttackTime = 0;
			BeingAttacked = false;
			Attacking = false;
			
			SelectedAttack = null;
			Position = new Vector2(119, Measurements.EighthScreen.Y + Measurements.FullScreen.X/4*3-30);
			PositionBeforeAttack = new Vector2(119, Measurements.EighthScreen.Y + Measurements.FullScreen.X / 4 * 3 - 30);
			BaseStats = new StatsHolder(50,50,50,50,5,5);
            Equipment = new EquipmentHolder();
            CurrentArea = (MapInfo.MapAreaIDs)master.storedDataManager.CurrentSaveFile.CurrentArea;
            Inventory = new Inventory(new List<Item>(), 200);
        }


		public string FormatTotalStats()
		{
            StatsHolder stats = new StatsHolder();
            stats.Add(BaseStats);
            Item item = Inventory.ItemByIndex(Equipment.Head);
			if ( item != null && item.IsEquipable) {
                stats.Add(item.EquipStats);
            }

			item = Inventory.ItemByIndex(Equipment.Chest);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}

			item = Inventory.ItemByIndex(Equipment.Legs);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}


			item = Inventory.ItemByIndex(Equipment.Weapon);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}

			item = Inventory.ItemByIndex(Equipment.Accessory);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}



			return $"HP: {stats.MaxHp} ({stats.Hp}) MP: {stats.MaxMp} ({stats.Mp})\\STR: {stats.Strength} DEF: {stats.Defence}";
		}

		public void Attack()
		{
			Attacking = true;
			CurrentAnimation = SelectedAttack.Animation.UserType;
			AttackTime = 0;
		}
		public void Update(MasterManager master)
		{
			if (master.entityManager.CurrentAttackingEnemyIndex == -1)
				BeingAttacked = false;

			if (BeingAttacked)
			{

				AttackTime += master.timePassed;
				Position = master.entityManager.GetAttackingMonster().Attack.Animation.GetMovement(AttackTime, PositionBeforeAttack, master.entityManager.GetAttackingMonster().PositionBeforeAttack, false, true);

				if (AttackTime > master.entityManager.GetAttackingMonster().Attack.Animation.Duration *2)
				{
					Position = PositionBeforeAttack;
					BeingAttacked = false;
					AttackTime = 0;
				}
				else
				if (AttackTime > master.entityManager.GetAttackingMonster().Attack.Animation.Duration)
				{
					Position = master.entityManager.GetAttackingMonster().Attack.Animation.GetMovement(AttackTime - master.entityManager.GetAttackingMonster().Attack.Animation.Duration, PositionBeforeAttack, master.entityManager.GetAttackingMonster().PositionBeforeAttack, true, true);

				}
			}

			if (Attacking)
			{
				AttackTime += master.timePassed;
				Position = SelectedAttack.Animation.GetMovement(AttackTime, PositionBeforeAttack, master.entityManager.GetEntityPos(master.entityManager.SelectedMonsterIndex));

				if (AttackTime > SelectedAttack.Animation.Duration * 2)
				{
					Position = PositionBeforeAttack;
					Attacking = false;
					AttackTime = 0;
					HasExecutedAttack = false;
				}
				else
				if (AttackTime > SelectedAttack.Animation.Duration)
				{
					Position = SelectedAttack.Animation.GetMovement(AttackTime - SelectedAttack.Animation.Duration, PositionBeforeAttack, master.entityManager.GetEntityPos(master.entityManager.SelectedMonsterIndex), true);
					if (SelectedAttack.NeedsTarget && !HasExecutedAttack && master.entityManager.SelectedMonsterIndex != -1)
						master.entityManager.DamageSelectedMonster();
					HasExecutedAttack = true;
				}
			}
		}
	}
}
