﻿using UnityEngine;
using System.Collections;

public enum FigurineRarity {
  Common,
  Rare,
  Mythic
}

public class FigurineModel : ScriptableObject {
  [SerializeField] FigurineRarity rarity;
  [SerializeField] string figureName;
  [SerializeField] Sprite sprite;

  [SerializeField] int deployCost;

  [SerializeField] int attack;
  [SerializeField] int health;
  [SerializeField] int armor;

  [SerializeField] AbilityModel useAbility;
  [SerializeField] AbilityModel deployAbility;

  public FigurineRarity Rarity { get { return rarity; } }
  public string Name { get { return figureName; } }
  public int    DeployCost { get { return deployCost; } }
  public Sprite Sprite { get { return sprite; } }

  public int Attack { get { return attack; } }
  public int Health { get { return health; } }
  public int Armor { get { return armor; } }

  public AbilityModel CastAbility { get { return useAbility; } }
  public AbilityModel DeployAbility { get { return deployAbility; } }
}
