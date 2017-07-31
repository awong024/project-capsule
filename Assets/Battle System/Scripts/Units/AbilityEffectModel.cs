using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEffectModel : ScriptableObject {
  public enum HealthEffect { 
    None,
    Damage,
    Heal
  }

  [SerializeField] HealthEffect healthEffect;
  [SerializeField] int power;

  public HealthEffect Health_Effect { get { return healthEffect; } }
  public int Power                  { get { return power; } }
}
