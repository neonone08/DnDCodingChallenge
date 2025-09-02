import React, { useEffect, useState } from "react";
import CharacterView from "./components/CharacterView";
import CharacterControls from "./components/CharacterControls";
import { Character } from "./types";

const App: React.FC = () => {
  const [character, setCharacter] = useState<Character | null>(null);

  useEffect(() => {
    async function fetchCharacter() {
      const res = await fetch("/api/characters/1"); // example: ID=1
      if (res.ok) {
        const data = await res.json();
        setCharacter(data);
      }
    }
    fetchCharacter();
  }, []);

  if (!character) return <p>Loading...</p>;

  return (
    <main>
      <CharacterView character={character} />
      <CharacterControls
        characterId={character.id}
        onUpdate={(updated) => setCharacter(updated)}
      />
    </main>
  );
};

export default App;
