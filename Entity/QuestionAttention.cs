using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class QuestionAttention:ICreateAndModify
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public virtual Question AttentionQuestion { get; set; }

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
