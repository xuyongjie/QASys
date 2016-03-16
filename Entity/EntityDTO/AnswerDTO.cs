using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.EntityDTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string FromUserId { get; set; }
        public string FromUserNickName { get; set; }
        public int ToAnswerId { get; set; }
        /// <summary>
        /// 0 ToQuestion
        /// 1 ToAnswer
        /// </summary>
        public int AnswerType { get; set; }
        public string ToUserId { get; set; }
        public string ToUserNickName { get; set; }
        public string Content { get; set; }
        public int NiceCount { get; set; }

        public DateTime CreateTime
        {
            get; set;
        }
    }
}
