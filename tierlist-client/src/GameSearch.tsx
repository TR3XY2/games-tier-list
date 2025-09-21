import React, { useEffect, useState } from "react";
import { Game } from "./App";

type GameSearchProps = {
  onAdd: (game: Game) => void;
};

const API_KEY = process.env.REACT_APP_RAWG_API_KEY;

function GameSearch({ onAdd }: GameSearchProps) {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<Game[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (!query) {
      setResults([]);
      return;
    }

    const fetchGames = async () => {
      setLoading(true);
      try {
        const res = await fetch(
          `https://api.rawg.io/api/games?key=${API_KEY}&search=${query}&page_size=9`
        );
        const data = await res.json();

        const mapped: Game[] = (data.results || []).map((g: any) => ({
          id: g.id,
          name: g.name,
          background_image: g.background_image,
        }));

        setResults(mapped);
      } catch (err) {
        console.error("Error fetching games", err);
      } finally {
        setLoading(false);
      }
    };

    const timeout = setTimeout(fetchGames, 500);
    return () => clearTimeout(timeout);
  }, [query]);

  return (
    <div className="search-box">
      <input
        type="text"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        placeholder="Search for a game..."
        className="search-input"
      />

      {loading && <p style={{ color: "white" }}>Loading...</p>}

      <div className="search-grid">
        {results.map((game) => (
          <div key={game.id} className="search-card">
            <img src={game.background_image} alt={game.name} />
            <span className="search-title">{game.name}</span>
            <button onClick={() => onAdd(game)} className="search-add-btn">
              +
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default GameSearch;
