using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.EntityDTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserNickName { get; set; }
        public string UserHeadImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AttentionCount { get; set; }
        public int AnswerCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
