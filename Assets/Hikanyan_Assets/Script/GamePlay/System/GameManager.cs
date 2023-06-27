using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Hikanyan.Core;

namespace Hikanyan.Gameplay
{
    public class GameManager : AbstractSingleton<GameManager>
    {
        public GameState _gameState = GameState.None;

        private ScoreManager _scoreManager;
        private TimerManager _timerManager;

        private void Start()
        {
            _scoreManager = new ScoreManager();
            _timerManager = new TimerManager();
            _gameState = GameState.None;
        }

        public void AddScore(int points)
        {
            _scoreManager.AddScore(points);
        }

        public void ResetScore()
        {
            _scoreManager.ResetScore();
        }

        public async UniTask StartTimer(float duration)
        {
            await _timerManager.StartTimer(duration);
        }
        public void StopTimer()
        {
            _timerManager.StopTimer();
        }

        public void ResetTimer()
        {
            _timerManager.ResetTimer();
        }
    }
}