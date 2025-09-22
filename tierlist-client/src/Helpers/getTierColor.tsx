export function getTierColor(label: string) {
  switch (label) {
    case "S":
      return "#e74c3c";
    case "A":
      return "#f39c12";
    case "B":
      return "#f1c40f";
    case "C":
      return "#27ae60";
    case "D":
      return "#3498db";
    default:
      return "#7f8c8d";
  }
}
