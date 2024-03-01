using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float _targetDistance;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panelOne;
    [SerializeField] private GameObject _panelTwo;
    private GameObject _currentPanel;

    private Vector3 _startPosition;


    void Start()
    {
        _startPosition = transform.position;
        _currentPanel = _panelOne;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _targetDistance)
        {
            _currentPanel.SetActive(true);
        }
       else
        {
            _panelOne.SetActive(false);
        }
    }


    public void Button()
    {
        _currentPanel.SetActive(false);
        _currentPanel = _panelTwo;
        
        _panelTwo.SetActive(true);
    }



}
