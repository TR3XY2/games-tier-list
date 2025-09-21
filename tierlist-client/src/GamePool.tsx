import React, { ReactNode } from "react";

type GamePoolProps = {
  children: ReactNode;
};

function GamePool({ children }: GamePoolProps) {
  return <div className="game-pool">{children}</div>;
}

export default GamePool;