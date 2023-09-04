using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class AttackAnimation
	{
		private float MovementToTarget;
		public string UserType;
		public string TargetType;
		private double Timer;
		private double Progress;
		public double Duration;

		public AttackAnimation (float movementToTarget, double duration, string userAnimationType, string victimType)
		{
			MovementToTarget = movementToTarget;
			Duration = duration;
			Timer = 0;
			Progress = 0;
			UserType = userAnimationType;
			TargetType = victimType;
		}
		public Vector2 GetMovement(double timer, Vector2 userPos, Vector2 targetPos, bool reverse = false, bool affectsTarget = false)
		{
			float progress;
			if (reverse)
				progress = (float)(timer / Duration) * -1 + 1;
			else
				progress = (float)(timer / Duration);

			string currentType;
			if (affectsTarget)
				currentType = TargetType;
			else
				currentType = UserType;

			Vector2 distance = new Vector2();
			switch (currentType)
			{
				case "move":
					distance = Vector2.Lerp(userPos, targetPos, progress * MovementToTarget);
					break;

				case "knockback":

					distance = Vector2.Lerp(userPos, targetPos, progress * 0.1f);
					distance = -(distance - userPos) + userPos;
					break;
			}

			return distance;
		}
	}
}
