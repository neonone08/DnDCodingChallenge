import React, { useState } from "react";
import styles from "../styles/CharacterControls.module.css";
import { DamageType, Character } from "../types";

interface Props {
  characterId: string;
  onUpdate: (updated: Character) => void;
}

const damageTypes: DamageType[] = [
  "Bludgeoning",
  "Piercing",
  "Slashing",
  "Fire",
  "Cold",
  "Acid",
  "Thunder",
  "Lightning",
  "Poison",
  "Radiant",
  "Necrotic",
  "Psychic",
  "Force",
];

const CharacterControls: React.FC<Props> = ({ characterId, onUpdate }) => {
  const [damageValue, setDamageValue] = useState<number>(0);
  const [damageType, setDamageType] = useState<DamageType>("Bludgeoning");
  const [healValue, setHealValue] = useState<number>(0);
  const [tempHpValue, setTempHpValue] = useState<number>(0);

  async function handleAction(endpoint: string, body: Record<string, unknown>) {
    const response = await fetch(
      `http://localhost:5151/api/Combat/${endpoint}`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      }
    );

    if (response.ok) {
      const updated = await response.json();
      onUpdate(updated);
    }
  }

  return (
    <div
      className={styles.controls}
      role="form"
      aria-label="Character controls"
    >
      {/* Deal Damage */}
      <div className={styles.controlGroup}>
        <label htmlFor="damageValue">Deal Damage</label>
        <input
          id="damageValue"
          className="char-input"
          type="number"
          value={damageValue}
          onChange={(e) => setDamageValue(Number(e.target.value))}
        />
        <select
          id="damageType"
          className="char-select"
          title="Damage Type"
          value={damageType}
          onChange={(e) => setDamageType(e.target.value as DamageType)}
        >
          {damageTypes.map((dt) => (
            <option key={dt} value={dt}>
              {dt}
            </option>
          ))}
        </select>
        <button
          className="character-btn"
          onClick={() =>
            handleAction("deal-damage", {
              name: characterId,
              amount: damageValue,
              damageType: damageType,
            })
          }
        >
          Deal Damage
        </button>
      </div>

      {/* Heal */}
      <div className={styles.controlGroup}>
        <label htmlFor="healValue">Heal</label>
        <input
          id="healValue"
          className="char-input"
          type="number"
          value={healValue}
          onChange={(e) => setHealValue(Number(e.target.value))}
        />
        <button
          className="character-btn"
          onClick={() => handleAction("heal", { name: characterId, amount: healValue })}
        >
          Heal
        </button>
      </div>

      {/* Temporary HP */}
      <div className={styles.controlGroup}>
        <label htmlFor="tempHpValue">Temporary HP</label>
        <input
          id="tempHpValue"
          className="char-input"
          type="number"
          value={tempHpValue}
          onChange={(e) => setTempHpValue(Number(e.target.value))}
        />
        <button
          className="character-btn"
          onClick={() => handleAction("temp-hp", { name: characterId, amount: tempHpValue })}
        >
          Set Temporary HP
        </button>
      </div>
    </div>
  );
};

export default CharacterControls;
