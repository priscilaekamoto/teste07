import { useEffect, useState } from "react";
import {
  Box,
  Flex,
  Heading,
  HStack,
  Table,
  useBreakpointValue,
} from "@chakra-ui/react";

import { useNavigate } from "react-router-dom";
import Layout from "../../components/Layout";
import BackButton from "../../components/buttons/BackButton";
import DeleteButton from "../../components/buttons/DeleteButton";
import AddButton from "../../components/buttons/GenericButton";
import { FiPlusCircle } from "react-icons/fi";
import { getPessoas } from "../../data/services/api";
import { toast, ToastContainer } from "react-toastify";
import { deletePessoaById } from "../../data/services/api";


interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

const PessoasList = () => {
  const navigate = useNavigate();
  const padding = useBreakpointValue({ base: 4, md: 8 });

  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const fetchPessoas = async () => {
    try {
      const response = await getPessoas();
      const data = response.data;
      setPessoas(data);
    } catch (error) {
      console.error("Erro ao buscar pessoas:", error);
    }
  };

  useEffect(() => {
    fetchPessoas();
  }, []);

  return (
    <Layout>
      <ToastContainer position="top-center" />
      <Box p={padding}>
        {/* Header */}
        <Flex justify="space-between" align="center" mb={4}>
          <HStack>
            <BackButton to="/" />
            <Heading fontSize="2xl">Pessoas</Heading>
          </HStack>
          <AddButton
            text="Adicionar Pessoa"
            icon={FiPlusCircle}
            colorPalette="black"
            onClick={() => navigate("/pessoas/cadastro")}
          />
        </Flex>

        {/* Tabela */}
        <Box overflowX="auto" borderRadius="md" boxShadow="sm">
          <Table.Root size="md">
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Nome</Table.ColumnHeader>
                <Table.ColumnHeader>Idade</Table.ColumnHeader>
                <Table.ColumnHeader>Ações</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>

            <Table.Body>
              {pessoas?.map((pessoa) => (
                <Table.Row
                  key={pessoa?.id}
                  _hover={{ bg: "gray.50", cursor: "pointer" }}
                >
                  <Table.Cell fontWeight="bold">{pessoa?.nome}</Table.Cell>
                  <Table.Cell>{pessoa?.idade}</Table.Cell>
                  {/* Coluna de ações */}
                  <Table.Cell>
                    <HStack gap={2}>
                      <DeleteButton
                        onClick={async () => {
                          try {
                            await deletePessoaById(pessoa.id);

                            setPessoas(prev =>
                              prev.filter(p => p.id !== pessoa.id)
                            );

                            toast.success("Pessoa deletada com sucesso!");
                          } catch (error) {
                            toast.error("Erro ao deletar pessoa");
                          }
                        }}
                        size="sm"
                      />
                      
                    </HStack>
                  </Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table.Root>
        </Box>
      </Box>
      <Box p={padding}>
        <Flex justify="center" align="center" mt={4} gap={2}>
          {/* <button
            disabled={pageNumber === 1}
            onClick={() => setPageNumber(prev => prev - 1)}
          >
            Anterior
          </button> */}

          {/* <span>{pageNumber} / {totalPages}</span>

          <button
            disabled={pageNumber === totalPages}
            onClick={() => setPageNumber(prev => prev + 1)}
          >
            Próxima
          </button> */}
        </Flex>
      </Box>
    </Layout>

  );
};

export default PessoasList;
