import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";

type GameItemProps = {
  id: string;
  title: string;
  image: string;
  onRemove: (id: number) => void;
};

export function GameItem({ id, title, image, onRemove }: GameItemProps) {
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({ id });

  const style: React.CSSProperties = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      className="game-item"
      {...attributes}
      {...listeners}
    >
      <img src={image} alt={title} className="game-img" />
      <p className="game-title">{title}</p>
      <button className="game-remove-btn" onClick={() => onRemove(Number(id))}>
        ×
      </button>
    </div>
  );
}
