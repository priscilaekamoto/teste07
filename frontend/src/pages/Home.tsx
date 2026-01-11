import { Grid } from '@chakra-ui/react';
import { useNavigate } from 'react-router-dom';
import DashboardCard from '../components/DashboardCard';
import { dashboardCards } from '../data/dashboardCards';
import Layout from '../components/Layout';

const Home = () => {
  const navigate = useNavigate();

  return (
    <Layout>
      <Grid
        templateColumns={{
          base: '1fr',
          sm: 'repeat(2, 1fr)',
          md: 'repeat(3, 1fr)',
          lg: 'repeat(4, 1fr)',
        }}
        gap={6}
        mt={6}
      >
        {dashboardCards.map(
          ({ title, description, icon, buttonText, buttonAction }) => (
            <DashboardCard
              key={title}
              title={title}
              description={description}
              icon={icon}
              buttonText={buttonText}
              buttonAction={() => buttonAction(navigate)}
              buttonVariant="outline"
              buttonColorScheme="blue"
            />
          )
        )}
      </Grid>
    </Layout>
  );
};

export default Home;
