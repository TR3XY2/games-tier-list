import React, { ReactNode } from "react";

type TierProps = {
  label: string;
  bgColor: string;
  children?: ReactNode;
};

export function Tier({ label, bgColor, children }: TierProps) {
  return (
    <div className="tier-row">
      <div className="tier-label" style={{ backgroundColor: bgColor }}>
        {label}
      </div>
      <div className="tier-items">{children}</div>
    </div>
  );
}
