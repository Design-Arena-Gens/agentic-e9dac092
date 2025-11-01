import {
  Line,
  LineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
  CartesianGrid,
  Legend,
} from "recharts";
import type { MonthlyFinancialTrendDto } from "../../types/api";

interface FinancialTrendChartProps {
  data: MonthlyFinancialTrendDto[];
}

export function FinancialTrendChart({ data }: FinancialTrendChartProps) {
  const enrichedData = data.map((entry) => ({
    ...entry,
    label: `${entry.month}/${entry.year}`,
  }));

  return (
    <ResponsiveContainer width="100%" height={320}>
      <LineChart data={enrichedData} margin={{ top: 16, right: 24, left: 0, bottom: 8 }}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="label" />
        <YAxis tickFormatter={(value) => `$${value / 1000}k`} />
        <Tooltip formatter={(value: number) => `$${value.toLocaleString()}`} />
        <Legend />
        <Line
          type="monotone"
          dataKey="recognizedRevenue"
          stroke="#6366f1"
          name="Recognized Revenue"
          strokeWidth={2}
          dot={false}
        />
        <Line
          type="monotone"
          dataKey="invoicedAmount"
          stroke="#22d3ee"
          name="Invoiced"
          strokeWidth={2}
          dot={false}
        />
        <Line
          type="monotone"
          dataKey="costIncurred"
          stroke="#f97316"
          name="Cost"
          strokeWidth={2}
          dot={false}
        />
      </LineChart>
    </ResponsiveContainer>
  );
}
