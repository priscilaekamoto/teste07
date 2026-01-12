import { IconType } from 'react-icons';
import { FiClipboard, FiDollarSign, FiUsers } from 'react-icons/fi';
import { NavigateFunction } from 'react-router-dom';

interface DashboardCardData {
  title: string;
  description: string;
  icon: IconType;
  buttonText: string;
  buttonAction: (navigate: NavigateFunction) => void;
}

export const dashboardCards: DashboardCardData[] = [
  {
    title: 'Pessoas',
    description: 'Cadastro e gerenciamento de pessoas',
    icon: FiUsers,
    buttonText: 'Ver pessoas',
    buttonAction: (navigate) => navigate('/pessoas'),
  },

  {
    title: 'Categorias',
    description: 'Cadastro e gerenciamento de categorias',
    icon: FiClipboard,
    buttonText: 'Ver categorias',
    buttonAction: (navigate) => navigate('/categorias'),
  },

  {
    title: 'Transações',
    description: 'Cadastro e gerenciamento de transações',
    icon: FiDollarSign,
    buttonText: 'Ver transações',
    buttonAction: (navigate) => navigate('/transacoes'),
  }

];
