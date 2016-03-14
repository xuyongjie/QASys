using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Answer:ICreateAndModify
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question LinkQuestion { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Content { get; set; }
        public virtual List<Nice> Nices { get; set; }

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
