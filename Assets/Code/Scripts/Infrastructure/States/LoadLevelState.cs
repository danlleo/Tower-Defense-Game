using Infrastructure.Factory;
using Logic;
using Services.Mobs;
using UnityEngine;

namespace Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
		}

		private void InitGameWorld()
		{
			InitSpawners();
		}
		
		private void InitSpawners()
		{
			foreach (GameObject spawnerObjects in GameObject.FindGameObjectsWithTag(Tags.EnemySpawner))
			{
				EnemySpawner spawner = spawnerObjects.GetComponent<EnemySpawner>();
				spawner.Spawn();
			}
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			_gameFactory.CreateHUD();

			_stateMachine.Enter<GameLoopState>();
		}
	}

}