using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Nice : ICreateAndModify
    {
        public DateTime CreateTime
        {
            get; set;
        }

        public DateTime ModifyTime
        {
            get; set;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer NiceAnswer { get; set; }
    }
}
