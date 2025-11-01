import { useTeamMembers } from "./use-team-members";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { Badge } from "../../components/ui/badge";

export function TeamPage() {
  const { data: teamMembers, isLoading, isError, error } = useTeamMembers();

  if (isLoading) {
    return <div className="text-muted-foreground">Loading team insights…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load team members: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Delivery Team</CardTitle>
          <CardDescription>Manage billable capacity and staffing allocations.</CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Name</TableHead>
                <TableHead>Role</TableHead>
                <TableHead>Email</TableHead>
                <TableHead>Bill Rate</TableHead>
                <TableHead>Cost Rate</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {teamMembers?.map((member) => (
                <TableRow key={member.id}>
                  <TableCell>
                    <div className="font-medium">{member.firstName} {member.lastName}</div>
                  </TableCell>
                  <TableCell>
                    <Badge variant="secondary">{member.role ?? "N/A"}</Badge>
                  </TableCell>
                  <TableCell>{member.email ?? "—"}</TableCell>
                  <TableCell>${member.defaultBillRate.toFixed(2)}</TableCell>
                  <TableCell>${member.defaultCostRate.toFixed(2)}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
          {teamMembers?.length === 0 && (
            <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
              No team members configured yet.
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  );
}
