export type ProjectHealth = "OnTrack" | "AtRisk" | "OffTrack";
export type ProjectStatus = "Draft" | "Active" | "OnHold" | "Completed" | "Cancelled";

export interface ProjectDto {
  id: string;
  name: string;
  clientId: string;
  clientName: string;
  description?: string;
  status: ProjectStatus;
  health: ProjectHealth;
  startDate: string;
  endDate: string;
  budgetHours: number;
  budgetAmount: number;
  actualHours: number;
  actualAmount: number;
  revenueRecognized: number;
  costIncurred: number;
  grossMargin: number;
  milestones: ProjectMilestoneDto[];
  statementsOfWork: StatementOfWorkDto[];
  changeRequests: ChangeRequestSummaryDto[];
  invoices: InvoiceSummaryDto[];
}

export interface ProjectMilestoneDto {
  id: string;
  name: string;
  targetDate: string;
  completionDate?: string;
  completionPercentage: number;
}

export interface StatementOfWorkDto {
  id: string;
  sowNumber?: string;
  SOWNumber?: string;
  effectiveDate: string;
  expiryDate: string;
  totalValue: number;
  totalHours: number;
  scopeSummary?: string;
}

export interface ChangeRequestSummaryDto {
  id: string;
  title: string;
  status: string;
  estimatedHours: number;
  estimatedAmount: number;
  createdAtUtc: string;
}

export interface InvoiceSummaryDto {
  id: string;
  invoiceNumber: string;
  status: string;
  invoiceDate: string;
  dueDate: string;
  totalAmount: number;
  amountPaid: number;
}

export interface DashboardSummaryDto {
  totalRevenue: number;
  totalCost: number;
  grossMargin: number;
  changeRequestBacklog: number;
  activeProjects: number;
  overdueInvoices: number;
  projectHealthBreakdown: ProjectHealthSummaryDto[];
  monthlyFinancials: MonthlyFinancialTrendDto[];
}

export interface ProjectHealthSummaryDto {
  health: ProjectHealth;
  count: number;
}

export interface MonthlyFinancialTrendDto {
  year: number;
  month: number;
  recognizedRevenue: number;
  invoicedAmount: number;
  costIncurred: number;
}

export interface InvoiceDto {
  id: string;
  projectId: string;
  invoiceNumber: string;
  status: string;
  invoiceDate: string;
  dueDate: string;
  subtotal: number;
  taxAmount: number;
  amountPaid: number;
  lines: InvoiceLineDto[];
}

export interface InvoiceLineDto {
  id: string;
  description?: string;
  quantity: number;
  unitPrice: number;
  lineTotal: number;
}

export interface ReminderDto {
  id: string;
  type: string;
  projectId?: string;
  invoiceId?: string;
  scheduledFor: string;
  isSent: boolean;
  message?: string;
}

export interface ChangeRequestDto extends ChangeRequestSummaryDto {
  projectId: string;
  description?: string;
  decisionDate?: string;
  decisionNotes?: string;
}

export interface EffortEntryDto {
  id: string;
  projectId: string;
  teamMemberId: string;
  teamMemberName: string;
  effortType: string;
  workDate: string;
  hours: number;
  billRate: number;
  costRate: number;
  notes?: string;
}

export interface TeamMemberDto {
  id: string;
  firstName: string;
  lastName: string;
  email?: string;
  role?: string;
  defaultBillRate: number;
  defaultCostRate: number;
}

export interface ClientDto {
  id: string;
  name: string;
  industry?: string;
  primaryContactName?: string;
  primaryContactEmail?: string;
  currency: string;
  annualContractValue: number;
}
