import keycloakTeste07 from "@/keycloakTeste07";
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// Serviços de Pessoas
export const getPessoas = () => api.get("/Pessoas");
export const deletePessoaById = (id: number) => api.delete(`/Pessoas/${id}`);
export const createPessoa = (data: any) => { return api.post("/Pessoas", data); };
export const getPessoaById = (id: number) => api.get(`/Pessoas/${id}`);
export const getTotaisReceitasDespesasSaldoPorPessoa = () => api.get("/Pessoas/TotalReceitasDespesasSaldo");

// Serviços de Categorias
export const getCategorias = () => api.get("/Categorias");
export const createCategoria = (data: any) => api.post("/Categorias", data);
export const getTotalReceitasDespesasSaldoPorCategoria = () => api.get("/Categorias/TotalReceitasDespesasSaldo");

// Serviços de Transações
export const getTransacoes = () => api.get("/Transacoes");
export const createTransacao = (data: any) => { return api.post("/Transacoes", data); };

// interceptador para incluir token no futuro
api.interceptors.request.use(
  (config) => {
    
    const token = keycloakTeste07.token;
    if (token && config.headers) {
      // config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;
