using Microsoft.EntityFrameworkCore;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.ComplaintDTO;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Implementations.Services
{
    class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IResponseRepository _responseRepository;
        private readonly IQuestionRepository _questionRepository;

        public ComplaintService(IComplaintRepository complaintRepository, IResponseRepository responseRepository, IQuestionRepository questionRepository)
        {
            _complaintRepository = complaintRepository;
            _responseRepository = responseRepository;
            _questionRepository = questionRepository;
        }
        public async Task BanOrIgnoreComplaintAsync(BanOrIgnoreComplaintDTO banOrIgnoreDTO)
        {
            bool check = false;
            if (banOrIgnoreDTO.SpecialId.StartsWith("QUES"))
            {
                check = await _questionRepository.AnyAsync(x => x.SpecificId == banOrIgnoreDTO.SpecialId);
                if (check)
                {
                    if (banOrIgnoreDTO.Ban)
                    {
                        Question question = await _questionRepository.GetBySpecialIdAsync(banOrIgnoreDTO.SpecialId);
                        _questionRepository.Delete(question);
                        await _questionRepository.SaveChangesAsync();
                    }
                    else
                    {
                        Question question = await _questionRepository.GetBySpecialIdAsync(banOrIgnoreDTO.SpecialId);
                        question.IsDeleted = true;
                        _questionRepository.Update(question);
                        await _questionRepository.SaveChangesAsync();
                    }
                }
            }
            else if (banOrIgnoreDTO.SpecialId.StartsWith("RESP"))
            {
                check = await _responseRepository.AnyAsync(x => x.SpecificId == banOrIgnoreDTO.SpecialId);
                if (check)
                {
                    if (banOrIgnoreDTO.Ban)
                    {
                        Response response = await _responseRepository.GetBySpecialIdAsync(banOrIgnoreDTO.SpecialId);
                        _responseRepository.Delete(response);
                        await _responseRepository.SaveChangesAsync();
                    }
                    else
                    {
                        Response response = await _responseRepository.GetBySpecialIdAsync(banOrIgnoreDTO.SpecialId);
                        response.IsDeleted = true;
                        _responseRepository.Update(response);
                        await _responseRepository.SaveChangesAsync();
                    }
                }
            }
            if (!check)
                throw new Exception("Resource not found");


        }

        public async Task CreateAsync(CreateComplaintDTO complaintDTO)
        {
            bool check = false;
            if (complaintDTO.SpecialId.StartsWith("QUES"))
            {
                check = await _questionRepository.AnyAsync(x => x.SpecificId == complaintDTO.SpecialId);
            }
            else if (complaintDTO.SpecialId.StartsWith("RESP"))
            {
                check = await _responseRepository.AnyAsync(x => x.SpecificId == complaintDTO.SpecialId);
            }
            if (!check)
                throw new Exception("Resource not found");
            bool Exist = await _complaintRepository.AnyAsync(x => x.SpecificId == complaintDTO.SpecialId);
            if (!Exist)
            {
                await _complaintRepository.AddAsync(new Domain.Entitities.Complaint
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    SpecificId = complaintDTO.SpecialId,
                    Descriptions = complaintDTO.Description,
                    ComplaintsAmount = 1
                });
                await _complaintRepository.SaveChangesAsync();
            }
            else
            {
                var Complaint = await _complaintRepository.GetBySpecialIdAsync(complaintDTO.SpecialId);
                Complaint.UpdatedAt = DateTime.Now;
                Complaint.Descriptions = string.Concat(complaintDTO.Description, " ", complaintDTO.Description);
                Complaint.ComplaintsAmount += 1;
                _complaintRepository.Update(Complaint);
                await _complaintRepository.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<ComplaintDTO>> GetAllAsync(GetAllComplaintsDTO getAllComplaintsDTO)
        {
            ICollection<Complaint> complaints = await _complaintRepository.GetAll()
                .Where(c => !c.IsDeleted)

               .Skip((getAllComplaintsDTO.page - 1) * getAllComplaintsDTO.take)
               .Take(getAllComplaintsDTO.take)
               .ToListAsync();
            if (getAllComplaintsDTO.orderByAmount) complaints = complaints.OrderByDescending(x => x.ComplaintsAmount).ToList();
            return complaints.Select(x => new ComplaintDTO(x.SpecificId, x.ComplaintsAmount));
        }

        public async Task<GetComplaintDTO> GetBySpecialIdAsync(string id)
        {
            Complaint complaint = await _complaintRepository.GetBySpecialIdAsync(id);
            if (complaint == null)
                throw new Exception("Complaint not found");
            bool check = false;
            if (id.StartsWith("QUES"))
            {
                check = await _questionRepository.AnyAsync(x => x.SpecificId == id);
                if (check)
                {
                    Question question = await _questionRepository.GetBySpecialIdAsync(id);
                    GetComplaintDTO getComplaintDTO = new GetComplaintDTO(
       complaint.SpecificId,
       complaint.ComplaintsAmount,
       complaint.Descriptions,
       string.Concat(question.Title, Environment.NewLine, question.MainText)
   );
                    return getComplaintDTO;
                }
            }
            else if (id.StartsWith("RESP"))
            {
                check = await _responseRepository.AnyAsync(x => x.SpecificId == id);
                if (check)
                {
                    Response response = await _responseRepository.GetBySpecialIdAsync(id);
                    GetComplaintDTO getComplaintDTO = new GetComplaintDTO(
       complaint.SpecificId,
       complaint.ComplaintsAmount,
       complaint.Descriptions,
        response.ResponseText
   );
                    return getComplaintDTO;
                }
            }

            throw new Exception("Resource not found");


        }
    }
}
