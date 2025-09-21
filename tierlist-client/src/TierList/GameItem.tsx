type GameItemProps = {
  title: string;
  image: string;
};

export function GameItem({ title, image }: GameItemProps) {
  return (
    <div className="game-item">
      <img src={image} alt={title} className="game-img" />
      <p className="game-title">{title}</p>
    </div>
  );
}