import { Grid, GridItem, Box, Text } from "@chakra-ui/react";

type DataGridProps = {
  headers: string[];          // nomes das colunas
  rows: (string | React.ReactNode)[][]; // cada linha é um array de valores ou componentes
  columnWidths?: string[];    // larguras opcionais das colunas
};

export function DataGrid({ headers, rows, columnWidths }: DataGridProps) {
  const template = columnWidths
    ? columnWidths.join(" ")
    : `repeat(${headers.length}, 1fr)`; // padrão: colunas iguais

  return (
    <Box border="1px solid" borderColor="gray.200" borderRadius="md" overflow="hidden">
      {/* Cabeçalho */}
      <Grid
        templateColumns={template}
        bg="gray.100"
        fontWeight="bold"
        borderBottom="1px solid"
        borderColor="gray.200"
      >
        {headers.map((header, i) => (
          <GridItem key={i} p={3}>
            {header}
          </GridItem>
        ))}
      </Grid>

      {/* Linhas */}
      {rows.map((row, i) => (
        <Grid
          key={i}
          templateColumns={template}
          borderBottom="1px solid"
          borderColor="gray.200"
          alignItems="center"
          _last={{ borderBottom: "none" }}
        >
          {row.map((cell, j) => (
            <GridItem key={j} p={3}>
              {typeof cell === "string" ? <Text>{cell}</Text> : cell}
            </GridItem>
          ))}
        </Grid>
      ))}
    </Box>
  );
}
