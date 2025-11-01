using AutoMapper;
using FluentValidation;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateInvoiceRequest> _validator;

    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateInvoiceRequest> validator)
    {
        _invoiceRepository = invoiceRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException($"Project {request.ProjectId} not found.");
        }

        var invoice = new Invoice
        {
            ProjectId = request.ProjectId,
            InvoiceNumber = request.InvoiceNumber,
            InvoiceDate = request.InvoiceDate,
            DueDate = request.DueDate,
            TaxAmount = request.TaxAmount,
            Subtotal = request.Lines.Sum(l => l.Quantity * l.UnitPrice)
        };

        foreach (var line in request.Lines)
        {
            invoice.Lines.Add(new InvoiceLine
            {
                Description = line.Description,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice
            });
        }

        await _invoiceRepository.AddAsync(invoice, cancellationToken);

        project.RevenueRecognized += invoice.Subtotal;
        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }

    public async Task<IReadOnlyList<InvoiceDto>> GetProjectInvoicesAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var invoices = await _invoiceRepository.GetByProjectAsync(projectId, cancellationToken);
        return invoices.Select(i => _mapper.Map<InvoiceDto>(i)).ToList();
    }

    public async Task<IReadOnlyList<InvoiceDto>> GetOverdueInvoicesAsync(DateTime asOf, CancellationToken cancellationToken = default)
    {
        var invoices = await _invoiceRepository.GetOverdueAsync(asOf, cancellationToken);
        return invoices.Select(i => _mapper.Map<InvoiceDto>(i)).ToList();
    }
}
