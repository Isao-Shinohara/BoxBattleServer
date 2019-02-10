using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BoxBattle
{
	public class BattleService : BaseService{

		public async Task<BattleEntity> InitializeBattle(string uuid)
		{
			// Battle.
			var battle = await battleRepository.GetAsync(BattleEntity.Key);
			if (battle == null) {
				battle = new BattleEntity();
			}

			// My player.
			var cRandom = new Random();
			var max = Enum.GetValues(typeof(CharacterType)).Cast<int>().Max() + 1;
			var myPlayer = await playerRepository.GetAsync(uuid);
			if(myPlayer != null) {
				myPlayer.ChangeCharacter((CharacterType)cRandom.Next(max));
				myPlayer.Recover();
			} else {
				myPlayer = new PlayerEntity(uuid, (CharacterType)cRandom.Next(max));
			}
			battle.UpdatePlayer(myPlayer);
			await playerRepository.UpdateAsync(myPlayer);

			// Enemy player.
			var enemyPlayer = await playerRepository.GetAsync(PlayerData.EnemyUuid);
			if (enemyPlayer == null) {
				enemyPlayer = new PlayerEntity(PlayerData.EnemyUuid, (CharacterType)cRandom.Next(max));
				await playerRepository.UpdateAsync(enemyPlayer);
				battle.UpdatePlayer(enemyPlayer);
			}
			battle.SetEnemyPlayer(enemyPlayer);

			// All player.
			await battleRepository.UpdateAsync(battle);
			await battleRepository.Save();

			battle = await battleRepository.GetAsync(BattleEntity.Key);
			return battle;
		}

		public async Task<PlayerEntity> JoinAsync(string uuid, Vector3 position, Quaternion rotation)
		{
			var player = await playerRepository.GetAsync(uuid);
			var battle = await battleRepository.GetAsync(BattleEntity.Key);
			player.Move(position, rotation);
			battle.UpdatePlayer(player);
			await playerRepository.UpdateAsync(player);
			await battleRepository.UpdateAsync(battle);
			await battleRepository.Save();
			return player;
		}

		public async Task<PlayerEntity> LeaveAsync(string uuid)
		{
			var player = await playerRepository.GetAsync(uuid);

			var battle = await battleRepository.GetAsync(BattleEntity.Key);
			if (battle != null) {
				battle.LeavePlayer(player);
				await battleRepository.UpdateAsync(battle);
				await battleRepository.Save();
			}

			return player;
		}

		public async Task<(PlayerEntity, List<PlayerEntity>)> Attack(string attackerUuid, int attackerMp, List<string> defenderUuidList)
		{
			var attacker = await playerRepository.GetAsync(attackerUuid);
			var defenderList = await playerRepository.GetListAsync(defenderUuidList);

			defenderList.ForEach(defender => {
				defender.Damage(attackerMp);
			});

			await playerRepository.UpdateListAsync(defenderList);
			await playerRepository.Save();

			return (attacker, defenderList);
		}

		public async Task<PlayerEntity> Recover(string uuid)
		{
			var player = await playerRepository.GetAsync(uuid);
			player.Recover();
			await playerRepository.UpdateAsync(player);
			await playerRepository.Save();
			return player;
		}

		public async Task Move(string uuid, Vector3 position, Quaternion rotation)
		{
			var player = await playerRepository.GetAsync(uuid);
			var battle = await battleRepository.GetAsync(BattleEntity.Key);
			player.Move(position, rotation);
			battle.UpdatePlayer(player);
			playerRepository.UpdateAsync(player);
			battleRepository.UpdateAsync(battle);
			await battleRepository.Save();
		}
	}
}