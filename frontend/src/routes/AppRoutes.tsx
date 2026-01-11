import Home from '../pages/Home';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import { ProtectedRoute } from './ProtectedRoute';
import CadastroPessoa from '../pages/Pessoas/CadastroPessoa';
import PessoasList from '../pages/Pessoas/PessoasList';

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
      </Routes>
    </Router>
  );
};

export default AppRoutes;

