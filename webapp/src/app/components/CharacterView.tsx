import React from "react";
import styles from "../styles/CharacterView.module.css";
import { Character } from "../types";
import Briv from "../assets/images/Briv.png";
import Image from "next/image";

interface Props {
  character: Character;
}

const CharacterView: React.FC<Props> = ({ character }) => {
  return (
    <div className={styles.card} role="region" aria-label="Character details">
      <div className={styles.cardContent}>
        <Image
          src={Briv}
          width={120}
          height={120}
          alt={`${character.name} portrait`}
          className={styles.characterImage}
        />
        <div className={styles.details}>
          <h2 className={styles.name}>{character.name}</h2>
          <p className={styles.sub}>
            {character.classes.map((cls, idx) => (
              <span key={idx}>
                {cls.name} (d{cls.hitDiceValue}) Lv {cls.classLevel}
                {idx < character.classes.length - 1 ? " • " : ""}
              </span>
            ))}{" "}
            • Level {character.level}
          </p>

          <div className={styles.hpSection}>
            <p>
              <strong>HP:</strong> {character.currentHitPoints}/
              {character.hitPoints}
            </p>
            <p>
              <strong>Temporary HP:</strong> {character.tempHitPoints}
            </p>
          </div>

          <h3 className={styles.statsTitle}>Stats</h3>
          <ul className={styles.statsList}>
            {Object.entries(character.stats).map(([key, value]) => (
              <li key={key}>
                <span className={styles.statLabel}>{key}</span>: {value}
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default CharacterView;
