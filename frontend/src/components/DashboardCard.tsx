import { Box, Button, Flex, Heading, Icon, Text } from '@chakra-ui/react';
import { IconType } from 'react-icons';

interface DashboardCardProps {
  title: string;
  description?: string;
  icon: IconType;
  buttonText: string;
  buttonAction: () => void;
  buttonVariant?: 'solid' | 'outline';
  buttonColorScheme?: string;
}

const DashboardCard = ({
  title,
  description,
  icon,
  buttonText,
  buttonAction,
  buttonVariant = 'outline',
  buttonColorScheme = 'blue',
}: DashboardCardProps) => {
  return (
    <Box
      borderWidth="1px"
      borderRadius="lg"
      boxShadow="md"
      p={6}
      bg="white"
      _hover={{ boxShadow: 'lg', cursor: 'pointer' }}
    >
      <Flex justifyContent="space-between" alignItems="center">
        <Heading fontSize="lg" fontWeight="bold" color={'black'}>
          {title}
        </Heading>
        <Icon as={icon} fontSize="2xl" color="black" />
      </Flex>
      {description && (
        <Text color="gray.500" mt={2}>
          {description}
        </Text>
      )}
      <Button
        onClick={buttonAction}
        mt={4}
        variant={buttonVariant}
        colorScheme={buttonColorScheme}
      >
        {buttonText}
      </Button>
    </Box>
  );
};

export default DashboardCard;
