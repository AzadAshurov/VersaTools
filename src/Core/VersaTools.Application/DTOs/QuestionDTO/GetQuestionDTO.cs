﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.DTOs.ResponseDTO;

namespace VersaTools.Application.DTOs.QuestionDTO
{
    public record GetQuestionDTO(string Title, string SpecificId, string MainText, ICollection<GetResponseDTO>? Responses);

}
