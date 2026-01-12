import Home from '../pages/Home';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ProtectedRoute } from './ProtectedRoute';
import CadastroPessoa from '../pages/Pessoas/CadastroPessoa';
import PessoasList from '../pages/Pessoas/PessoasList';
import CategoriasList from '../pages/Categorias/CategoriasList';
import CadastroCategoria from '../pages/Categorias/CadastroCategoria';
import TransacaoList from '../pages/Transacoes/TransacaoList';

const AppRoutes = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
      
        <Route
          path="/pessoas"
          element={<ProtectedRoute><PessoasList /></ProtectedRoute>}
        />
        <Route
          path="/pessoas/cadastro"
          element={<ProtectedRoute><CadastroPessoa /></ProtectedRoute>}
        />

         <Route
          path="/categorias"
          element={<ProtectedRoute><CategoriasList /></ProtectedRoute>}
        />

        <Route
          path="/categorias/cadastro"
          element={<ProtectedRoute><CadastroCategoria /></ProtectedRoute>}
        />

         <Route
          path="/transacoes"
          element={<ProtectedRoute><TransacaoList /></ProtectedRoute>}
        />
      </Routes>
    </Router>
  );
};

export default AppRoutes;

