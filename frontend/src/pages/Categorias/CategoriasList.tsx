import { useEffect, useState } from "react";
import {
    Box,
    Flex,
    Heading,
    HStack,
    Table
} from "@chakra-ui/react";

import { useNavigate } from "react-router-dom";
import Layout from "../../components/Layout";
import BackButton from "../../components/buttons/BackButton";
import AddButton from "../../components/buttons/GenericButton";
import { FiPlusCircle } from "react-icons/fi";
import { getCategorias } from "../../data/services/api";
import { CategoriaDb, Finalidade } from "@/types";

const CategoriasList = () => {
    const navigate = useNavigate();
    const [categorias, setCategorias] = useState<CategoriaDb[]>([]);
    const fetchCategorias = async () => {
        try {
            const response = await getCategorias();
            const data = response.data;
            setCategorias(data);
        } catch (error) {
            console.error("Erro ao buscar categorias:", error);
        }
    };

    useEffect(() => {
        fetchCategorias();
    }, []);

    return (
        <Layout>
            <Box>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <BackButton to="/" />
                        <Heading fontSize="2xl">Categorias</Heading>
                    </HStack>
                    <AddButton
                        text="Adicionar Categoria"
                        icon={FiPlusCircle}
                        colorPalette="black"
                        onClick={() => navigate("/categorias/cadastro")}
                    />
                </Flex>
                <Box overflowX="auto" borderRadius="md" boxShadow="sm">
                    <Table.Root size="md">
                        <Table.Header>
                            <Table.Row>
                                <Table.ColumnHeader>Descrição</Table.ColumnHeader>
                                <Table.ColumnHeader>Finalidade</Table.ColumnHeader>
                            </Table.Row>
                        </Table.Header>

                        <Table.Body>
                            {categorias?.map((categoria) => (
                                <Table.Row
                                    key={categoria?.id}
                                    _hover={{ bg: "gray.50", cursor: "pointer" }}
                                >
                                    <Table.Cell fontWeight="bold">{categoria?.descricao}</Table.Cell>
                                    <Table.Cell> {Finalidade[categoria.finalidade]}</Table.Cell>

                                </Table.Row>
                            ))}
                        </Table.Body>
                    </Table.Root>
                </Box>
            </Box>
        </Layout>
    );
};

export default CategoriasList;
