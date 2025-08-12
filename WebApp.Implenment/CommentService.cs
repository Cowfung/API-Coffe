using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Infrastructure;
using WebApp.Interface;
using WebApp.Model;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Implenment
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _uploadRootPath;
       
        public CommentService(ICommentRepository commentRepository, IMapper mapper, IOptions<AppSettings> options, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _uploadRootPath = options.Value.UploadRootPath;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommentViewModel> AddComment(CommentCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                if (string.IsNullOrWhiteSpace(request.GuestName) ||
                    string.IsNullOrWhiteSpace(request.GuestEmail) ||
                    string.IsNullOrWhiteSpace(request.GuestPhone))
                    throw new Exception("input your name , email , your phone number");
            }

            var comment = _mapper.Map<Comment>(request);
            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            comment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            if (request.Files != null)
            {
                foreach(var file in request.Files)
                {
                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                    var allowed = new[] { ".jpg", ".png", ".mp4", ".webm", ".jpeg" };
                    if (!allowed.Contains(ext)) continue;
                    var filename = Guid.NewGuid() + ext;
                    var savePath = Path.Combine(_uploadRootPath, "uploads/comments", filename);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                    using(var stream = System.IO.File.Create(savePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    comment.Media.Add(new CommentMedia
                    {
                        FilePath = $"/uploads/comments/{filename}",
                        FileType = file.ContentType
                    });
                }
            }
           
            await _commentRepository.AddAsync(comment);

            await _unitOfWork.CommitAsync();
            return _mapper.Map<CommentViewModel>(comment);

        }

        public async Task<List<CommentViewModel>> GetCommentbyProductId(int productId)
        {
            var comments = await _commentRepository.GetByProductId(productId);
            return _mapper.Map<List<CommentViewModel>>(comments);
        }
    }
}
