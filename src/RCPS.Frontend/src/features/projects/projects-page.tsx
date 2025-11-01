import { useMemo, useState } from "react";
import { useProjects } from "./use-projects";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { Badge } from "../../components/ui/badge";
import { Input } from "../../components/ui/input";
import { Tabs, TabsList, TabsTrigger } from "../../components/ui/tabs";
import { cn } from "../../lib/utils";

const statusColors: Record<string, string> = {
  Active: "bg-emerald-500/10 text-emerald-600",
  Draft: "bg-slate-500/10 text-slate-600",
  OnHold: "bg-amber-500/10 text-amber-600",
  Completed: "bg-blue-500/10 text-blue-600",
  Cancelled: "bg-rose-500/10 text-rose-600",
};

const healthColors: Record<string, string> = {
  OnTrack: "bg-emerald-500/10 text-emerald-600",
  AtRisk: "bg-amber-500/10 text-amber-600",
  OffTrack: "bg-rose-500/10 text-rose-600",
};

export function ProjectsPage() {
  const { data: projects, isLoading, isError, error } = useProjects();
  const [search, setSearch] = useState("");
  const [tab, setTab] = useState("all");

  const filteredProjects = useMemo(() => {
    if (!projects) return [];
    return projects.filter((project) => {
      const matchesSearch = project.name.toLowerCase().includes(search.toLowerCase());
      const matchesTab = tab === "all" || project.status === tab;
      return matchesSearch && matchesTab;
    });
  }, [projects, search, tab]);

  if (isLoading) {
    return <div className="text-muted-foreground">Loading projects…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load projects: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Projects</CardTitle>
          <CardDescription>Track project delivery, health, and budget adherence.</CardDescription>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="flex flex-col gap-2 md:flex-row md:items-center md:justify-between">
            <Input
              placeholder="Search projects"
              className="md:w-64"
              value={search}
              onChange={(event) => setSearch(event.target.value)}
            />
            <Tabs value={tab} onValueChange={setTab} className="w-full md:w-auto">
              <TabsList>
                {[
                  { value: "all", label: "All" },
                  { value: "Active", label: "Active" },
                  { value: "OnHold", label: "On Hold" },
                  { value: "Completed", label: "Completed" },
                ].map((tabOption) => (
                  <TabsTrigger key={tabOption.value} value={tabOption.value}>
                    {tabOption.label}
                  </TabsTrigger>
                ))}
              </TabsList>
            </Tabs>
          </div>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Project</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Health</TableHead>
                <TableHead className="text-right">Budget ($)</TableHead>
                <TableHead className="text-right">Actual ($)</TableHead>
                <TableHead className="text-right">Margin</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {filteredProjects.map((project) => (
                <TableRow key={project.id}>
                  <TableCell className="font-medium">
                    <div>{project.name}</div>
                    <div className="text-xs text-muted-foreground">{project.clientName}</div>
                  </TableCell>
                  <TableCell>
                    <Badge
                      className={cn("capitalize", statusColors[project.status] ?? "")}
                      variant="outline"
                    >
                      {project.status}
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <Badge
                      className={cn("capitalize", healthColors[project.health] ?? "")}
                      variant="outline"
                    >
                      {project.health}
                    </Badge>
                  </TableCell>
                  <TableCell className="text-right">${project.budgetAmount.toLocaleString()}</TableCell>
                  <TableCell className="text-right">${project.actualAmount.toLocaleString()}</TableCell>
                  <TableCell className="text-right">
                    {(project.grossMargin * 100).toFixed(1)}%
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
          {filteredProjects.length === 0 && (
            <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
              No projects match the current filter.
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  );
}
