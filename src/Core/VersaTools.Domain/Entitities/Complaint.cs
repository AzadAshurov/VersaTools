using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Domain.Entitities.Base;

namespace VersaTools.Domain.Entitities
{
    public class Complaint : BaseEntity
    {
        public string SpecificId { get; set; }
        public string Descriptions { get; set; }
        public int ComplaintsAmount { get; set; }
        //public User
    }
}
