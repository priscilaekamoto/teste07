import { useEffect, useState } from "react";
import {
  Box,
  Flex,
  Heading,
  HStack,
  Table,
} from "@chakra-ui/react";
import Layout from "../../components/Layout";
import BackButton from "../../components/buttons/BackButton";
import { getTotalReceitasDespesasSaldoPorCategoria } from "../../data/services/api";
import { ToastContainer } from "react-toastify";
import { CategoriaTotal } from "@/types";
import { formatCurrency } from "@/utils";

const CategoriaTotaisList = () => {
  const [categoria, setCategoria] = useState<CategoriaTotal[]>([]);

  const fetchCategoria = async () => {
    try {
      const response = await getTotalReceitasDespesasSaldoPorCategoria();
      setCategoria(response.data);
    } catch (error) {
      console.error("Erro ao buscar totais :", error);
    }
  };

  useEffect(() => {
    fetchCategoria();
  }, []);

  const totalizador = categoria.reduce(
    (acc, item) => {
      acc.totalReceita += item.totalReceita;
      acc.totalDespesa += item.totalDespesa;
      acc.saldo += item.saldo;
      return acc;
    },
    {
      totalReceita: 0,
      totalDespesa: 0,
      saldo: 0,
    }
  );

  return (
    <Layout>
      <ToastContainer position="top-center" />

      <Box>
        <Flex justify="space-between" align="center" mb={4}>
          <HStack>
            <BackButton to="/" />
            <Heading fontSize="2xl">
              Total de Receita, Despesa e Saldo Por Categoria
            </Heading>
          </HStack>
        </Flex>

        <Box overflowX="auto" borderRadius="md" boxShadow="sm">
          <Table.Root size="md">
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Descrição</Table.ColumnHeader>
                <Table.ColumnHeader>Total Receita</Table.ColumnHeader>
                <Table.ColumnHeader>Total Despesa</Table.ColumnHeader>
                <Table.ColumnHeader>Saldo</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>

            <Table.Body>
              {categoria.map((item) => (
                <Table.Row
                  key={item.id}
                  _hover={{ bg: "gray.50", cursor: "pointer" }}
                >
                  <Table.Cell fontWeight="bold">
                    {item.descricao}
                  </Table.Cell>
                  <Table.Cell>
                    {formatCurrency(item.totalReceita)}
                  </Table.Cell>
                  <Table.Cell>
                    {formatCurrency(item.totalDespesa)}
                  </Table.Cell>
                  <Table.Cell
                    color={item.saldo < 0 ? "red.500" : "green.600"}
                  >
                    {formatCurrency(item.saldo)}
                  </Table.Cell>
                </Table.Row>
              ))}

              {/* Totalizador */}
              <Table.Row bg="gray.100" fontWeight="bold">
                <Table.Cell>Total</Table.Cell>
                <Table.Cell>
                  {formatCurrency(totalizador.totalReceita)}
                </Table.Cell>
                <Table.Cell>
                  {formatCurrency(totalizador.totalDespesa)}
                </Table.Cell>
                <Table.Cell
                  color={totalizador.saldo < 0 ? "red.600" : "green.700"}
                >
                  {formatCurrency(totalizador.saldo)}
                </Table.Cell>
              </Table.Row>
            </Table.Body>
          </Table.Root>
        </Box>
      </Box>
    </Layout>
  );
};

export default CategoriaTotaisList;
