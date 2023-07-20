using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    
    private static int AnimationHealing = Animator.StringToHash("Healing");
    private static int AnimationTakeDamage = Animator.StringToHash("TakeDamage");
    private static int AnimationIsDie = Animator.StringToHash("isDie");
    private static int AnimationDeath = Animator.StringToHash("Death");

    private UnityEvent _healthChanged = new UnityEvent();
    private int _currentHealth;
    private Animator _animator;

    private void Awake()
    {        
        TryGetComponent<Animator>(out _animator);
        _currentHelth = _maxHelth;        
    }

    private void OnValidate()
    {
        if (_maxHelth == 0)
            _maxHelth = 250;
    }

    public bool Death { get; private set; }

    public event UnityAction HealthChanged
    {
        add => _healthChanged.AddListener(value);
        remove => _healthChanged.RemoveListener(value);
    }

    public void TakeDamage(int damage)
    {
        if (Deth || damage <= 0) 
            return;

        _currentHelth -= damage;

        if (IsDethChange() == false)
            _animator.SetTrigger(AnimationTakeDamage);

        _healthChanged.Invoke();
    }

    public void TakeHeal(int heal)
    {
        if (_currentHelth == _maxHelth || heal <= 0)
            return;

        _currentHelth += heal;

        if (_currentHelth > _maxHelth)
            _currentHelth = _maxHelth;

        if (IsDethChange() == false)
            _animator.SetTrigger(AnimationHealing);

        _healthChanged.Invoke();
    }
    public float GetCurrentHealthCoefficient()
    {
        return (float)_currentHelth / (float)_maxHelth;
    }

    private bool IsDeathChange()
    {
        if(Deth && _currentHelth > 0)
        {
            Deth = false;
            _animator.SetBool(AnimationIsDie, false);
            return true;
        }
        if(Deth == false && _currentHelth <= 0)
        {
            Deth = true;
            _currentHelth = 0;
            _animator.SetBool(AnimationIsDie, true);
            _animator.SetTrigger(AnimationDeth);
            return true;
        }

        return false;
    }
}
