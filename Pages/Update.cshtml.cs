using BugOrdersApp.Data;
using BugOrdersApp.Models;
using BugOrdersApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BugOrdersApp.Pages
{
    [Authorize(Roles = ("Admin"))]

    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _uploadService;
        private readonly IBlobService _blobService;
        public string FilePath;

        public UpdateModel(ApplicationDbContext context,
            IFileUploadService fileUploadService,
            IBlobService blobService)
        {
            _context = context;
            _uploadService = fileUploadService;
            _blobService = blobService;
        }

        [BindProperty]
        public BugOrderUpdate Update { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile imageURL)
        {
            var Updates = await _context.BugOrders.AddAsync(Update);
            Update.ImageURL = imageURL.FileName.ToString();
            Update.UserName = User.Identity.Name.ToString();
            Update.DateTime = DateTime.Now;

            var fileName = Guid.NewGuid() + Path.GetExtension(imageURL.FileName);
            var result = await _blobService.UploadBlob(fileName, imageURL, "images");


            _context.SaveChanges();

            //This still saves image locally

            //if (imageURL != null)
            //{
            //    FilePath = await _uploadService.UploadFileAsync(imageURL);
            //}


            return RedirectToAction("/Index");
        }
    }
}
