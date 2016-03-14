using Account.Client;
using Entity.EntityDTO;
using GalaSoft.MvvmLight.Command;
using QA.Client;
using QA.UWP.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;
using Windows.UI.Xaml.Controls;

namespace QA.UWP.ViewModel
{
    public class QuestionsViewModel:MyViewModelBase,INavigatable
    {
        public QuestionsViewModel()
        {
            _allQuestions = new ObservableCollection<QuestionDTO>();
            FirstLoad();
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        private ObservableCollection<QuestionDTO> _allQuestions;
        public ObservableCollection<QuestionDTO> AllQuestions
        {
            get { return _allQuestions; }
            set { Set(ref _allQuestions, value); }
        }

        private QuestionDTO _selectedQuestion;
        public QuestionDTO SelectedQuestion
        {
            get { return _selectedQuestion; }
            set { Set(ref _selectedQuestion, value); }
        }

        private string _loadErrorMessage;
        public string LoadErrorMessage
        {
            get { return _loadErrorMessage; }
            set { Set(ref _loadErrorMessage, value); }
        }

        private UserInfo _myInfo;
        public UserInfo MyInfo
        {
            get { return _myInfo; }
            set { Set(ref _myInfo, value); }
        }

        public async Task GetMyInfo()
        {
            HttpResult<UserInfo> result;
            using (AccountClient client = ClientFactory.CreateAccountClient())
            {
                result = await client.GetUserInfoAsync();
            }
            if(result!=null&&result.Succeeded)
            {
                MyInfo = result.Content;
            }
            else
            {

            }
        }

        public async Task GetAllQuestions()
        {
            HttpResult<List<QuestionDTO>> result;
            using (QAClient client = ClientFactory.CreateTravelClient())
            {
                result = null;
                //result=await client.GetMyEventListAsync();
            }
            if(result!=null&&result.Succeeded)
            {
                AllQuestions = new ObservableCollection<QuestionDTO>(result.Content);
            }
            else
            {
                StringBuilder builder = new StringBuilder("Load Error:");
                foreach(var item in result.Errors)
                {
                    builder.Append(item).Append("\n");
                }
                LoadErrorMessage = builder.ToString();
            }
        }

        public void NavigateTo(object parameter)
        {
        }

        public void NavigateFrom(object parameter)
        {
        }

        private async void FirstLoad()
        {
            IsLoading = true;
            await GetMyInfo();
            await GetAllQuestions();
            IsLoading = false;
        }

        private RelayCommand _loadCommand;
        public RelayCommand LoadCommand
        {
            get {
                return _loadCommand ?? (_loadCommand = new RelayCommand(() =>
              {
                 
              }));
            }
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get {
                return _createCommand ?? (_createCommand = new RelayCommand(() =>
              {
                  NavigationService.NavigateTo(typeof(CreateQuestionViewModel).FullName);
              }));
            }
        }

        private RelayCommand<object> _itemClickCommand;
        public RelayCommand<object> ItemClickCommand
        {
            get {
                return _itemClickCommand ?? (_itemClickCommand = new RelayCommand<object>((parameter) =>
              {
                  ItemClickEventArgs arg = parameter as ItemClickEventArgs;
                  NavigationService.NavigateTo(typeof(QuestionDetailViewModel).FullName, arg.ClickedItem);
              }));
            }
        }
    }
}
