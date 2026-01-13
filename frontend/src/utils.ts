import { AxiosError } from "axios";
import { toast } from "react-toastify";

export function handleAxiosValidationError(error: unknown): boolean {
  if (!(error instanceof AxiosError)) {
    return false;
  }

  const errors = error.response?.data?.errors;

  if (!errors) {
    return false;
  }

  (Object.values(errors) as string[][]).forEach(messages => {
    messages.forEach(message => {
        toast.error(message);
    });
  });

  return true;
}

export function formatCurrency (value: number) 
{
  return value.toLocaleString("pt-BR", {
    style: "currency",
    currency: "BRL",
  });
}
    