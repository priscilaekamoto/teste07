import React, { useState, useEffect } from "react";
import Layout from '../../components/Layout';
import { Flex, HStack, Heading, Box, Input } from "@chakra-ui/react";
import { FiPlusCircle } from "react-icons/fi";
import GenericButton from "../../components/buttons/GenericButton";
import { createCategoria } from "../../data/services/api";
import { toast, ToastContainer } from "react-toastify";
import CustomSelect from '../../components/select/CustomSelect';
import BackButton from "@/components/buttons/BackButton";
import { handleAxiosValidationError } from "@/utils";

function CadastroCategoria() {

    const [mostrarErros, setMostrarErros] = useState(false);
    const [enums, setEnums] = useState<any>({});
    const [selecionarFinalidade, setSelecionarFinalidade] = useState<number | string>('');
    const [formData, setFormData] = useState({ descricao: ''});
    const fetchFinalidadesEnum = () => {
        const finalidadeEnum = {
            Finalidade: [
                { value: 1, label: "Receita" },
                { value: 2, label: "Despesa" },
                { value: 3, label: "Ambas" }
            ]
        };
        setEnums(finalidadeEnum);
    };

    useEffect(() => {
        fetchFinalidadesEnum();
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const limpaFormulario = () => {
        setFormData({
            descricao: '',
        });
    }

    const handleSubmit = async () => {

        if (!formData.descricao.trim()) {
            setMostrarErros(true);
            toast.warn("Por favor, preencha todos os campos antes de salvar.");
            return;
        }

        const payload = {
            descricao: formData.descricao,
            finalidade: Number(selecionarFinalidade),
        };
        try {
            await createCategoria(payload);
            toast.success("Categoria criada com sucesso!");
            setMostrarErros(false);
            limpaFormulario();
            setSelecionarFinalidade('');
        } catch (error) {
            // Erros Validators ou Exceptions
            handleAxiosValidationError(error);
        }
    }

    return (
        <Layout>
            <Box>
                <form>
                    <Flex justify="space-between" align="center" mb={4}>
                        <HStack>
                            <BackButton to="/categorias" />
                            <Heading fontSize="2xl">Cadastro de Categoria</Heading>
                        </HStack>
                    </Flex>

                    <Flex gap={8} mt={8} pl={20} align="start" h="100%">
                        <Flex direction="column" w="400px" h="100%">
                            <Box fontWeight="bold" mb={1}>Descrição</Box>
                            <Input placeholder="Descrição" size="md"
                                name="descricao"
                                value={formData.descricao}
                                onChange={handleChange}
                            />
                            {mostrarErros && !formData.descricao.trim() && <Box color="red.500" fontSize="sm" height={"100%"} visibility={mostrarErros && !formData.descricao.trim() ? "visible" : "hidden"}>Campo obrigatório</Box>}
                        </Flex>

                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Finalidade</Box>
                            <CustomSelect
                                name="selecionarFinalidade"
                                options={(enums as any)?.Finalidade ?? []}
                                value={selecionarFinalidade}
                                onChange={(e) => {
                                    setSelecionarFinalidade(e.target.value)
                                }}
                                w="200px"
                            />
                            {mostrarErros && !selecionarFinalidade && (<Box color="red.500" w={300} fontSize="sm">Seleção de finalidade é obrigatória</Box>)}

                        </Flex>
                        <Flex h="100%">
                            <GenericButton
                                offsetY={27}
                                onClick={handleSubmit}
                                text="Salvar Categoria"
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

export default CadastroCategoria;