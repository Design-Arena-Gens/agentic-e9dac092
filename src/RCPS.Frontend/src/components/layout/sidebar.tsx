import { NavLink } from "react-router-dom";
import { cn } from "../../lib/utils";
import {
  FileText,
  PieChart,
  Briefcase,
  DollarSign,
  Clock,
  Bell,
  Users,
} from "lucide-react";

const navItems = [
  { to: "/", label: "Dashboard", icon: PieChart },
  { to: "/projects", label: "Projects", icon: Briefcase },
  { to: "/financials", label: "Financials", icon: DollarSign },
  { to: "/invoicing", label: "Invoicing", icon: FileText },
  { to: "/change-requests", label: "Change Requests", icon: Clock },
  { to: "/reminders", label: "Reminders", icon: Bell },
  { to: "/team", label: "Team", icon: Users },
];

export function Sidebar() {
  return (
    <aside className="hidden w-64 flex-col border-r bg-muted/40 p-4 md:flex">
      <div className="flex items-center gap-2 px-2 text-lg font-semibold">
        <span className="rounded-full bg-primary/10 p-2 text-primary">RCPS</span>
        <span>Implementation Suite</span>
      </div>
      <nav className="mt-8 space-y-1">
        {navItems.map((item) => {
          const Icon = item.icon;
          return (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) =>
                cn(
                  "flex items-center gap-2 rounded-md px-3 py-2 text-sm font-medium transition",
                  isActive
                    ? "bg-primary text-primary-foreground"
                    : "text-muted-foreground hover:bg-muted hover:text-foreground"
                )
              }
            >
              <Icon className="h-4 w-4" />
              {item.label}
            </NavLink>
          );
        })}
      </nav>
    </aside>
  );
}
