using VersaTools.Application.DTOs.ResponseDTO;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IResponseService
    {
        Task<IEnumerable<ResponsesDTO>> GetAllAsync(int page, int take);
        Task<GetResponseDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateResponseDTO responseDTO);
        Task UpdateResponseAsync(int id, UpdateResponseDTO responseDTO);
        Task DeleteResponseAsync(int id);
    }

}
