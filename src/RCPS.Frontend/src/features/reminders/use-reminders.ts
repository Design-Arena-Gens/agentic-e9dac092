import { useQuery } from "@tanstack/react-query";
import { fetcher } from "../../lib/api";
import type { ReminderDto } from "../../types/api";

export function useReminders() {
  return useQuery<ReminderDto[], Error>({
    queryKey: ["reminders"],
    queryFn: () => fetcher<ReminderDto[]>("/reminders"),
  });
}
