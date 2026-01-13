export type Pessoa = {
  id: number;
  nome: string;
  idade: number;
};

export type PessoaTotal = {
    id: number;
    nome: string;
    totalReceita: number;
    totalDespesa: number;
    saldo: number;
}

export type Categoria = {
  id: number;
  descricao: string;
  finalidade: number;
};

export type CategoriaTotal = {
    id: number;
    descricao: string;
    totalReceita: number;
    totalDespesa: number;
    saldo: number;
}

export type Transacao = {
  id: number;
  descricao: string;
  valor: number;
  tipo: number;
  categoriaId: number;
  pessoaId: number;
};

export type ValidationErrors = {
  [field: string]: string[];
};

export type ErrorResponseData = {
  errors: ValidationErrors;
};

export enum Finalidade {
    Receita = 1,
    Despesa = 2,
    Ambas = 3,
}

export type CategoriaDb = {
    id: number;
    descricao: string;
    finalidade: Finalidade;
}

enum Tipo {
    Receita = 1,
    Despesa = 2
}

export type TransacaoData = {
    id: number;
    descricao: string;
    valor: number;
    tipo: Tipo;
    categoriaId: number;
    pessoaId: number;
    categoria?: Categoria;
    pessoa?: Pessoa;
}

export  type Option = { value: string | number; label: string };