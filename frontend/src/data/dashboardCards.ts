import { IconType } from 'react-icons';
import { FiUsers } from 'react-icons/fi';
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
  }
];
