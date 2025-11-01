import { Route, Routes } from "react-router-dom";
import { AppLayout } from "./components/layout/app-layout";
import { DashboardPage } from "./features/dashboard/dashboard-page";
import { ProjectsPage } from "./features/projects/projects-page";
import { FinancialsPage } from "./features/financials/financials-page";
import { InvoicingPage } from "./features/invoicing/invoicing-page";
import { ChangeRequestsPage } from "./features/projects/change-requests-page";
import { RemindersPage } from "./features/reminders/reminders-page";
import { TeamPage } from "./features/team/team-page";

function App() {
  return (
    <Routes>
      <Route path="/" element={<AppLayout />}>
        <Route index element={<DashboardPage />} />
        <Route path="projects" element={<ProjectsPage />} />
        <Route path="financials" element={<FinancialsPage />} />
        <Route path="invoicing" element={<InvoicingPage />} />
        <Route path="change-requests" element={<ChangeRequestsPage />} />
        <Route path="reminders" element={<RemindersPage />} />
        <Route path="team" element={<TeamPage />} />
      </Route>
    </Routes>
  );
}

export default App;
