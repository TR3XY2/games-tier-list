import { useDroppable } from "@dnd-kit/core";
import React, { ReactNode } from "react";

type TierProps = {
  label: string;
  bgColor: string;
  children?: ReactNode;
};

export function Tier({ label, bgColor, children }: TierProps) {
  const { setNodeRef, isOver } = useDroppable({ id: label });

  return (
    <div
      ref={setNodeRef}
      className="tier-row"
      style={{
        backgroundColor: bgColor,
        opacity: isOver ? 0.8 : 1,
      }}
    >
      <div className="tier-label">{label}</div>
      <div className="tier-content">{children}</div>
    </div>
  );
}
