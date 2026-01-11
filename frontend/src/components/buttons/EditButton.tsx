import { IconButton } from "@chakra-ui/react";
import { FiEdit } from "react-icons/fi";

interface EditButtonProps {
  onClick: () => void;
  size?: "xs" | "sm" | "md" | "lg";
  ariaLabel?: string;
}

const EditButton = ({ onClick, size = "xs", ariaLabel = "Editar" }: EditButtonProps) => {
  return (
    <IconButton
      as={FiEdit}
      aria-label={ariaLabel}
      size={size}
      variant="ghost"
      colorPalette="black"
      onClick={(e) => {
        e.stopPropagation(); // impede que clique da linha dispare
        onClick();
      }}
    />
  );
};

export default EditButton;
