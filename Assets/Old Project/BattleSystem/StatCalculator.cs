using UnityEngine;
using System.Collections;

public class StatCalculator
{
  const int HEALTH_POINTS_BASE = 1000;
  const int HEALTH_POINTS_PER_VITALITY = 10;

  const int ATTACK_TIMER_BASE = 30;
  const float ATTACK_TIMER_PER_AGILITY = 0.35f;
  const int ATTACK_TIMER_MIN = 10;

  const int ENERGY_POINTS_BASE = 10;
  const float ENERGY_POINTS_PER_AGILITY = 0.2f;
  const float ENERGY_POINTS_PER_INTELLECT = 0.1f;

  const int AUTO_DAMAGE_BASE = 50;
  const int AUTO_DAMAGE_PER_STRENGTH = 1;

  const int ABILITY_DAMAGE_BASE = 200;
  const int ABILITY_DAMAGE_PER_INTELLECT = 4;

  public static int HealthFromVitality(int vitality) {
    return HEALTH_POINTS_BASE + (HEALTH_POINTS_PER_VITALITY * vitality);
  }

  public static int AttackTimerFromAgility(int agility) {
    return Mathf.Max( ATTACK_TIMER_BASE - (int)(ATTACK_TIMER_PER_AGILITY * (float)agility), ATTACK_TIMER_MIN );
  }

  public static int EnergyPerTurnFromAgilityAndIntellect(int agility, int intellect) {
    return ENERGY_POINTS_BASE + (int)(ENERGY_POINTS_PER_AGILITY * (float)agility) + (int)(ENERGY_POINTS_PER_INTELLECT * (float)intellect);
  }

  public static int AutoDamageFromStrength(int strength) {
    return AUTO_DAMAGE_BASE + (AUTO_DAMAGE_PER_STRENGTH * strength);
  }

  public static int AbilityDamageFromIntellect(int intellect) {
    return ABILITY_DAMAGE_BASE + (ABILITY_DAMAGE_PER_INTELLECT * intellect);
  }
}
