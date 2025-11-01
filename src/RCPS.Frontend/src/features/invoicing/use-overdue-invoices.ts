import { useQuery } from "@tanstack/react-query";
import { fetcher } from "../../lib/api";
import type { InvoiceDto } from "../../types/api";

export function useOverdueInvoices() {
  return useQuery<InvoiceDto[], Error>({
    queryKey: ["invoices", "overdue"],
    queryFn: () => fetcher<InvoiceDto[]>("/invoices/overdue"),
  });
}
