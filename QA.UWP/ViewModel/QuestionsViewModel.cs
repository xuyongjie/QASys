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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace QA.UWP.ViewModel
{
    public class QuestionsViewModel : MyViewModelBase, INavigatable
    {
        public QuestionsViewModel()
        {
            _allQuestions = new ObservableCollection<QuestionDTO>();
            _myAttentionQuestions = new ObservableCollection<QuestionDTO>();
            GetInfoAsync();
        }

        public async void GetInfoAsync()
        {
            await GetMyInfo();
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

        private ObservableCollection<QuestionDTO> _myAttentionQuestions;
        public ObservableCollection<QuestionDTO> MyAttentionQuestions
        {
            get { return _myAttentionQuestions; }
            set { Set(ref _myAttentionQuestions, value); }
        }

        private int _pivotSelectedIndex;
        public int PivotSelectedIndex
        {
            get { return _pivotSelectedIndex; }
            set { Set(ref _pivotSelectedIndex, value); }
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
            if (result != null && result.Succeeded)
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
            using (QAClient client = ClientFactory.CreateQAClient())
            {
                result = null;
                result = await client.GetAllQuestionsAsync();
            }
            if (result != null && result.Succeeded)
            {
                AllQuestions = new ObservableCollection<QuestionDTO>(result.Content);
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
        }

        public async Task GetMyAttentionQuestions()
        {
            HttpResult<List<QuestionDTO>> result;
            using (QAClient client = ClientFactory.CreateQAClient())
            {
                result = null;
                result = await client.GetAttentionQuestionsAsync();
            }
            if (result != null && result.Succeeded)
            {
                MyAttentionQuestions = new ObservableCollection<QuestionDTO>(result.Content);
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
        }

        public void NavigateTo(object parameter)
        {
            (Window.Current.Content
                 as Frame).BackStack.Clear();
            LoadQuestions();
        }

        public void NavigateFrom(object parameter)
        {
        }

        private async void LoadQuestions()
        {
            IsLoading = true;
            await GetAllQuestions();
            await GetMyAttentionQuestions();
            IsLoading = false;
        }

        private RelayCommand _refreshAllCommand;
        public RelayCommand RefreshAllCommand
        {
            get
            {
                return _refreshAllCommand ?? (_refreshAllCommand = new RelayCommand(async () =>
              {
                  if (PivotSelectedIndex == 0)
                  {

                      await GetAllQuestions();
                  }
                  else if (PivotSelectedIndex == 1)
                  {
                      await GetMyAttentionQuestions();
                  }
              }));
            }
        }

        private RelayCommand _createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ?? (_createCommand = new RelayCommand(() =>
              {
                  NavigationService.NavigateTo(typeof(CreateQuestionViewModel).FullName);
              }));
            }
        }

        private RelayCommand<object> _itemClickCommand;
        public RelayCommand<object> ItemClickCommand
        {
            get
            {
                return _itemClickCommand ?? (_itemClickCommand = new RelayCommand<object>((parameter) =>
              {
                  ItemClickEventArgs arg = parameter as ItemClickEventArgs;
                  QuestionDTO question = arg.ClickedItem as QuestionDTO;
                  NavigationService.NavigateTo(typeof(QuestionDetailViewModel).FullName, question.Id);
              }));
            }
        }

        private RelayCommand _userInfoCommand;
        public RelayCommand UserInfoCommand
        {
            get
            {
                return _userInfoCommand ?? (_userInfoCommand = new RelayCommand(() =>
                {
                    NavigationService.NavigateTo(typeof(UserInfoViewModel).FullName, MyInfo);
                }));
            }
        }
    }
}
