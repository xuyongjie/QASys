using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Question:ICreateAndModify
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }

        public DateTime CreateTime
        {
            get; set;
        }

        public DateTime ModifyTime
        {
            get; set;
        }
    }
}
