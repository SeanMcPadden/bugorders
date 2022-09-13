using Azure.Storage.Blobs;
using BugOrdersApp.Data;
using BugOrdersApp.Models;
using BugOrdersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BugOrders.Pages
{
    public class NewtownabbeyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _blobService;
        private readonly BlobServiceClient _blobServiceClient;
        public string LatestUpdate;
        public string LatestImage;
        public string LatestImageFromAzure;

        public List<string> listOfFiles = new List<string>();
        public string File;

        public NewtownabbeyModel(ApplicationDbContext context, 
                                 IBlobService blobService,
                                 BlobServiceClient bobServiceClient)
        {
            _context = context;
            _blobService = blobService;
            _blobServiceClient = bobServiceClient;
        }

        public IList<BugOrderUpdate> Updates { get; set; }
        public BugOrderUpdate LastUpdate { get; set; }

        public async Task OnGetAsync()
        {
            Updates = await _context.BugOrders.ToListAsync();

            var orderUpdateBy = _context.BugOrders.OrderByDescending(a => a.Id);
            

            var highestId = orderUpdateBy.FirstOrDefault();
            var latestImage = orderUpdateBy.FirstOrDefault();

            LatestUpdate = highestId.DateTime.ToString();

            //This would show the image from local storage 
            //If using this method, don't forget to put ~/images/ infront of the html image path.
            //LatestImage = highestId.ImageURL.ToString();

            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("images");

            var blobs = blobContainerClient.GetBlobs();

            var gettingLastModified = blobs.OrderByDescending(m => m.Properties.LastModified)
                                    .ToList()
                                    .First();
            LatestImageFromAzure = gettingLastModified.Name.ToString();

            File = await _blobService.GetBlob(LatestImageFromAzure, "images");
        }
    }
}
