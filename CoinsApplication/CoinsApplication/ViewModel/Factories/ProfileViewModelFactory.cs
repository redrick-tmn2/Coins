using CoinsApplication.Models;
using CoinsApplication.Services;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.ViewModel.Factories
{
    public class ProfileViewModelFactory
    {
        private readonly IDialogService _dialogService;
        private readonly IImageReaderService _imageReaderService;

        public ProfileViewModelFactory(IDialogService dialogService, IImageReaderService imageReaderService)
        {
            _dialogService = dialogService;
            _imageReaderService = imageReaderService;
        }

        public ProfileViewModel Create(ProfileModel model)
        {
            return new ProfileViewModel(model, _dialogService, _imageReaderService);
        }
    }
}