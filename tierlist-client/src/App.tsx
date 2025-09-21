import React, { useState } from "react";
import "./index.css";
import { NavBar } from "./NavBar/NavBar";
import { Tier } from "./TierList/Tier";
import { TierList } from "./TierList/TierList";
import { GameItem } from "./TierList/GameItem";
import GamePool from "./GamePool";
import GameSearch from "./GameSearch";

export type Game = {
  id: number;
  name: string;
  background_image: string;
  tier: string | null;
};

function App() {
  const [games, setGames] = useState<Game[]>([]);

  const handleAddGame = (game: Game) => {
    if (!games.find((g) => g.id === game.id)) {
      setGames((prev) => [...prev, { ...game, tier: null }]);
    }
  };

  const handleRemoveGame = (id: number) => {
    setGames((prev) => prev.filter((game) => game.id !== id));
  };

  return (
    <div>
      <NavBar />

      <TierList>
        <Tier label="S" bgColor="#e74c3c" />
        <Tier label="A" bgColor="#f39c12" />
        <Tier label="B" bgColor="#f1c40f" />
        <Tier label="C" bgColor="#27ae60" />
        <Tier label="D" bgColor="#3498db" />
      </TierList>

      <GameSearch onAdd={handleAddGame} />

      <GamePool>
        {games.map((g) => (
          <GameItem
            id={g.id}
            key={g.id}
            title={g.name}
            image={g.background_image}
            onRemove={handleRemoveGame}
          />
        ))}
      </GamePool>
    </div>
  );
}

export default App;
