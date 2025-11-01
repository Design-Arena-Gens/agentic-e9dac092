import { useQuery } from "@tanstack/react-query";
import { fetcher } from "../../lib/api";
import type { TeamMemberDto } from "../../types/api";

export function useTeamMembers() {
  return useQuery<TeamMemberDto[], Error>({
    queryKey: ["team-members"],
    queryFn: () => fetcher<TeamMemberDto[]>("/team-members"),
  });
}
