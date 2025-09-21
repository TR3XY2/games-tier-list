import React, { ReactNode } from "react";

type TierListProps = {
  children: ReactNode;
};

export function TierList({ children }: TierListProps) {
  return <div className="tier-list">{children}</div>;
}
