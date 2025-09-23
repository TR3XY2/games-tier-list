import React, { ReactNode } from "react";
import { useDroppable } from "@dnd-kit/core";

type GamePoolProps = {
  children: ReactNode;
};

function GamePool({ children }: GamePoolProps) {
  const { setNodeRef, isOver } = useDroppable({ id: "pool" });

  return (
    <div
      ref={setNodeRef}
      className="game-pool"
      style={{ outline: isOver ? "2px dashed #fff" : undefined }}
    >
      {children}
    </div>
  );
}

export default GamePool;
