using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHelth;
    [SerializeField] private UnityEvent _healthChanged;

    private int _currentHelth;
    private Animator _animator;

    private void Awake()
    {
        if (_maxHelth == 0)
            _maxHelth = 250;

        TryGetComponent<Animator>(out _animator);
        _currentHelth = _maxHelth;
    }

    public bool Deth { get; private set; }

    public void ChangeHelth(int count)
    {
        if(count == 0)        
            return;    
        if(count>0)
            TakeHeal(count);
        else
            TakeDamage(count);
    }

    public float GetCurrentHelthCoefficient()
    {
        return (float)_currentHelth / (float)_maxHelth;
    }

    private void TakeDamage(int damage)
    {
        if (Deth)
            return;

        _currentHelth += damage;

        if (IsDethChange() == false)
            _animator.SetTrigger("TakeDamage");

        _healthChanged.Invoke();
    }

    private void TakeHeal(int heal)
    {
        if (_currentHelth == _maxHelth)
            return;

        _currentHelth += heal;

        if (_currentHelth > _maxHelth)
            _currentHelth = _maxHelth;

        if (IsDethChange() == false)
            _animator.SetTrigger("Healing");

        _healthChanged.Invoke();
    }

    private bool IsDethChange()
    {
        if(Deth && _currentHelth > 0)
        {
            Deth = false;
            _animator.SetBool("isDie", false);
            return true;
        }
        if(Deth == false && _currentHelth <= 0)
        {
            Deth = true;
            _currentHelth = 0;
            _animator.SetBool("isDie", true);
            _animator.SetTrigger("Deth");
            return true;
        }

        return false;
    }
}
