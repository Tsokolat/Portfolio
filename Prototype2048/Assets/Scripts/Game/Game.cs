using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class Game : ScriptableObject
	{
		public static Game Instance { get; private set; }

		public ServiceLocator ServiceLocator { get; } = new ServiceLocator();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Init()
		{
			Instance = CreateInstance<Game>(); // or another source
		}

		void Awake()
		{
          
            var scoreTracker = new ScoreTracker();
            var maxLevelMergeMediator = new MaxLevelMergeMediator(scoreTracker);
            var board = new Board(maxLevelMergeMediator);

            ServiceLocator.Bind(board);
            ServiceLocator.Bind(scoreTracker);
		}

	}
}
