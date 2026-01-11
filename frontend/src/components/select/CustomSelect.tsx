import React from "react";
import { Flex, Text } from "@chakra-ui/react";

type Option = {
  value: string | number;
  label: React.ReactNode;
  disabled?: boolean;
};
interface CustomSelectProps {
  label?: React.ReactNode;
  name?: string;
  options?: Option[];
  value?: string | number;
  onChange?: React.ChangeEventHandler<HTMLSelectElement>;
  disabled?: boolean;
  w?: string | { base?: string; md?: string }; // adicionado
  h?: string | { base?: string; md?: string }; // adicionado
}
const CustomSelect: React.FC<CustomSelectProps> = ({
  label,
  name,
  options = [],
  value,
  onChange,
  }) => {
  return (
    <Flex direction="column" mb={1}>
      <Text fontWeight="bold" w="auto">
        {label}
      </Text>
      <select
        name={name}
        value={value ?? ""}
        onChange={onChange}
        disabled={false}
        style={{
          width: "210px", // define largura fixa
          padding: "8px",
          borderRadius: "6px",
          border: "1px solid #CBD5E0", // cor de borda do Chakra
        }}
      >
         {/* Placeholder fixo */}
        <option value="" disabled>
          Selecione...
        </option>
        {options.map((option) => (
          <option key={option.value} value={option.value} disabled={option.disabled}>
            {option.label}
          </option>
        ))}
      </select>
    </Flex>
  );
};
export default CustomSelect;