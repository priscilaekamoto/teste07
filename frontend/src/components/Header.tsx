import { Button, Flex, Heading } from '@chakra-ui/react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from "../contexts/AuthContext";
import keycloakTeste07 from "../keycloakTeste07";

const Header = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { isAuthenticated, logout, initialized } = useAuth();

  const isHome = location.pathname === '/';

  const handleLogout = () => {
    logout();
  };

  const handleLogin = () => {
    keycloakTeste07.login({
      redirectUri: window.location.origin
    });
  };

  return (
    <Flex
      justifyContent="space-between"
      alignItems="center"
      position="fixed"
      top={0}
      left={0}
      right={0}
      bg="white"
      zIndex={10}
      p={4}
      boxShadow="md"
      as="header"
    >
      <Heading fontSize="2xl" color="black">
        Teste07
      </Heading>

      <Flex gap={4}>
        {!isHome && (
          <Button variant="ghost" onClick={() => navigate('/')}>
            Início
          </Button>
        )}

        <Button variant="ghost">Ajuda</Button>
        <Button variant="ghost">Configurações</Button>

        {/* Só mostra os botões quando auth já carregou */}
        {initialized && (
          <>
            {!isAuthenticated && (
              <Button colorScheme="green" variant="solid" onClick={handleLogin}>
                Login
              </Button>
            )}

            {isAuthenticated && (
              <Button colorScheme="red" variant="solid" onClick={handleLogout}>
                Logout
              </Button>
            )}
          </>
        )}
      </Flex>
    </Flex>
  );
};

export default Header;
