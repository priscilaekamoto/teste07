import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { Provider } from './components/ui/provider.tsx';
import AppRoutes from './routes/AppRoutes.tsx';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { AuthProvider } from './contexts/AuthContext.tsx';

const queryClient = new QueryClient()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider>
      <Provider>
        <QueryClientProvider client={queryClient}>
          <AppRoutes />
        </QueryClientProvider>
      </Provider>  
    </AuthProvider>
  </StrictMode>
);
