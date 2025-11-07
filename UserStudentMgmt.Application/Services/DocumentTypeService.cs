using AutoMapper;
using UserStudentMgmt.Application.DTOs.DocumentTypes;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Interfaces;

namespace UserStudentMgmt.Application.Services;

public class DocumentTypeService : IDocumentTypeService
{
    private readonly IDocumentTypeRepository _docTypeRepo;
    private readonly IMapper _mapper;

    public DocumentTypeService(IDocumentTypeRepository docTypeRepo, IMapper mapper)
    {
        _docTypeRepo = docTypeRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DocumentTypeDto>> GetAllAsync()
    {
        var docTypes = await _docTypeRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<DocumentTypeDto>>(docTypes);
    }
}