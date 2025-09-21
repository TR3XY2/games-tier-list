import React, { useEffect, useState } from "react";
import "./index.css";
import { NavBar } from "./NavBar/NavBar";
import { Tier } from "./TierList/Tier";
import { TierList } from "./TierList/TierList";
import { GameItem } from "./TierList/GameItem";
import GamePool from "./GamePool";
import GameSearch from "./GameSearch";

type Game = {
  id: number;
  name: string;
  background_image: string;
};

function App() {
  const [gamePool, setGamePool] = useState<Game[]>([]);

  const handleAddGame = (game: Game) => {
    if (!gamePool.find((g) => g.id === game.id)) {
      setGamePool((prev) => [...prev, game]);
    }
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
        {gamePool.map((g) => (
          <GameItem key={g.id} title={g.name} image={g.background_image} />
        ))}
      </GamePool>
    </div>
  );
}

export default App;
