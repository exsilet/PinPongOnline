using System;
using TMPro;
using UnityEngine;

namespace CodeBase
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private ScorePlayer _player;
        private ScoreEnemy _enemy;
        
        private int _scoreTextEnemy = 0;
        private int _scoreTextPlayer = 0;

        public void Construct(ScorePlayer scorePlayer, ScoreEnemy scoreEnemy)
        {
            _player = scorePlayer;
            _enemy = scoreEnemy;
        }
        
        private void Start()
        {
            UpdateScore();
            _enemy.TextChanged += OnValueChangedEnemy;
            _player.TextChanged += OnValueChangedPlayer;
        }

        private void OnEnable()
        {
            // _enemy.TextChanged += OnValueChangedEnemy;
            // _player.TextChanged += OnValueChangedPlayer;
        }

        private void OnDisable()
        {
            _enemy.TextChanged -= OnValueChangedEnemy;
            _player.TextChanged -= OnValueChangedPlayer;
        }

        private void OnValueChangedEnemy(int value)
        {
            _scoreTextEnemy++;
            UpdateScore();
        }
        
        private void OnValueChangedPlayer(int value)
        {
            _scoreTextPlayer++;
            UpdateScore();
        }

        private void UpdateScore()
        {
            _text.text = $"{_enemy.ScorePlayer} : {_player.ScoreEnemy}";
        }
    }
}
