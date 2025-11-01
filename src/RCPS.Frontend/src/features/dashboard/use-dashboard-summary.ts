import { useQuery } from "@tanstack/react-query";
import { fetcher } from "../../lib/api";
import type { DashboardSummaryDto } from "../../types/api";

export function useDashboardSummary() {
  return useQuery<DashboardSummaryDto, Error>({
    queryKey: ["dashboard", "summary"],
    queryFn: () => fetcher<DashboardSummaryDto>("/dashboard/summary"),
    staleTime: 60_000,
  });
}
