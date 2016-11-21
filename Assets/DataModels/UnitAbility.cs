using UnityEngine;
using System.Collections;

public class UnitAbility : ScriptableObject {
  public enum EffectType {
    Damage,
    Healing,
    Shield
  }

  [SerializeField] string abilityName;
  [SerializeField] Sprite abilityIcon;
  [SerializeField] EffectType abilityEffect;
  [SerializeField] int castTime;
  [SerializeField] int numTargets;
  [SerializeField] string animationFX_name;

  public string AbilityName { get { return abilityName; } }
  public Sprite AbilityIcon { get { return abilityIcon; } }
  public EffectType AbilityEffect { get { return abilityEffect; } }
  public int CastTime { get { return castTime; } }
  public int NumTargets { get { return numTargets; } }
  public string AnimationName { get { return animationFX_name; } }
}
