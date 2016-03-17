using Entity;
using Entity.EntityDTO;
using GalaSoft.MvvmLight.Command;
using QA.Client;
using QA.UWP.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;
using Windows.Storage.Pickers;

namespace QA.UWP.ViewModel
{
    public class QuestionDetailViewModel : MyViewModelBase, INavigatable
    {
        public QuestionDetailViewModel()
        {
            IsLoading = false;
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        private QuestionDetailDTO _currentQuestionDetail;
        public QuestionDetailDTO CurrentQuestionDetail
        {
            get { return _currentQuestionDetail; }
            set
            {
                Set(ref _currentQuestionDetail, value);
            }
        }
        private string _loadErrorMessage;
        public string LoadErrorMessage
        {
            get { return _loadErrorMessage; }
            set
            {
                Set(ref _loadErrorMessage, value);
            }
        }

        public void NavigateFrom(object parameter)
        {
            CurrentQuestionDetail = null;
        }
        private int _currentQuestionId;
        public async void NavigateTo(object parameter)
        {
            _currentQuestionId = (int)parameter;
            await GetQuestionDetailAsync(_currentQuestionId);
        }

        private async Task GetQuestionDetailAsync(int questionId)
        {
            IsLoading = true;
            HttpResult<QuestionDetailDTO> result;
            using (QAClient client = ClientFactory.CreateQAClient())
            {
                result = await client.GetQuestionDetailByQuestionIdAsync(questionId);
            }
            if (result != null && result.Succeeded)
            {
                CurrentQuestionDetail = result.Content;
                var answers = CurrentQuestionDetail.Answers;
            }
            else
            {
                StringBuilder builder = new StringBuilder("Load Error:");
                foreach (var item in result.Errors)
                {
                    builder.Append(item).Append("\n");
                }
                LoadErrorMessage = builder.ToString();
            }
            IsLoading = false;
        }

        private string _answerHolderText;
        public string AnswerHolderText
        {
            get { return _answerHolderText; }
            set { Set(ref _answerHolderText, value); }
        }

        private string _answerContent;
        public string AnswerContent
        {
            get { return _answerContent; }
            set { Set(ref _answerContent, value); }
        }

        private bool _answerPanelVisible;
        public bool AnswerPanelVisible
        {
            get { return _answerPanelVisible; }
            set { Set(ref _answerPanelVisible, value); }
        }



        private RelayCommand _attentionCommand;
        public RelayCommand AttentionCommand
        {
            get
            {
                return _attentionCommand ?? (_attentionCommand = new RelayCommand(async() =>
                {
                    await ModifyAttentionAsync(!CurrentQuestionDetail.AttentedOrNot);
                }));
            }
        }

        private RelayCommand<int> _niceCommand;
        public RelayCommand<int> NiceCommand
        {
            get
            {
                return _niceCommand ?? (_niceCommand = new RelayCommand<int>(async(id) =>
              {
                  var toAnswer = CurrentQuestionDetail.Answers.Where(a => a.Id == id).FirstOrDefault();
                  await ModifyNiceAsync(!toAnswer.NiceOrNot, id);
              }));
            }
        }

        private int _commentToAnswerId;
        private int _answerType;//0 toquestion 1 toanswer

        private RelayCommand<int> _commentToAnswerCommand;
        public RelayCommand<int> CommentToAnswerCommand
        {
            get
            {
                return _commentToAnswerCommand ?? (_commentToAnswerCommand = new RelayCommand<int>((id) =>
                {
                    _commentToAnswerId = id;
                    var toAnswer = CurrentQuestionDetail.Answers.Where(a => a.Id == id).FirstOrDefault();
                    AnswerHolderText = "回复：" + toAnswer.FromUserNickName;
                    _answerType = 1;
                }));
            }
        }

        private RelayCommand _answerToQuestionCommand;
        public RelayCommand AnswerToQuestionCommand
        {
            get
            {
                return _answerToQuestionCommand ?? (_answerToQuestionCommand = new RelayCommand(() =>
                {
                    AnswerHolderText = "回复：" + CurrentQuestionDetail.UserNickName;
                    _answerType = 0;
                }));
            }
        }

        private RelayCommand _addAnswerSubmitCommand;
        public RelayCommand AddAnswerSubmitCommand
        {
            get
            {
                return _addAnswerSubmitCommand ?? (_addAnswerSubmitCommand = new RelayCommand(async () =>
                {
                    await PostAnswerAsync();

                }));
            }
        }


        private async Task PostAnswerAsync()
        {
            if (string.IsNullOrEmpty(AnswerContent))
            {
                return;
            }
            Answer answer = new Answer();
            answer.AnswerType = _answerType;
            answer.Content = AnswerContent;
            AnswerContent = null;
            answer.QuestionId = CurrentQuestionDetail.Id;
            if (_answerType == 1)
            {
                answer.ToAnswerId = _commentToAnswerId;
            }

            HttpResult<AnswerDTO> result;
            using (var client = ClientFactory.CreateQAClient())
            {
                result = await client.PostAnswerAsync(answer);
                if (result != null && result.Succeeded)
                {
                    await GetQuestionDetailAsync(_currentQuestionId);
                }
                else
                {

                }
            }
        }

        private async Task ModifyAttentionAsync(bool attentionOrNot)
        {
            HttpResult result;
            using (var client = ClientFactory.CreateQAClient())
            {
                if(attentionOrNot)
                {
                    QuestionAttention attention = new QuestionAttention();
                    attention.QuestionId = _currentQuestionId;
                    result = await client.PostAttentionAsync(attention);
                }
                else
                {
                    result = await client.DeleteAttentionAsync(_currentQuestionId);
                }
                if(result.Succeeded)
                {
                    await GetQuestionDetailAsync(_currentQuestionId);
                }
                else
                {

                }
            }
        }

        private async Task ModifyNiceAsync(bool niceOrNot,int answerId)
        {
            HttpResult result;
            using (var client = ClientFactory.CreateQAClient())
            {
                if (niceOrNot)
                {
                    Nice nice = new Nice();
                    nice.AnswerId = answerId;
                    result = await client.PostNiceAsync(nice);
                }
                else
                {
                    result = await client.DeleteNiceAsync(answerId);
                }
                if (result.Succeeded)
                {
                    await GetQuestionDetailAsync(_currentQuestionId);
                }
                else
                {

                }
            }
        }

    }
}
