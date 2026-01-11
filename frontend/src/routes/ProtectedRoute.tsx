import { JSX, useEffect } from "react";
import { useAuth } from "../contexts/AuthContext";
import keycloak from "../keycloakTeste07";

export const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  const { initialized, isAuthenticated } = useAuth();

  if (!initialized) return <div>Carregando...</div>;

  if (!isAuthenticated) {
    useEffect(() => {
      keycloak.login();
    }, []);

    return <div>Redirecionando para login...</div>;
  }

  return children;
};