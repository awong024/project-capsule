using UnityEngine;
using System.Collections;

public class StatCalculator
{
  const int HEALTH_POINTS_BASE = 500;
  const int HEALTH_POINTS_PER_VITALITY = 10;

  public static int HealthFromVitality(int vitality) {
    return HEALTH_POINTS_BASE + (HEALTH_POINTS_PER_VITALITY * vitality);
  }
}
