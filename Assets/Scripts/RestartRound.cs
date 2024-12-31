using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RestartRound : MonoBehaviour
{
    private int ADDITION_SCORE_FOR_HIT = 10;

    [Header("Respawn")]
    [SerializeField] private Transform _hero;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _heroRespawnTransform;
    [SerializeField] private Transform _enemyRespawnTransform;



    [Header("Score")]
    [SerializeField] private GameObject _heroScore;
    [SerializeField] private GameObject _enemyScore;
    private Color _enemyColor;
    private Color _heroColor;

    private Dictionary<Color, GameObject> _scores;

    private void OnEnable()
    {
        Unithit.OnUnitHit += AddScorePointAndRestart;
    }

    private void OnDisable()
    {
        Unithit.OnUnitHit -= AddScorePointAndRestart;
    }

    private void Awake()
    {
        _heroColor = _hero.gameObject.GetComponent<SpriteRenderer>().color;
        _enemyColor = _enemy.gameObject.GetComponent<SpriteRenderer>().color;
        _scores = new Dictionary<Color, GameObject> { { _heroColor, _enemyScore }, {_enemyColor, _heroScore } };
    }

    private void AddScorePointAndRestart(Color color)
    {
        UpdateScores(color);
        RespawnUnits();
    }

    private void UpdateScores(Color color)
    {
        TextMeshProUGUI textMeshPro = _scores[color].GetComponent<TextMeshProUGUI>();

        if (int.TryParse(textMeshPro.text, out int currentScore))
        {
            textMeshPro.text = (currentScore + ADDITION_SCORE_FOR_HIT).ToString();
        }
    }

    private void RespawnUnits()
    {
        _hero.position = _heroRespawnTransform.position;
        _enemy.gameObject.GetComponent<NavMeshAgent>().Warp(_enemyRespawnTransform.position);
    }


}
