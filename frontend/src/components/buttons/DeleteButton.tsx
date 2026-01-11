import { IconButton } from "@chakra-ui/react";
import { FiTrash } from "react-icons/fi";
import { IconType } from "react-icons";

interface DeleteButtonProps {
  text?: string; // texto do botão
  icon?: IconType; // ícone opcional
  colorPalette?: string; // cor do botão
  onClick: () => void;
  size?: "xs" | "sm" | "md" | "lg";
  ariaLabel?: string;
}

const DeleteButton = ({ onClick, size = "xs", ariaLabel = "Excluir" }: DeleteButtonProps) => {
  return (
    <IconButton
      as={FiTrash}
      aria-label={ariaLabel}
      size={size}
      variant="ghost"
      colorPalette="black"
      onClick={(e) => {
        e.stopPropagation();
        onClick();
      }}
    />
  );
};

export default DeleteButton;
