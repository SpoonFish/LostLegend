using LostLegend.Entities.InventoryStuff;
using LostLegend.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class EquipmentHolder
	{
		public int Weapon;
		public int Head;
		public int Accessory;
		public int Chest;
		public int Legs;

		public EquipmentHolder() 
		{
			Weapon = -1;
			Head = -1;
				
			Accessory = -1;
			Chest = -1;
			Legs = -1;
		}

		public bool IsEquipped(int itemIndex)
		{
			if (itemIndex == Weapon || itemIndex ==Head || itemIndex == Legs || itemIndex == Accessory || itemIndex == Chest)
				return true;
			else
				return false;
		}

		public Item GetHead(MasterManager master)
		{
			if (Head > -1)
				return master.entityManager.Player.Inventory.Items[Head];
			else
				return null;
		}
		public Item GetChest(MasterManager master)
		{
			if (Chest > -1)
				return master.entityManager.Player.Inventory.Items[Chest];
			else
				return null;
		}
		public Item GetLegs(MasterManager master)
		{
			if (Legs > -1)
				return master.entityManager.Player.Inventory.Items[Legs];
			else
				return null;
		}
		public Item GetWeapon(MasterManager master)
		{
			if (Weapon > -1)
				return master.entityManager.Player.Inventory.Items[Weapon];
			else
				return null;
		}
		public Item GetAccessory(MasterManager master)
		{
			if (Accessory > -1)
				return master.entityManager.Player.Inventory.Items[Accessory];
			else
				return null;
		}
	}
}
