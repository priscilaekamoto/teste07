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
import { getTransacoes } from "../../data/services/api";
import { useNavigate } from "react-router-dom";
import AddButton from "../../components/buttons/GenericButton";
import { FiPlusCircle } from "react-icons/fi";
import { TransacaoData } from "@/types";
import { formatCurrency } from "@/utils";

const tipos = new Array();
tipos[1] = "Receita";
tipos[2] = "Despesa";

const TransacaoList = () => {
    const navigate = useNavigate();
    const [transacoes, setTransacoes] = useState<TransacaoData[]>([]);
    
    const fetchTransacoes = async () => {
        try {
            const response = await getTransacoes();
            const data = response.data;
            console.log("Transações buscadas:", data);
            setTransacoes(data);
            
        } catch (error) {
            console.error("Erro ao buscar transações:", error);
        }
    };

    useEffect(() => {
        fetchTransacoes();
    }, []);

    
    return (
        <Layout>
            <Box>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <BackButton to="/" />
                        <Heading fontSize="2xl">Transações</Heading>
                    </HStack>
                    <AddButton
                        text="Adicionar Transação"
                        icon={FiPlusCircle}
                        colorPalette="black"
                        onClick={() => navigate("/transacoes/cadastro")}
                    />
                </Flex>
                <Box overflowX="auto" borderRadius="md" boxShadow="sm">
                    <Table.Root>
                        <Table.Header>
                            <Table.Row>
                                <Table.ColumnHeader>Descrição</Table.ColumnHeader>
                                <Table.ColumnHeader>Valor</Table.ColumnHeader>
                                <Table.ColumnHeader>Tipo</Table.ColumnHeader>
                                <Table.ColumnHeader>Categoria</Table.ColumnHeader>
                                <Table.ColumnHeader>Pessoa</Table.ColumnHeader>
                            </Table.Row>
                        </Table.Header>

                        <Table.Body>
                            {transacoes?.map((transacao) => (
                                <Table.Row
                                    key={transacao?.id}
                                    _hover={{ bg: "gray.50", cursor: "pointer" }}
                                >
                                    <Table.Cell fontWeight="bold">{transacao?.descricao}</Table.Cell>
                                    <Table.Cell>{formatCurrency(transacao?.valor)}</Table.Cell>
                                    <Table.Cell>{tipos[transacao?.tipo]}</Table.Cell>
                                    <Table.Cell>{transacao?.categoria?.descricao}</Table.Cell>
                                    <Table.Cell>{transacao?.pessoa?.nome}</Table.Cell>
                                </Table.Row>
                            ))}
                        </Table.Body>
                    </Table.Root>
                </Box>
            </Box>
        </Layout>
    );
};

export default TransacaoList;
