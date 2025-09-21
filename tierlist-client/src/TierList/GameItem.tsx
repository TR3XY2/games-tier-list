type GameItemProps = {
  id: number; 
  title: string;
  image: string;
  onRemove: (id: number) => void;
};

export function GameItem({id, title, image, onRemove }: GameItemProps) {
  return (
    <div className="game-item">
      <img src={image} alt={title} className="game-img" />
      <p className="game-title">{title}</p>
      <button className="game-remove-btn" onClick={() => onRemove(id)}>
        ×
      </button>
    </div>
  );
}
