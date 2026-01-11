import { Container } from '@chakra-ui/react';
import { ReactNode } from 'react';
import Header from './Header';

type LayoutProps = {
  children: ReactNode;
};

const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <Header />
      <Container pt="80px" px={4}>
        {children}
      </Container>
    </>
  );
};

export default Layout;
