import React, { useEffect, useState } from "react";
import "./index.css";
import { NavBar } from "./NavBar/NavBar";
import { Tier } from "./TierList/Tier";
import { TierList } from "./TierList/TierList";
import { GameItem } from "./TierList/GameItem";
import GamePool from "./GamePool";
import GameSearch from "./GameSearch";
import { getTierColor } from "./Helpers/getTierColor";
import {
  DndContext,
  DragEndEvent,
  DragOverlay,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import {
  SortableContext,
  arrayMove,
  rectSortingStrategy,
} from "@dnd-kit/sortable";

export type Game = {
  id: number;
  name: string;
  background_image: string;
  tier: string | null;
  order: number;
};

const LOCAL_STORAGE_KEY = "tierlist-games";

function App() {
  const [games, setGames] = useState<Game[]>(() => {
    const stored = localStorage.getItem(LOCAL_STORAGE_KEY);
    return stored ? JSON.parse(stored) : [];
  });
  const [activeId, setActiveId] = useState<string | null>(null);

  useEffect(() => {
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(games));
  }, [games]);

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 0,
      },
    })
  );

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over) return;

    const activeId = String(active.id);
    const overId = String(over.id);

    const tierLabels = ["S", "A", "B", "C", "D", "pool"];

    setGames((prev) => {
      const activeGame = prev.find((g) => String(g.id) === activeId);
      if (!activeGame) return prev;

      let newTier: string | null = null;
      if (overId === "pool") {
        newTier = null;
      } else if (tierLabels.includes(overId)) {
        newTier = overId === "pool" ? null : overId;
      } else {
        const overGame = prev.find((g) => String(g.id) === overId);
        newTier = overGame ? overGame.tier : null;
      }

      const sourceTier = activeGame.tier;
      const targetTier = newTier;

      let updated = [...prev];

      if (sourceTier === targetTier) {
        const tierGames = updated
          .filter((g) => (sourceTier ?? "pool") === (g.tier ?? "pool"))
          .sort((a, b) => a.order - b.order);

        const oldIndex = tierGames.findIndex((g) => String(g.id) === activeId);
        const newIndex = tierGames.findIndex((g) => String(g.id) === overId);

        const reordered = arrayMove(tierGames, oldIndex, newIndex);

        reordered.forEach((g, i) => {
          const idx = updated.findIndex((x) => x.id === g.id);
          updated[idx] = { ...g, order: i };
        });
      } else {
        updated = updated.map((g) =>
          String(g.id) === activeId
            ? { ...g, tier: targetTier, order: Date.now() }
            : g
        );
      }

      return updated;
    });
  };

  const handleAddGame = (game: Game) => {
    if (!games.find((g) => g.id === game.id)) {
      const poolGames = games.filter((g) => g.tier === null);
      const maxOrder =
        poolGames.length > 0 ? Math.max(...poolGames.map((g) => g.order)) : 0;

      setGames((prev) => [
        ...prev,
        { ...game, tier: null, order: maxOrder + 1 },
      ]);
    }
  };

  const handleRemoveGame = (id: number) => {
    setGames((prev) => prev.filter((game) => game.id !== id));
  };

  return (
    <div>
      <NavBar />

      <DndContext
        sensors={sensors}
        onDragEnd={handleDragEnd}
        onDragStart={(event) => setActiveId(String(event.active.id))}
      >
        <TierList>
          {["S", "A", "B", "C", "D"].map((label) => (
            <SortableContext
              key={label}
              id={label}
              items={games
                .filter((g) => g.tier === label)
                .map((g) => String(g.id))}
              strategy={rectSortingStrategy}
            >
              <Tier label={label} bgColor={getTierColor(label)}>
                {games
                  .filter((g) => g.tier === label)
                  .sort((a, b) => a.order - b.order)
                  .map((g) => (
                    <GameItem
                      id={String(g.id)}
                      key={g.id}
                      title={g.name}
                      image={g.background_image}
                      onRemove={handleRemoveGame}
                    />
                  ))}
              </Tier>
            </SortableContext>
          ))}
        </TierList>

        <SortableContext
          id="pool"
          items={games.filter((g) => g.tier === null).map((g) => String(g.id))}
          strategy={rectSortingStrategy}
        >
          <GamePool>
            {games
              .filter((g) => g.tier === null)
              .sort((a, b) => a.order - b.order)
              .map((g) => (
                <GameItem
                  id={String(g.id)}
                  key={g.id}
                  title={g.name}
                  image={g.background_image}
                  onRemove={handleRemoveGame}
                />
              ))}
          </GamePool>
        </SortableContext>

        <DragOverlay>
          {activeId ? (
            <GameItem
              id={activeId}
              title={games.find((g) => String(g.id) === activeId)?.name || ""}
              image={
                games.find((g) => String(g.id) === activeId)
                  ?.background_image || ""
              }
              onRemove={handleRemoveGame}
            />
          ) : null}
        </DragOverlay>
      </DndContext>

      <GameSearch onAdd={handleAddGame} />
    </div>
  );
}

export default App;
