using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class StatsHolder
	{
		public float Hp;
		public float MaxHp;
		public float OrigMaxHp;
		public float Mp;
		public float MaxMp;
		public float OrigMaxMp;
		public float Strength;
		public float Defence;
		public float OrigStrength;
		public float OrigDefence;

		public StatsHolder(float hp =0, float maxHp=0, float mp=0, float maxMp=0, float strength=0, float defence=0)
		{
			Hp = hp;
			MaxHp = maxHp;
			OrigMaxHp = maxHp;
			Mp = mp;
			MaxMp = maxMp;
			OrigMaxMp = maxMp;
			Strength = strength;
			OrigStrength = strength;
			Defence = defence;
			OrigDefence = defence;
		}

		public void Add(StatsHolder stats)
		{
			Hp += stats.Hp;
			MaxHp += stats.MaxHp;
			Mp += stats.Mp;
			MaxMp += stats.MaxMp;
			Strength += stats.Strength;
			Defence += stats.Defence;
		}
	}
}
