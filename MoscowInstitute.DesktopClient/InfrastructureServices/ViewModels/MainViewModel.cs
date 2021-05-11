using RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase;
using RussiaPublicHealthProtection.ApplicationServices.Ports;
using RussiaPublicHealthProtection.DomainObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.DesktopClient.InfrastructureServices.ViewModels 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGetPublicHealthProtectionListUseCase _getPublicHealthProtectionListUseCase;

        public MainViewModel(IGetPublicHealthProtectionListUseCase getPublicHealthProtectionListUseCase)
            => _getPublicHealthProtectionListUseCase = getPublicHealthProtectionListUseCase;

        private Task<bool> _loadingTask;
        private PublicHealthProtection _currentPublicHealthProtection;
        private ObservableCollection<PublicHealthProtection> _PublicHealthProtectiont;

        public event PropertyChangedEventHandler PropertyChanged;

        public PublicHealthProtection CurrentPublicHealthProtection
        {
            get => _currentPublicHealthProtection;
            set
            {
                if (_currentPublicHealthProtection != value)
                {
                    _currentPublicHealthProtection = value;
                    OnPropertyChanged(nameof(CurrentPublicHealthProtection));
                }
            }
        }

        private async Task<bool> LoadPublicHealthProtections()
        {
            var outputPort = new OutputPort();
            bool result = await _getPublicHealthProtectionListUseCase.Handle(GetPublicHealthProtectionListUseCaseRequest.CreateAllPublicHealthProtectionRequest(), outputPort);
            if (result)
            {
                PublicHealthProtections = new ObservableCollection<PublicHealthProtection>(outputPort.PublicHealthProtections);
            }
            return result;
        }

        public ObservableCollection<PublicHealthProtection> PublicHealthProtections
        {
            get
            {
                if (_loadingTask == null)
                {
                    _loadingTask = LoadPublicHealthProtections();
                }

                return _PublicHealthProtectiont;
            }
            set
            {
                if (_PublicHealthProtectiont != value)
                {
                    _PublicHealthProtectiont = value;
                    OnPropertyChanged(nameof(PublicHealthProtections));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private class OutputPort : IOutputPort<GetPublicHealthProtectionListUseCaseResponse>
        {
            public IEnumerable<PublicHealthProtection> PublicHealthProtections { get; private set; }

            public void Handle(GetPublicHealthProtectionListUseCaseResponse response)
            {
                if (response.Success)
                {
                    PublicHealthProtections = new ObservableCollection<PublicHealthProtection>(response.PublicHealthProtection);
                }
            }
        }
    }
}
