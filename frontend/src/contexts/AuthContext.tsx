import { createContext, useContext, useEffect, useState } from "react";
import keycloakTeste07 from "../keycloakTeste07";

interface AuthContextType {
  initialized: boolean;
  isAuthenticated: boolean;
  token: string | undefined;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [initialized, setInitialized] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [token, setToken] = useState<string | undefined>();

  useEffect(() => {

    // Só inicializa se ainda não tiver sido inicializado
    if(keycloakTeste07.didInitialize) {
      return;
    }

    keycloakTeste07
      .init({
        checkLoginIframe: false,
        pkceMethod: "S256",
        onLoad: "check-sso",
      })
      .then((authenticated) => {
        setIsAuthenticated(authenticated);
        setToken(keycloakTeste07.token);
        setInitialized(true);

        // Atualização automática do token
        setInterval(() => {
          keycloakTeste07.updateToken(30).then((refreshed) => {
            if (refreshed) {
              setToken(keycloakTeste07.token);
            }
          });
        }, 20000);// TODO: Enviar para .env
      });
  }, []);

  return (
    <AuthContext.Provider
      value={{
        initialized,
        isAuthenticated,
        token,
        logout: () => {
          keycloakTeste07.logout({
            redirectUri: window.location.origin
          });
        }
          
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be used inside <AuthProvider>");
  return ctx;
};
