import {
    Box,
    Button,
    Card,
    Flex,
    Grid,
    Heading,
    HStack,
    Icon,
    Stack,
    Text,
    useBreakpointValue,
} from '@chakra-ui/react';
import {
    FiArrowLeft,
    FiCalendar,
    FiDollarSign,
    FiFile,
    FiType,
    FiUser,
} from 'react-icons/fi';
import { useNavigate } from 'react-router-dom';
import Layout from '../../components/Layout';
import { useEffect, useState } from 'react';
import { getTransacaoById } from "../../data/services/api";
import { useParams } from "react-router-dom";
import { Categoria, Pessoa } from '../../types';
import { formatDate } from '@/utils';

interface Transacao {
    id: number;
    pessoa: Pessoa;
    categoria: Categoria;
    descricao: string;
    valor: number;
    tipo: number;
    recorrencia: number;
    dataInicio: string;
    dataFim: string;
}

const TransacaoDetail = () => {
    const navigate = useNavigate();
    const padding = useBreakpointValue({ base: 4, md: 8 });

    const tipo: Record<number, string> = {
        1: "Receita",
        2: "Despesa"
    };

    const recorrencia: Record<number, string> = {
        1: "Diária",
        2: "Semanal",
        3: "Mensal",
        4: "Anual"
    };


    const [transacao, setTransacao] = useState<Transacao | null>(null);
    const { id } = useParams<{ id: string }>();

    const fetchTransacao = async (id: number) => {
        try {
            const response = await getTransacaoById(id); // Ajuste o segundo parâmetro conforme necessário
            setTransacao(response.data);
        } catch (error) {
            console.error("Erro ao buscar transação:", error);
        }
    };

    console.log("Transação:", transacao);
    // Buscar a transação quando o componente for montado ou quando o ID mudar
    useEffect(() => {
        if (id) {
            fetchTransacao(Number(id));
        }
    }, [id]);

    return (
        <Layout>
            <Box p={padding}>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <Button onClick={() => navigate(`/transacoes`)} mr={4}>
                            <Icon as={FiArrowLeft} fontSize="2xl" />
                        </Button>
                        <Heading fontSize="2xl">Transações</Heading>
                    </HStack>
                </Flex>

                <Grid
                    templateColumns={{ base: '1fr', md: 'repeat(2, 1fr)' }}
                    gap={6}
                    mt={6}
                >
                    {/* Card com dados da ordem de serviço */}
                    <Card.Root width="100%">
                        <Card.Body gap="2">
                            <Card.Title mt="2">Dados da Transação</Card.Title>
                            <Card.Description>
                                <Stack gap={3} margin={3}>
                                    {/* Número da OS */}
                                    <HStack >
                                        <Icon as={FiUser} fontSize="xl" />
                                        <Text fontWeight="bold">Nome: </Text>
                                        <Text>{transacao?.pessoa?.nome}</Text>
                                    </HStack>

                                    {/* Nome do cliente */}
                                    <HStack >
                                        <Icon as={FiFile} fontSize="xl" />
                                        <Text fontWeight="bold">Descrição: </Text>
                                        <Text>{transacao?.descricao}</Text>
                                    </HStack>

                                    <HStack >
                                        <Icon as={FiDollarSign} fontSize="xl" />
                                        <Text fontWeight="bold">Valor: </Text>
                                        <Text>{transacao?.valor}</Text>
                                    </HStack>

                                    <HStack >
                                        <Icon as={FiType} fontSize="xl" />
                                        <Text fontWeight="bold">Categoria: </Text>
                                        <Text>{transacao?.categoria?.descricao}</Text>
                                    </HStack>

                                    <HStack align="start" flexWrap="wrap">
                                        <Icon as={FiType} fontSize="xl" mt="1" />
                                        <Text fontWeight="bold">Tipo:</Text>
                                        <Text>{tipo[transacao?.tipo ?? -1] || "Tipo não especificado"}</Text>

                                    </HStack>

                                    <HStack align="start" flexWrap="wrap">
                                        <Icon as={FiCalendar} fontSize="xl" mt="1" />
                                        <Text fontWeight="bold">Recorrência:</Text>
                                        <Text>{recorrencia[transacao?.recorrencia ?? -1] || "Recorrência não especificada"}</Text>

                                    </HStack>

                                    <HStack align="start" flexWrap="wrap">
                                        <Icon as={FiCalendar} fontSize="xl" mt="1" />
                                        <Text fontWeight="bold">Data de Início:</Text>
                                        <Text>{formatDate(transacao?.dataInicio ?? "Data não especificada")}</Text>
                                    </HStack>

                                    <HStack align="start" flexWrap="wrap">
                                        <Icon as={FiCalendar} fontSize="xl" mt="1" />
                                        <Text fontWeight="bold">Data de Fim:</Text>
                                        <Text>{formatDate(transacao?.dataFim ?? "Data não especificada")}</Text>
                                    </HStack>


                                </Stack>
                            </Card.Description>
                        </Card.Body>
                    </Card.Root>

                    {/* Card de observações */}
                    <Card.Root width="100%">
                        <Card.Body gap="2">
                            <Card.Title mt="2">Observações</Card.Title>
                            <Card.Description>
                                Observações adicionais sobre a transação podem ser inseridas aqui.
                            </Card.Description>
                        </Card.Body>
                    </Card.Root>
                </Grid>
            </Box>
        </Layout>
    );
};

export default TransacaoDetail;
