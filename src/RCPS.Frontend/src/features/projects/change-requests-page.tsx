import { useMemo, useState } from "react";
import { useProjects } from "./use-projects";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { Badge } from "../../components/ui/badge";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../../components/ui/select";
import { format } from "date-fns";

const statusVariant: Record<string, { label: string; variant: "default" | "secondary" | "destructive" | "outline" }> = {
  Draft: { label: "Draft", variant: "outline" },
  Submitted: { label: "Submitted", variant: "secondary" },
  Approved: { label: "Approved", variant: "default" },
  Rejected: { label: "Rejected", variant: "destructive" },
  Implemented: { label: "Implemented", variant: "default" },
};

export function ChangeRequestsPage() {
  const { data: projects, isLoading, isError, error } = useProjects();
  const [selectedProjectId, setSelectedProjectId] = useState<string>("all");

  const changeRequests = useMemo(() => {
    if (!projects) return [];

    return projects
      .filter((project) => selectedProjectId === "all" || project.id === selectedProjectId)
      .flatMap((project) =>
        project.changeRequests.map((cr) => ({
          ...cr,
          projectName: project.name,
        }))
      );
  }, [projects, selectedProjectId]);

  if (isLoading) {
    return <div className="text-muted-foreground">Loading change requests…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load change requests: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Change Requests</CardTitle>
          <CardDescription>Govern scope changes, approval status, and financial impact.</CardDescription>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="flex flex-col gap-2 sm:flex-row sm:items-center sm:justify-between">
            <div className="space-y-1">
              <span className="text-xs font-medium uppercase tracking-wide text-muted-foreground">
                Filter by project
              </span>
              <Select value={selectedProjectId} onValueChange={setSelectedProjectId}>
                <SelectTrigger className="w-full sm:w-64">
                  <SelectValue placeholder="Select a project" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="all">All projects</SelectItem>
                  {projects?.map((project) => (
                    <SelectItem key={project.id} value={project.id}>
                      {project.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
          </div>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Title</TableHead>
                <TableHead>Project</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Hours</TableHead>
                <TableHead>Amount</TableHead>
                <TableHead>Submitted</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {changeRequests.map((cr) => {
                const statusMeta = statusVariant[cr.status] ?? statusVariant.Draft;
                return (
                  <TableRow key={cr.id}>
                    <TableCell className="font-medium">{cr.title}</TableCell>
                    <TableCell className="text-sm text-muted-foreground">{cr.projectName}</TableCell>
                    <TableCell>
                      <Badge variant={statusMeta.variant}>{statusMeta.label}</Badge>
                    </TableCell>
                    <TableCell>{cr.estimatedHours.toFixed(1)}</TableCell>
                    <TableCell>${cr.estimatedAmount.toLocaleString()}</TableCell>
                    <TableCell>{format(new Date(cr.createdAtUtc), "MMM d, yyyy")}</TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
          {changeRequests.length === 0 && (
            <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
              No change requests in the selected view.
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  );
}
