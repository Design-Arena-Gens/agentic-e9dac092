import { useMemo } from "react";
import { useReminders } from "./use-reminders";
import { useProjects } from "../projects/use-projects";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { Badge } from "../../components/ui/badge";
import { formatDistanceToNow } from "date-fns";

const reminderLabels: Record<string, string> = {
  InvoiceDue: "Invoice Due",
  TimesheetMissing: "Missing Timesheet",
  MilestoneUpcoming: "Milestone Upcoming",
  ChangeRequestPending: "CR Pending",
  StatementOfWorkExpiry: "SOW Expiry",
};

export function RemindersPage() {
  const { data: reminders, isLoading, isError, error } = useReminders();
  const { data: projects } = useProjects();

  const pendingReminders = useMemo(
    () => reminders?.filter((reminder) => !reminder.isSent) ?? [],
    [reminders]
  );

  if (isLoading) {
    return <div className="text-muted-foreground">Loading reminders…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load reminders: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Operational Reminders</CardTitle>
          <CardDescription>Automated alerts for invoices, milestones, and delivery commitments.</CardDescription>
        </CardHeader>
        <CardContent>
          <ReminderTable reminders={pendingReminders} projects={projects} emptyLabel="No pending reminders." />
        </CardContent>
      </Card>
    </div>
  );
}

interface ReminderTableProps {
  reminders: ReturnType<typeof useReminders>["data"];
  projects: ReturnType<typeof useProjects>["data"];
  emptyLabel: string;
}

function ReminderTable({ reminders, projects, emptyLabel }: ReminderTableProps) {
  const projectLookup = useMemo(() => {
    if (!projects) return new Map<string, string>();
    return new Map(projects.map((project) => [project.id, project.name]));
  }, [projects]);

  if (!reminders || reminders.length === 0) {
    return (
      <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
        {emptyLabel}
      </div>
    );
  }

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Type</TableHead>
          <TableHead>Project</TableHead>
          <TableHead>When</TableHead>
          <TableHead>Status</TableHead>
          <TableHead>Message</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {reminders.map((reminder) => (
          <TableRow key={reminder.id}>
            <TableCell>{reminderLabels[reminder.type] ?? reminder.type}</TableCell>
            <TableCell>{reminder.projectId ? projectLookup.get(reminder.projectId) ?? reminder.projectId : "—"}</TableCell>
            <TableCell>
              {formatDistanceToNow(new Date(reminder.scheduledFor), { addSuffix: true })}
            </TableCell>
            <TableCell>
              <Badge variant={reminder.isSent ? "secondary" : "default"}>
                {reminder.isSent ? "Sent" : "Scheduled"}
              </Badge>
            </TableCell>
            <TableCell className="max-w-md truncate text-sm text-muted-foreground">
              {reminder.message ?? "—"}
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
