import React, { useState } from "react";
import "./index.css";
import { NavBar } from "./NavBar/NavBar";
import { Tier } from "./TierList/Tier";
import { TierList } from "./TierList/TierList";
import { GameItem } from "./TierList/GameItem";
import GamePool from "./GamePool";
import GameSearch from "./GameSearch";
import { getTierColor } from "./Helpers/getTierColor";

export type Game = {
  id: number;
  name: string;
  background_image: string;
  tier: string | null;
  order?: number;
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
        {["S", "A", "B", "C", "D"].map((label) => (
          <Tier key={label} label={label} bgColor={getTierColor(label)}>
            {games
              .filter((g) => g.tier === label)
              .map((g) => (
                <GameItem
                  id={g.id}
                  key={g.id}
                  title={g.name}
                  image={g.background_image}
                  onRemove={handleRemoveGame}
                />
              ))}
          </Tier>
        ))}
      </TierList>

      <GameSearch onAdd={handleAddGame} />

      <GamePool>
        {games
          .filter((g) => g.tier === null)
          .map((g) => (
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
