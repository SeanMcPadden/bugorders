namespace BugOrdersApp.Services
{
    public class BlobFiles
    {
        private readonly IBlobService _blobService;

        public BlobFiles(IBlobService blobService)
        {
            _blobService = blobService;
        }
    }
}
