export type Pessoa = {
  id: number;
  nome: string;
  idade: number;
};

export type Categoria = {
  id: number;
  descricao: string;
  finalidade: number;
};

export type Transacao = {
  id: number;
  descricao: string;
  valor: number;
  tipo: number;
  categoriaId: number;
  pessoaId: number;
};

export  type Option = { value: string | number; label: string };