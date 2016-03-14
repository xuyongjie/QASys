using Entity;
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
            _participants = new ObservableCollection<User>();
            _participants.Add(new User { Avatar = "/Assets/StoreLogo.png", UserName = "Add" });
        }


        private ObservableCollection<User> _participants;
        public ObservableCollection<User> Participants
        {
            get { return _participants; }
            set { Set(ref _participants, value); }
        }



        private RelayCommand _loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new RelayCommand(() =>
                {
                }));
            }
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

        private Question _newQuestion;
        public Question NewQuestion
        {
            get { return _newQuestion; }
            set
            {
                Set(ref _newQuestion, value);
            }
        }

        private async Task PostQuestionAsync()
        {
            HttpResult<Question> result;
            using (var client = ClientFactory.CreateTravelClient())
            {
                //result = await client.PostEventAsync(NewQuestion);
                //if (result != null && result.Succeeded)
                //{
                //    //MessengerInstance.Send(result.Content, "SelectedActivity");
                //    NavigationService.NavigateTo(typeof(QuestionDetailViewModel).FullName, NewQuestion);
                //}
                //else
                //{

                //}
            }
        }
    }
}
