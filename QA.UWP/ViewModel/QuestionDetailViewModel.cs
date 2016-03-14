using Entity.EntityDTO;
using GalaSoft.MvvmLight.Command;
using QA.UWP.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



        public void NavigateFrom(object parameter)
        {
        }

        public async void NavigateTo(object parameter)
        {
            //CurrentEvent = parameter as Event;
            //await GetEventNodes(CurrentEvent.Id);
        }


        private async Task PostNodeAsync()
        {
            //NewNode.CreatorId = "xuyongjie1128@hotmail.com";
            //NewNode.EventId = CurrentEvent.Id;
            //NewNode.NodeTypeId = SelectedNodeType.Id;

            //HttpResult<Node> result;
            //using (var client = ClientFactory.CreateTravelClient())
            //{
            //    result = await client.PostNodeAsync(NewNode);
            //    if (result != null && result.Succeeded)
            //    {
                
            //        Nodes.Add(result.Content);
            //    }
            //    else
            //    {

            //    }
            //}
        }




        private bool _isCost;
        public bool IsCost
        {
            get { return _isCost; }
            set { Set(ref _isCost, value); }
        }

        private RelayCommand _addNodeSubmitCommand;
        public RelayCommand AddNodeSubmitCommand
        {
            get
            {
                return _addNodeSubmitCommand ?? (_addNodeSubmitCommand = new RelayCommand(async () =>
                {
                    await PostNodeAsync();

                }));
            }
        }

        private RelayCommand _chooseImageCommand;
        public RelayCommand ChooseImageCommand
        {
            get {
                return _chooseImageCommand ?? (_chooseImageCommand = new RelayCommand(() =>
              {
                  ChooseImage();
              }));
            }
        }


        private ObservableCollection<SelectedImage> _selectedImages;
        public ObservableCollection<SelectedImage> SelectedImages
        {
            get { return _selectedImages; }
            set { Set(ref _selectedImages, value); }
        }
        public async void ChooseImage()
        {
            if (SelectedImages == null)
            {
                SelectedImages = new ObservableCollection<SelectedImage>();
            }
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".png");
            var results = await fileOpenPicker.PickMultipleFilesAsync();
            foreach(var item in results)
            {
                SelectedImages.Add(new SelectedImage(item.Path));
            }
        }


        public class SelectedImage
        {
            public SelectedImage(string path)
            {
                this.ImagePath = path;
            }
            public string ImagePath { get; set; }
        }
    }
}
