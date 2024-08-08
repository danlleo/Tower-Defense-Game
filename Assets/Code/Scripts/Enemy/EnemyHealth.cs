using System;
using System.Collections;
using Logic;
using UnityEngine;

namespace Enemy
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public Animator Animator;

    [SerializeField] private float _current;
    [SerializeField] private float _max;

    public event Action HealthChanged;
    
    private bool isRegenerating;

    public float Current
    {
      get => _current;
      set => _current = value;
    }

    public float Max
    {
      get => _max;
      set => _max = value;
    }

    public void TakeDamage(float damage)
    {
      Current -= damage;
      
      //Animate
      
      HealthChanged?.Invoke();
    }

    public void StartRegeneration(float regenerationRate, float regenerationAmount)
    {
      if (!isRegenerating)
      {
        isRegenerating = true;
        StartCoroutine(RegenerateHealth(regenerationRate, regenerationAmount));
        HealthChanged?.Invoke();
      }
    }
    
    public void StopRegeneration() =>
      isRegenerating = false;

    private IEnumerator RegenerateHealth(float regenerationSpeed, float amount)
    {
      while (isRegenerating && _current < _max)
      {
        _current += amount;
        _current = Mathf.Clamp(_current, 0, _max);
        yield return new WaitForSeconds(regenerationSpeed);
      }
      isRegenerating = false;
    }
  }
}