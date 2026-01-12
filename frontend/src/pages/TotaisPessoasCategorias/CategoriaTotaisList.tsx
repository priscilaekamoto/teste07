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
import { getTotalReceitasDespesasSaldoPorCategoria } from "../../data/services/api";
import { ToastContainer } from "react-toastify";

interface Categoria {
    id: number;
    descricao: string;
    totalReceita: number;
    totalDespesa: number;
    saldo: number;
}

const CategoriaTotaisList = () => {

    const [categoria, setCategoria] = useState<Categoria[]>([]);
    const fetchCategoria = async () => {
        try {
            const response = await getTotalReceitasDespesasSaldoPorCategoria();
            const data = response.data;
            setCategoria(data);
        } catch (error) {
            console.error("Erro ao buscar totais :", error);
        }
    };

    useEffect(() => {
        fetchCategoria();
    }, []);

    return (
        <Layout>
            <ToastContainer position="top-center" />
            <Box>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <BackButton to="/" />
                        <Heading fontSize="2xl"> Total de Receita, Despesa e Saldo Por Categoria</Heading>
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
                            {categoria?.map((categoria) => (
                                <Table.Row
                                    key={categoria?.id}
                                    _hover={{ bg: "gray.50", cursor: "pointer" }}
                                >
                                    <Table.Cell fontWeight="bold">{categoria?.descricao}</Table.Cell>
                                    <Table.Cell>{categoria?.totalReceita}</Table.Cell>
                                    <Table.Cell>{categoria?.totalDespesa}</Table.Cell>
                                    <Table.Cell>{categoria?.saldo}</Table.Cell>
                                </Table.Row>
                            ))}
                        </Table.Body>
                    </Table.Root>
                </Box>
            </Box>
        </Layout>
    );
};

export default CategoriaTotaisList;