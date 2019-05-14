using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpliChat.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("conStr")
        {

        }
    }
}
