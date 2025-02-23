using VersaTools.Application.DTOs.ComplaintDTO;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IComplaintService
    {
        Task<IEnumerable<ComplaintDTO>> GetAllAsync(GetAllComplaintsDTO getAllComplaintsDTO);
        Task<GetComplaintDTO> GetBySpecialIdAsync(string specialId);
        Task CreateAsync(CreateComplaintDTO complaintDTO);
        Task BanOrIgnoreComplaintAsync(BanOrIgnoreComplaintDTO banOrIgnoreDTO);
    }
}
