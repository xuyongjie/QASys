using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.EntityDTO
{
    public class QuestionDetailDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserNickName { get; set; }
        public string UserHeadImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AnswerCount { get; set; }
        public bool AttentedOrNot { get; set; }
        public DateTime CreateTime { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
