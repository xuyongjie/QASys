using Entity;
using Entity.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;

namespace QA.Client
{
    public class QAClient : IDisposable
    {
        private const string QuestionPrefix = "api/Questions/";
        private const string AnswerPrefix = "api/Answers/";
		
		private const string GetAllQuestions="all";
		private const string GetAllTimeLineQuestions="timeline";
		private const string CreateQuestion="";
		
		private const string CreateAnswer="";

        public HttpClient HttpClient { get; private set; }
        private bool disposed;
        public QAClient(HttpClient client)
        {
            if(client==null)
            {
                throw new ArgumentNullException();
            }
            this.HttpClient = client;
        }
		
		
		
		public async Task<HttpResult<List<QuestionDTO>>> GetAllQuestionsAsync()
        {
            ThrowIfDisposed();
            HttpResult<List<QuestionDTO>> result = await HttpClient.GetAsync<List<QuestionDTO>>(GetAllQuestions);
            return result;
        }
		
		public async Task<HttpResult<List<QuestionDTO>>> GetTimeLineAllQuestionsAsync()
        {
            ThrowIfDisposed();
            HttpResult<List<QuestionDTO>> result = await HttpClient.GetAsync<List<QuestionDTO>>(GetAllTimeLineQuestions);
            return result;
        }

        public async Task<HttpResult<QuestionDetailDTO>> GetQuestionDetailByQuestionIdAsync(int questionId)
        {
            ThrowIfDisposed();
            HttpResult<QuestionDetailDTO> result = await HttpClient.GetAsync<QuestionDetailDTO>(QuestionPrefix+questionId.ToString());
            return result;
        }

        public async Task<HttpResult<QuestionDTO>> PostQuestionAsync(Question question)
        {
            ThrowIfDisposed();
            HttpResult<QuestionDTO> result = await HttpClient.PostAsJsonAsync<Question, QuestionDTO>(CreateQuestion, question);
            return result;
        }

		
		
		
        public async Task<HttpResult<AnswerDTO>> PostAnswerAsync(Answer answer)
        {
            ThrowIfDisposed();
            HttpResult<AnswerDTO> result = await HttpClient.PostAsJsonAsync<Answer, AnswerDTO>(CreateAnswer, answer);
            return result;
        }
		
		
        public void Dispose()
        {
            if(!disposed)
            {
                HttpClient.Dispose();
                disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if(disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
