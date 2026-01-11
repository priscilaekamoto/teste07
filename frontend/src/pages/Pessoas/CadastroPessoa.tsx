import React, { useState } from "react";
import Layout from '../../components/Layout';
import { Flex, HStack, Button, Icon, Heading, Box, Input } from "@chakra-ui/react";
import { FiArrowLeft, FiPlusCircle } from "react-icons/fi";
import { useNavigate } from "react-router-dom";
import GenericButton from "../../components/buttons/GenericButton";
import { createPessoa } from "../../data/services/api";
import { toast, ToastContainer } from "react-toastify";

function CadastroPessoa() {
    const navigate = useNavigate();
    const [mostrarErros, setMostrarErros] = useState(false);
    const [formData, setFormData] = useState({
        nome: '',
        idade: ''
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const limpaFormulario = () => {
        setFormData({
            nome: '',
            idade: ''
        });
    }

    const handleSubmit = async () => {

        if (!formData.nome || !formData.idade) {
            setMostrarErros(true);
            toast.warn("Por favor, preencha todos os campos antes de salvar.");
            return;
        }

        const payload = {
            nome: formData.nome,
            idade: parseInt(formData.idade),
        };
        try {
            await createPessoa(payload);
            toast.success("Pessoa criada com sucesso!");
            setMostrarErros(false);
            limpaFormulario();
        } catch (error) {
            toast.error("Erro ao salvar a pessoa. Por favor, tente novamente.");
        }
    }

    return (
        <Layout>
            <Box>
                <form>
                    <Flex justify="space-between" align="center" mb={4}>
                        <HStack>
                            <Button onClick={() => navigate(`/pessoas`)} mr={4}>
                                <Icon as={FiArrowLeft} fontSize="2xl" />
                            </Button>
                            <Heading fontSize="2xl">Cadastro de Pessoa</Heading>
                        </HStack>
                    </Flex>

                    <Flex gap={8} mt={8} pl={20} align="start" h="100%">
                        <Flex direction="column" w="600px" h="100%">
                            <Box fontWeight="bold" mb={1}>Nome</Box>
                            <Input placeholder="Nome" size="md"
                                name="nome"
                                value={formData.nome}
                                onChange={handleChange}
                            />
                            {mostrarErros && !formData.nome && <Box color="red.500" fontSize="sm" height={"100%"} visibility={mostrarErros && !formData.nome  ? "visible" : "hidden"}>Campo obrigatório</Box>}
                        </Flex>

                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Idade</Box>
                            <Input placeholder="Idade" size="md" type="number"
                                name="idade"
                                value={formData.idade}
                                onChange={handleChange}
                            />
                            {mostrarErros && !formData.idade && <Box color="red.500" fontSize="sm" minH="18px" visibility={mostrarErros && !formData.idade ? "visible" : "hidden"}>Campo obrigatório</Box>}
                        </Flex>
                        <Flex h="100%">
                            <GenericButton
                                offsetY={27}
                                onClick={handleSubmit}
                                text="Salvar Pessoa"
                                colorPalette="black"
                                icon={FiPlusCircle}
                                size="md"
                            />
                        </Flex>
                        <ToastContainer position="top-right" />
                    </Flex>
                </form>
            </Box>
        </Layout>
    );
}

export default CadastroPessoa;