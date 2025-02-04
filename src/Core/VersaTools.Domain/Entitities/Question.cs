using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Domain.Entitities.Base;

namespace VersaTools.Domain.Entitities
{
    public class Question : BaseEntity
    {
        public string SpecificId {  get; set; } 
        public string Title { get; set; }
        public string MainText { get; set; }
        //Relation
        public ICollection<Response> Responses { get; set; }

    }
}
