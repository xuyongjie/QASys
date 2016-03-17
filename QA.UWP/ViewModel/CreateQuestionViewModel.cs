using Entity;
using Entity.EntityDTO;
using GalaSoft.MvvmLight.Command;
using QA.UWP.Core;
using QA.UWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WebApi.Client;

namespace QA.UWP.ViewModel
{
    public class CreateQuestionViewModel : MyViewModelBase
    {
        public CreateQuestionViewModel()
        {
        }


        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { Set(ref _errorMessage, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { Set(ref _content, value); }
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ?? (_createCommand = new RelayCommand(async () =>
              {
                  await PostQuestionAsync();
              }));
            }
        }

        private async Task PostQuestionAsync()
        {
            HttpResult<QuestionDTO> result;
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Content))
            {
                ErrorMessage = "主题或内容不能为空";
                return;
            }
            using (var client = ClientFactory.CreateQAClient())
            {
                Question question = new Question();
                question.Content = Content;
                question.Title = Title;
                result = await client.PostQuestionAsync(question);
                if (result != null && result.Succeeded)
                {
                    NavigationService.GoBack();
                    //NavigationService.NavigateTo(typeof(QuestionDetailViewModel).FullName, result);
                }
                else
                {

                }
            }
        }
    }
}
