import { useQuery } from "@tanstack/react-query";
import { fetcher } from "../../lib/api";
import type { ProjectDto } from "../../types/api";

export function useProjects() {
  return useQuery<ProjectDto[], Error>({
    queryKey: ["projects"],
    queryFn: () => fetcher<ProjectDto[]>("/projects"),
  });
}
