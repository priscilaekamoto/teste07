import { Button } from "@chakra-ui/react";
import { IconType } from "react-icons";

interface AddButtonProps {
  text?: string; // texto do botão
  icon?: IconType; // ícone opcional
  colorPalette?: string; // cor do botão
  onClick: () => void;
  size?: "xs" | "sm" | "md" | "lg";
  offsetY?: number; // ajuste vertical opcional
  offsetX?: number; // ajuste horizontal opcional
}

const GenericButton = ({
  text = "",
  icon: Icon,
  colorPalette = "black",
  onClick,
  size = "sm",
  offsetY = 0,
  offsetX = 0,
}: AddButtonProps) => {
  return (
    <Button
      colorPalette={colorPalette}
      size={size}
      variant="solid"
      onClick={onClick}
      style={{ transform: `translateY(${offsetY}px)` }}
      marginLeft={offsetX}
    >
      {text}
      {Icon && <Icon style={{ marginLeft: 8 }} />}
    </Button>
  );
};

export default GenericButton;
