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
import { getTotaisReceitasDespesasSaldoPorPessoa } from "../../data/services/api";
import { ToastContainer } from "react-toastify";
import { PessoaTotal } from "@/types";
import { formatCurrency } from "@/utils";

const PessoasTotaisList = () => {
  const [pessoas, setPessoas] = useState<PessoaTotal[]>([]);

  const fetchPessoas = async () => {
    try {
      const response = await getTotaisReceitasDespesasSaldoPorPessoa();
      setPessoas(response.data);
    } catch (error) {
      console.error("Erro ao buscar totais :", error);
    }
  };

  useEffect(() => {
    fetchPessoas();
  }, []);

  const totalizador = pessoas.reduce(
    (acc, pessoa) => {
      acc.totalReceita += pessoa.totalReceita;
      acc.totalDespesa += pessoa.totalDespesa;
      acc.saldo += pessoa.saldo;
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
              Total de Receita, Despesa e Saldo Por Pessoa
            </Heading>
          </HStack>
        </Flex>

        <Box overflowX="auto" borderRadius="md" boxShadow="sm">
          <Table.Root size="md">
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Nome</Table.ColumnHeader>
                <Table.ColumnHeader>Total Receita</Table.ColumnHeader>
                <Table.ColumnHeader>Total Despesa</Table.ColumnHeader>
                <Table.ColumnHeader>Saldo</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>

            <Table.Body>
              {pessoas.map((pessoa) => (
                <Table.Row
                  key={pessoa.id}
                  _hover={{ bg: "gray.50", cursor: "pointer" }}
                >
                  <Table.Cell fontWeight="bold">
                    {pessoa.nome}
                  </Table.Cell>
                  <Table.Cell>
                    {formatCurrency(pessoa.totalReceita)}
                  </Table.Cell>
                  <Table.Cell>
                    {formatCurrency(pessoa.totalDespesa)}
                  </Table.Cell>
                  <Table.Cell
                    color={pessoa.saldo < 0 ? "red.500" : "green.600"}
                  >
                    {formatCurrency(pessoa.saldo)}
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

export default PessoasTotaisList;
