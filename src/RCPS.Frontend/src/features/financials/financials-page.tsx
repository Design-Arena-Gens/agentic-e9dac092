import { useMemo } from "react";
import { useDashboardSummary } from "../dashboard/use-dashboard-summary";
import { useProjects } from "../projects/use-projects";
import { Card, CardContent, CardHeader, CardTitle } from "../../components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../components/ui/table";
import { FinancialTrendChart } from "../../components/charts/financial-trend-chart";

export function FinancialsPage() {
  const { data: summary, isLoading: summaryLoading } = useDashboardSummary();
  const { data: projects, isLoading: projectsLoading } = useProjects();

  const topProjects = useMemo(() => {
    if (!projects) return [];
    return [...projects]
      .sort((a, b) => b.revenueRecognized - a.revenueRecognized)
      .slice(0, 5);
  }, [projects]);

  if (summaryLoading || projectsLoading) {
    return <div className="text-muted-foreground">Loading financial analytics…</div>;
  }

  if (!summary || !projects) {
    return <div className="text-destructive">Unable to load financial data.</div>;
  }

  return (
    <div className="space-y-6">
      <div className="grid gap-4 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Revenue vs. Cost Trend</CardTitle>
          </CardHeader>
          <CardContent className="h-[360px]">
            <FinancialTrendChart data={summary.monthlyFinancials} />
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Portfolio Snapshot</CardTitle>
          </CardHeader>
          <CardContent className="space-y-3 text-sm">
            <div className="flex items-center justify-between">
              <span>Total Recognized Revenue</span>
              <span className="font-semibold">${summary.totalRevenue.toLocaleString()}</span>
            </div>
            <div className="flex items-center justify-between">
              <span>Total Cost</span>
              <span className="font-semibold">${summary.totalCost.toLocaleString()}</span>
            </div>
            <div className="flex items-center justify-between">
              <span>Contribution Margin</span>
              <span className="font-semibold">
                ${(summary.grossMargin).toLocaleString()} ({((summary.grossMargin / summary.totalRevenue) * 100 || 0).toFixed(1)}%)
              </span>
            </div>
            <div className="flex items-center justify-between">
              <span>Active Projects</span>
              <span className="font-semibold">{summary.activeProjects}</span>
            </div>
          </CardContent>
        </Card>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Top Revenue Projects</CardTitle>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Project</TableHead>
                <TableHead>Client</TableHead>
                <TableHead>Revenue</TableHead>
                <TableHead>Cost</TableHead>
                <TableHead>Margin</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {topProjects.map((project) => (
                <TableRow key={project.id}>
                  <TableCell>{project.name}</TableCell>
                  <TableCell className="text-sm text-muted-foreground">{project.clientName}</TableCell>
                  <TableCell>${project.revenueRecognized.toLocaleString()}</TableCell>
                  <TableCell>${project.costIncurred.toLocaleString()}</TableCell>
                  <TableCell>{(project.grossMargin * 100).toFixed(1)}%</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
          {topProjects.length === 0 && (
            <div className="rounded-md border border-dashed p-6 text-center text-sm text-muted-foreground">
              No financial data available yet.
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  );
}
