using System;
using System.Threading.Tasks;
using DTT.MinigameMemory;
using Naninovel;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Service
{
    [InitializeAtRuntime]
    [CommandAlias("miniGame")]
    public class MiniGameService : Naninovel.Command,IEngineService
    {
        private MemoryGameManager _memoryGameManager;
        private MemoryGameResults _memoryGameResults;
        private TaskCompletionSource<MemoryGameResults> _taskCompletionSource = new TaskCompletionSource<MemoryGameResults>();

        public BooleanParameter State;
        
        public UniTask InitializeServiceAsync() => UniTask.CompletedTask;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            if (State)
                return StartGameAsync();

            ResetService();
            return UniTask.CompletedTask;
        }
        
        public void ResetService()
        {
            if (_memoryGameManager != null)
            {
                _memoryGameManager.Finish -= StoreResults;
                _memoryGameManager = null;
            }
            _memoryGameResults = null;
            State = false;
        }

        public void DestroyService() => ResetService();

        public async UniTask StartGameAsync()
        {
            await SceneManager.LoadSceneAsync("Demo");
            _memoryGameManager = Object.FindObjectOfType<MemoryGameManager>();
            _memoryGameManager.Finish += StoreResults;
            await _taskCompletionSource.Task;
            await SceneManager.UnloadSceneAsync("Demo");
        }
        
        private void OnGameFinished(MemoryGameResults results)
        {
            _taskCompletionSource.TrySetResult(results);
            _memoryGameManager.Finish -= StoreResults;
            SceneManager.UnloadSceneAsync("Demo");
            SceneManager.LoadSceneAsync("SampleScene");
            State = false;
        }
        
        private void StoreResults(MemoryGameResults results)
        {
            if(results.amountOfTurns >= 3)
                OnGameFinished(results);
            if (_memoryGameResults == null)
            {
                _memoryGameResults = results;
                return;
            }

            TimeSpan totalTimeTaken = _memoryGameResults.timeTaken + results.timeTaken;
            int totalTurnsTaken = _memoryGameResults.amountOfTurns + results.amountOfTurns;
            _memoryGameResults = new MemoryGameResults(totalTimeTaken, totalTurnsTaken);
            _taskCompletionSource.TrySetResult(_memoryGameResults);
        }
    }
}