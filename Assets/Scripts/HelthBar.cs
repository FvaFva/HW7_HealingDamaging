using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _smoothnessFactor;

    private Slider _slider;
    private Coroutine _changeWork;
    private float _targetValue;

    private void Awake()
    {
        TryGetComponent<Slider>(out _slider);
    }

    private void OnValidate()
    {
        if (_smoothnessFactor == 0)
            _smoothnessFactor = 1;
    }

    private void OnEnable()
    {       
        _player.HealthChanged += UpdatePlayerHealthInfo;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= UpdatePlayerHealthInfo;
    }

    public void UpdatePlayerHealthInfo()
    {
        _targetValue = _player.GetCurrentHelthCoefficient();

        if (_changeWork != null)
            StopCoroutine(_changeWork);

        _changeWork = StartCoroutine(InfoUpdater());
    }

    private IEnumerator InfoUpdater()
    {
        while(_slider.value != _targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Mathf.Abs(_smoothnessFactor) * Time.deltaTime);
            yield return null;
        }
    }
}
