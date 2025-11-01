import {
  RadialBarChart,
  RadialBar,
  PolarAngleAxis,
  ResponsiveContainer,
} from "recharts";

interface GrossMarginRadialProps {
  margin: number;
}

export function GrossMarginRadial({ margin }: GrossMarginRadialProps) {
  const chartValue = Math.max(Math.min(margin * 100, 100), 0);
  const data = [
    {
      name: "Gross Margin",
      value: chartValue,
      fill: chartValue >= 0 ? "#22c55e" : "#ef4444",
    },
  ];

  return (
    <ResponsiveContainer width="100%" height={240}>
      <RadialBarChart
        data={data}
        startAngle={90}
        endAngle={-270}
        innerRadius="60%"
        outerRadius="100%"
      >
        <PolarAngleAxis type="number" domain={[0, 100]} tick={false} />
        <RadialBar dataKey="value" />
      </RadialBarChart>
    </ResponsiveContainer>
  );
}
