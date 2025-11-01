import { useMemo, useState } from "react";
import { useOverdueInvoices } from "./use-overdue-invoices";
import { useProjects } from "../projects/use-projects";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { Badge } from "../../components/ui/badge";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../../components/ui/select";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "../../components/ui/tabs";
import { format } from "date-fns";

export function InvoicingPage() {
  const { data: overdueInvoices, isLoading, isError, error } = useOverdueInvoices();
  const { data: projects } = useProjects();
  const [tab, setTab] = useState("overdue");
  const [projectFilter, setProjectFilter] = useState("all");

  const projectNameLookup = useMemo(() => {
    if (!projects) return new Map<string, string>();
    return new Map(projects.map((project) => [project.id, project.name]));
  }, [projects]);

  const projectInvoices = useMemo(() => {
    if (!projects) return [];
    return projects
      .filter((project) => projectFilter === "all" || project.id === projectFilter)
      .flatMap((project) =>
        project.invoices.map((invoice) => ({
          ...invoice,
          projectName: project.name,
        }))
      );
  }, [projects, projectFilter]);

  if (isLoading) {
    return <div className="text-muted-foreground">Loading invoicing data…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load invoices: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Invoicing</CardTitle>
          <CardDescription>Monitor overdue invoices and invoicing cadence by project.</CardDescription>
        </CardHeader>
        <CardContent className="space-y-4">
          <Tabs value={tab} onValueChange={setTab}>
            <TabsList>
              <TabsTrigger value="overdue">Overdue</TabsTrigger>
              <TabsTrigger value="portfolio">Portfolio</TabsTrigger>
            </TabsList>
            <TabsContent value="overdue" className="space-y-4">
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Invoice</TableHead>
                    <TableHead>Project</TableHead>
                    <TableHead>Due Date</TableHead>
                    <TableHead>Amount</TableHead>
                    <TableHead>Balance</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {overdueInvoices?.map((invoice) => (
                    <TableRow key={invoice.id}>
                      <TableCell className="font-medium">{invoice.invoiceNumber}</TableCell>
                      <TableCell className="text-sm text-muted-foreground">
                        {projectNameLookup.get(invoice.projectId) ?? invoice.projectId}
                      </TableCell>
                      <TableCell>{format(new Date(invoice.dueDate), "MMM d, yyyy")}</TableCell>
                      <TableCell>${(invoice.subtotal + invoice.taxAmount).toLocaleString()}</TableCell>
                      <TableCell>
                        ${((invoice.subtotal + invoice.taxAmount) - invoice.amountPaid).toLocaleString()}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
              {overdueInvoices?.length === 0 && (
                <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
                  No overdue invoices. Great job!
                </div>
              )}
            </TabsContent>
            <TabsContent value="portfolio" className="space-y-4">
              <div className="flex flex-col gap-2 sm:flex-row sm:items-center sm:justify-between">
                <Select value={projectFilter} onValueChange={setProjectFilter}>
                  <SelectTrigger className="w-full sm:w-64">
                    <SelectValue placeholder="Filter by project" />
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
                <Badge variant="outline">
                  Total invoices: {projectInvoices.length}
                </Badge>
              </div>
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Invoice</TableHead>
                    <TableHead>Project</TableHead>
                    <TableHead>Date</TableHead>
                    <TableHead>Status</TableHead>
                    <TableHead>Amount</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {projectInvoices.map((invoice) => (
                    <TableRow key={invoice.id}>
                      <TableCell>{invoice.invoiceNumber}</TableCell>
                      <TableCell>{invoice.projectName}</TableCell>
                      <TableCell>{format(new Date(invoice.invoiceDate), "MMM d, yyyy")}</TableCell>
                      <TableCell>
                        <Badge variant={invoice.status === "Paid" ? "default" : "secondary"}>
                          {invoice.status}
                        </Badge>
                      </TableCell>
                      <TableCell>${invoice.totalAmount.toLocaleString()}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
              {projectInvoices.length === 0 && (
                <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
                  No invoices for the selected project.
                </div>
              )}
            </TabsContent>
          </Tabs>
        </CardContent>
      </Card>
    </div>
  );
}
