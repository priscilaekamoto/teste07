import { Button } from "@chakra-ui/react";
import { FiArrowLeft } from "react-icons/fi";
import { useNavigate } from "react-router-dom";

interface BackButtonProps {
  to?: string; // caminho para onde navegar
  colorPalette?: string; // cor do botão
  size?: "xs" | "sm" | "md" | "lg";
  ariaLabel?: string;
}

const BackButton = ({
  to = "/",
  colorPalette = "black",
  size = "sm",
  ariaLabel = "Voltar",
}: BackButtonProps) => {
  const navigate = useNavigate();

  return (
    <Button
      onClick={() => navigate(to)}
      colorPalette={colorPalette}
      size={size}
      variant="solid"
      aria-label={ariaLabel}
    >
      <FiArrowLeft />
    </Button>
  );
};

export default BackButton;
