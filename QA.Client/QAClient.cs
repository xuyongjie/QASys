using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace QA.Client
{
    public class QAClient : IDisposable
    {
        private const string QuestionPrefix = "api/Questions/";
        private const string AnswerPrefix = "api/Answers/";

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
