export type DamageType =
  | "Bludgeoning"
  | "Piercing"
  | "Slashing"
  | "Fire"
  | "Cold"
  | "Acid"
  | "Thunder"
  | "Lightning"
  | "Poison"
  | "Radiant"
  | "Necrotic"
  | "Psychic"
  | "Force";

export interface CharacterClass {
  name: string;
  hitDiceValue: number;
  classLevel: number;
}

export interface Character {
  id: number;
  name: string;
  level: number;
  hitPoints: number;
  currentHitPoints: number;
  tempHitPoints: number;
  classes: CharacterClass[];
  stats: {
    strength: number;
    dexterity: number;
    constitution: number;
    intelligence: number;
    wisdom: number;
    charisma: number;
  };
}
