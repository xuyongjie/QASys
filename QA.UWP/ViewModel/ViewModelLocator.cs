using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using QA.UWP.View;

namespace QA.UWP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = InitNavigationService();
            SimpleIoc.Default.Register<INavigationService>(()=>navigationService);
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<QuestionsViewModel>();
            SimpleIoc.Default.Register<CreateQuestionViewModel>();
            SimpleIoc.Default.Register<QuestionDetailViewModel>();
            SimpleIoc.Default.Register<UserInfoViewModel>();
        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public RegisterViewModel RegisterViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegisterViewModel>();
            }
        }

        public QuestionsViewModel MyActivitiesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionsViewModel>();
            }
        }

        public CreateQuestionViewModel CreateActivityViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CreateQuestionViewModel>(); }
        }

        public QuestionDetailViewModel ActivityDetailViewModel
        {
            get { return ServiceLocator.Current.GetInstance<QuestionDetailViewModel>(); }
        }

        public UserInfoViewModel UserInfoViewModel
        {
            get { return ServiceLocator.Current.GetInstance<UserInfoViewModel>(); }
        }

        protected INavigationService InitNavigationService()
        {
            var service = new NavigationService();
            service.Configure(typeof(LoginViewModel).FullName, typeof(LoginPage));
            service.Configure(typeof(RegisterViewModel).FullName, typeof(RegisterPage));
            service.Configure(typeof(QuestionsViewModel).FullName, typeof(QuestionsPage));
            service.Configure(typeof(CreateQuestionViewModel).FullName, typeof(CreateQuestionPage));
            service.Configure(typeof(QuestionDetailViewModel).FullName, typeof(QuestionDetailPage));
            service.Configure(typeof(UserInfoViewModel).FullName, typeof(UserInfoPage));
            return service;
        }
    }
}
