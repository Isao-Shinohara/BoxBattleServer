using System;

namespace BoxBattle
{
	public static class BattleCalculate
	{
		public static int Attack(int attackerMp)
		{
			var rate = 1 + attackerMp / 10;

			var cRandom = new Random();
			int damagePoint = cRandom.Next(5000, 10001);
			damagePoint *= rate;

			return damagePoint;
		}
	}
}
