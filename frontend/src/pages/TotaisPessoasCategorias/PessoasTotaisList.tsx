import { useEffect, useState } from "react";
import {
    Box,
    Flex,
    Heading,
    HStack,
    Table
} from "@chakra-ui/react";
import Layout from "../../components/Layout";
import BackButton from "../../components/buttons/BackButton";
import { getTotaisReceitasDespesasSaldoPorPessoa } from "../../data/services/api";
import { ToastContainer } from "react-toastify";

interface Pessoa {
    id: number;
    nome: string;
    totalReceita: number;
    totalDespesa: number;
    saldo: number;
}

const PessoasTotaisList = () => {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const fetchPessoas = async () => {
        try {
            const response = await getTotaisReceitasDespesasSaldoPorPessoa();
            const data = response.data;
            setPessoas(data);
        } catch (error) {
            console.error("Erro ao buscar totais :", error);
        }
    };

    useEffect(() => {
        fetchPessoas();
    }, []);

    return (
        <Layout>
            <ToastContainer position="top-center" />
            <Box>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <BackButton to="/" />
                        <Heading fontSize="2xl"> Total de Receita, Despesa e Saldo Por Pessoa</Heading>
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
                            {pessoas?.map((pessoa) => (
                                <Table.Row
                                    key={pessoa?.id}
                                    _hover={{ bg: "gray.50", cursor: "pointer" }}
                                >
                                    <Table.Cell fontWeight="bold">{pessoa?.nome}</Table.Cell>
                                    <Table.Cell>{pessoa?.totalReceita}</Table.Cell>
                                    <Table.Cell>{pessoa?.totalDespesa}</Table.Cell>
                                    <Table.Cell>{pessoa?.saldo}</Table.Cell>
                                </Table.Row>
                            ))}
                        </Table.Body>
                    </Table.Root>
                </Box>
            </Box>
        </Layout>
    );
};

export default PessoasTotaisList;