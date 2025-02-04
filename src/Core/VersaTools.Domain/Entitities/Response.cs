using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Domain.Entitities.Base;

namespace VersaTools.Domain.Entitities
{
    public class Response : BaseEntity
    {
        public string SpecificId { get; set; }
        public string ResponseText { get; set; }
        //Relation
       public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
