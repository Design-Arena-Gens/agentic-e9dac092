import { useMemo } from "react";
import { useDashboardSummary } from "./use-dashboard-summary";
import { Card, CardContent, CardHeader, CardTitle } from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { FinancialTrendChart } from "../../components/charts/financial-trend-chart";
import { GrossMarginRadial } from "../../components/charts/gross-margin-radial";
import { Separator } from "../../components/ui/separator";

export function DashboardPage() {
  const { data, isLoading, isError, error } = useDashboardSummary();

  const grossMarginPercent = useMemo(() => {
    if (!data) return 0;
    if (data.totalRevenue === 0) return 0;
    return (data.grossMargin / data.totalRevenue) * 100;
  }, [data]);

  if (isLoading) {
    return <div className="text-muted-foreground">Loading dashboard metrics…</div>;
  }

  if (isError) {
    return <div className="text-destructive">Failed to load dashboard: {error.message}</div>;
  }

  if (!data) {
    return <div className="text-muted-foreground">No dashboard data available.</div>;
  }

  return (
    <div className="space-y-6">
      <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        <Card>
          <CardHeader>
            <CardTitle>Total Revenue</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="text-3xl font-bold">${data.totalRevenue.toLocaleString()}</div>
            <p className="text-sm text-muted-foreground">Recognized revenue YTD</p>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Total Cost</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="text-3xl font-bold">${data.totalCost.toLocaleString()}</div>
            <p className="text-sm text-muted-foreground">Delivery cost incurred</p>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Gross Margin</CardTitle>
          </CardHeader>
          <CardContent className="flex items-end justify-between">
            <div>
              <div className="text-3xl font-bold">${data.grossMargin.toLocaleString()}</div>
              <p className="text-sm text-muted-foreground">{grossMarginPercent.toFixed(1)}% margin</p>
            </div>
            <Badge variant={grossMarginPercent >= 25 ? "default" : "destructive"}>
              {grossMarginPercent >= 25 ? "Healthy" : "Monitor"}
            </Badge>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Operational Alerts</CardTitle>
          </CardHeader>
          <CardContent className="space-y-2">
            <div className="flex items-center justify-between text-sm">
              <span>Active Projects</span>
              <span className="font-semibold">{data.activeProjects}</span>
            </div>
            <div className="flex items-center justify-between text-sm">
              <span>Overdue Invoices</span>
              <span className="font-semibold text-destructive">{data.overdueInvoices}</span>
            </div>
            <div className="flex items-center justify-between text-sm">
              <span>CR Backlog</span>
              <span className="font-semibold">${data.changeRequestBacklog.toLocaleString()}</span>
            </div>
          </CardContent>
        </Card>
      </div>

      <div className="grid gap-4 lg:grid-cols-3">
        <Card className="lg:col-span-2">
          <CardHeader>
            <CardTitle>Financial Performance</CardTitle>
          </CardHeader>
          <CardContent className="h-[360px]">
            <FinancialTrendChart data={data.monthlyFinancials} />
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Gross Margin Ratio</CardTitle>
          </CardHeader>
          <CardContent>
            <GrossMarginRadial margin={grossMarginPercent / 100} />
            <Separator className="my-4" />
            <div className="space-y-2 text-sm text-muted-foreground">
              <p>
                Target: <span className="font-semibold text-foreground">35%</span>
              </p>
              <p>
                Variance: {grossMarginPercent >= 35 ? "On target" : `${(grossMarginPercent - 35).toFixed(1)} pts`}
              </p>
            </div>
          </CardContent>
        </Card>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Project Health Overview</CardTitle>
        </CardHeader>
        <CardContent className="grid gap-4 sm:grid-cols-3">
          {data.projectHealthBreakdown.map((item) => (
            <div
              key={item.health}
              className="rounded-lg border bg-background p-4 shadow-sm"
            >
              <div className="text-sm text-muted-foreground">{item.health}</div>
              <div className="text-2xl font-semibold">{item.count}</div>
            </div>
          ))}
        </CardContent>
      </Card>
    </div>
  );
}
