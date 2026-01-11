import {
    Portal,
    Select,
    createListCollection,
    Wrap,
    WrapItem,
    Box,
} from "@chakra-ui/react";
import { Tag } from "@chakra-ui/react";
import React from "react";

export type Option = {
    value: string | number;
    label: string;
    disabled?: boolean;
};

interface MultiSelectProps {
    label: string;
    options: Option[];
    selected: Option[];
    onChange: (values: Option[]) => void;
    placeholder?: string;
    width?: string;
    size?: "sm" | "md" | "lg";
    showTags?: boolean; // se deve exibir tags com os itens selecionados
    disabled?: boolean | void | undefined;
}

const MultiSelect: React.FC<MultiSelectProps> = ({
    label,
    options,
    selected,
    onChange,
    placeholder = "Selecione...",
    disabled = false || undefined,
    width = "320px",
    size = "sm",
    showTags = true,
}) => {
    const collection = createListCollection({ items: options });

    return (
        <Box>
            <Select.Root
                multiple
                collection={collection}
                disabled={disabled}
                value={selected.map((s) => String(s.value))}
                onValueChange={(e) => {
                    const values = e.value as string[];
                    const selecionados = options.filter((o) =>
                        values.includes(String(o.value))
                    );
                    onChange(selecionados);
                }}
                size={size}
                width={width}
            
            >
                <Select.HiddenSelect />
                <Select.Label fontWeight="bold" fontFamily="body" fontSize="md">{label}</Select.Label>
                <Select.Control>
                    <Select.Trigger>
                        <Select.ValueText placeholder={placeholder} />
                    </Select.Trigger>
                    <Select.IndicatorGroup>
                        <Select.Indicator />
                    </Select.IndicatorGroup>
                </Select.Control>
                <Portal>
                    <Select.Positioner>
                        <Select.Content >
                            {collection.items.map((item) => (
                                <Select.Item item={item} key={item.value}>
                                    {item.label}
                                    <Select.ItemIndicator />
                                </Select.Item>
                            ))}
                        </Select.Content>
                    </Select.Positioner>
                </Portal>
            </Select.Root>

            {/* Exibe as tags dos selecionados */}
            {showTags && selected.length > 0 && (
                <Wrap mt={2}>
                    {selected.map((opt) => (
                        <WrapItem key={opt.value}>
                            <Tag.Root borderRadius="full" colorPalette="blue" variant="subtle">
                                <Tag.Label>{opt.label}</Tag.Label>
                            </Tag.Root>
                        </WrapItem>
                    ))}
                </Wrap>
            )}
        </Box>
    );
};

export default MultiSelect;
