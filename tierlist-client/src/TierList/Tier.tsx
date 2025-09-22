import React, { ReactNode } from "react";

type TierProps = {
  label: string;
  bgColor: string;
  children?: ReactNode;
};

export function Tier({ label, bgColor, children }: TierProps) {
  return (
    <div className="tier-row" style={{ backgroundColor: bgColor }}>
      <div className="tier-label">{label}</div>
      <div className="tier-content">{children}</div>
    </div>
  );
}
