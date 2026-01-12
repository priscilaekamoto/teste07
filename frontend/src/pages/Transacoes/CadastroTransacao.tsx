import React, { useState, useEffect } from "react";
import Layout from '../../components/Layout';
import { Flex, HStack, Heading, Box, Input } from "@chakra-ui/react";
import { FiPlusCircle } from "react-icons/fi";
import { useNavigate } from "react-router-dom";
import GenericButton from "../../components/buttons/GenericButton";
import { createTransacao, getCategorias, getPessoas } from "../../data/services/api";
import { toast, ToastContainer } from "react-toastify";
import CurrencyInput from 'react-currency-input-field';
import CustomSelect from '../../components/select/CustomSelect';
import BackButton from "@/components/buttons/BackButton";
import AddButton from "../../components/buttons/GenericButton";

function CadastroTransacao() {

    const navigate = useNavigate();

    const [mostrarErros, setMostrarErros] = useState(false);
    const [enums, setEnums] = useState<any>({});
    const [selecionarTipo, setSelecionarTipo] = useState<number | string>('');
    const [categorias, setCategorias] = useState<any[]>([]);
    const [selecionarCategoria, setSelecionarCategoria] = useState<number | string>('');
    const [pessoas, setPessoas] = useState<any[]>([]);
    const [selecionarPessoa, setSelecionarPessoa] = useState<number | string>('');

    const [formData, setFormData] = useState({
        descricao: '',
        valor: '',

    });

    const fetchTiposEnum = () => {
        const tipoEnum = {
            Tipo: [
                { value: 1, label: "Receita" },
                { value: 2, label: "Despesa" },
            ]
        };
        setEnums(tipoEnum);
    };

    useEffect(() => {
        fetchTiposEnum();
    }, []);

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

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const limpaFormulario = () => {
        setFormData({
            descricao: '',
            valor: '',
        });
    }

    const handleSubmit = async () => {

        if (!formData.descricao) {
            setMostrarErros(true);
            toast.warn("Por favor, preencha todos os campos antes de salvar.");
            return;
        }

        const payload = {
            descricao: formData.descricao,
            valor: Number(formData.valor.replace("R$ ", "").replace(",", ".")),
            tipo: Number(selecionarTipo),
            categoriaId: selecionarCategoria ? Number(selecionarCategoria) : null,
            pessoaId: selecionarPessoa ? Number(selecionarPessoa) : null,
        };
        try {
            const ret = await createTransacao(payload);
            
            if(ret.data.code !== 200) {
                ret.data.messages.forEach((msg: string) => {
                    toast.error(msg);
                });
                return;
            }

            toast.success("Transação criada com sucesso!");
            setMostrarErros(false);
            limpaFormulario();
            setSelecionarTipo('');
            setSelecionarCategoria('');
            setSelecionarPessoa('');
        } catch (error) {
            toast.error("Erro ao salvar a transação. Por favor, tente novamente.");
        }
    }

    return (
        <Layout>
            <Box>
                <Flex justify="space-between" align="center" mb={4}>
                    <HStack>
                        <BackButton to="/transacoes" />
                        <Heading fontSize="2xl">Cadastro de Transação</Heading>
                    </HStack>
                    <AddButton
                        text="Adicionar Transação"
                        icon={FiPlusCircle}
                        colorPalette="black"
                        onClick={() => navigate("/transacoes/cadastro")}
                    />
                </Flex>
                <form>
                    <Flex gap={8} mt={8} pl={20} align="start" h="100%">
                        <Flex direction="column" w="400px" h="100%">
                            <Box fontWeight="bold" mb={1}>Descrição</Box>
                            <Input placeholder="Descrição" size="md"
                                name="descricao"
                                value={formData.descricao}
                                onChange={handleChange}
                            />
                            {mostrarErros && !formData.descricao && <Box color="red.500" fontSize="sm" height={"100%"} visibility={mostrarErros && !formData.descricao ? "visible" : "hidden"}>Campo obrigatório</Box>}
                        </Flex>

                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Valor</Box>
                            <CurrencyInput
                                id="valor"
                                name="valor"
                                placeholder="R$ 0,00"
                                decimalsLimit={2}
                                prefix="R$ "
                                value={formData.valor}
                                onValueChange={(val) =>
                                    setFormData({ ...formData, valor: val || "" })
                                }
                                style={{
                                    height: "40px",
                                    padding: "0 12px",
                                    borderRadius: "8px",
                                    border: "1px solid #E2E8F0",
                                    fontSize: "16px",
                                    width: "200px",
                                }}
                            />
                            {mostrarErros && !formData.valor && (<Box color="red.500" fontSize="sm">Valor é obrigatório</Box>
                            )}

                        </Flex>
                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Tipo</Box>
                            <CustomSelect
                                name="selecionarTipo"
                                options={(enums as any)?.Tipo ?? []}
                                value={selecionarTipo}
                                onChange={(e) => {
                                    setSelecionarTipo(e.target.value)
                                }}
                                w="200px"
                            />
                            {mostrarErros && !selecionarTipo && (<Box color="red.500" w={300} fontSize="sm">Seleção de tipo é obrigatória</Box>)}
                        </Flex>


                        <ToastContainer position="top-right" />
                    </Flex>
                    <Flex gap={8} mt={8} pl={20} align="start" h="100%">

                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Categoria</Box>
                            <CustomSelect
                                name="selecionarCategoria"
                                options={categorias.map(categoria => ({
                                    value: categoria.id,
                                    label: categoria.descricao
                                }))}
                                value={selecionarCategoria}
                                onChange={(e) => {
                                    setSelecionarCategoria(e.target.value)
                                }}
                                w="200px"
                            />
                            {mostrarErros && !selecionarCategoria && (<Box color="red.500" w={300} fontSize="sm">Seleção de categoria é obrigatória</Box>)}
                        </Flex>


                        <Flex direction="column" w="200px" h="100%">
                            <Box fontWeight="bold" mb={1}>Pessoa</Box>
                            <CustomSelect
                                name="selecionarPessoa"
                                options={pessoas.map(pessoa => ({
                                    value: pessoa.id,
                                    label: pessoa.nome
                                }))}
                                value={selecionarPessoa}
                                onChange={(e) => {
                                    setSelecionarPessoa(e.target.value)
                                }}
                                w="400px"
                            />
                            {mostrarErros && !selecionarPessoa && (<Box color="red.500" w={300} fontSize="sm">Seleção de pessoa é obrigatória</Box>)}
                        </Flex>



                        <Flex h="100%">
                            <GenericButton
                                offsetY={27}
                                onClick={handleSubmit}
                                text="Salvar Transação"
                                colorPalette="black"
                                icon={FiPlusCircle}
                                size="md"
                            />
                        </Flex>
                    </Flex>
                </form>
            </Box>
        </Layout>
    );
}

export default CadastroTransacao;