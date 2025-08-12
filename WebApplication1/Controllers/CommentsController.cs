using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Model;
using WebApp.Service.Implenment;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApplication1.Extenxions;

namespace WebApplication1.Controllers 
{

    [AllowAnonymous]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IHubContext<CommentHub> _hubContext;
        public CommentsController(ICommentService commentService,IHubContext<CommentHub> hubContext)
        {
            _commentService = commentService;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> PostComment([FromForm] CommentCreateRequest request)
        {
            var comment = await _commentService.AddComment(request);
            await _hubContext.Clients.All.SendAsync("NewComment", comment);
            return Success("Ok");
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult>GetCommentByProduct(int productId)
        {
           
            var comments = await _commentService.GetCommentbyProductId(productId);
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}"; // Lấy domain hiện tại, ví dụ: http://mydomain.com
            foreach (var comment in comments)
            {
                if (comment.MediaUrls != null && comment.MediaUrls.Any())
                {
                    comment.MediaUrls = comment.MediaUrls
                        .Select(url => url.StartsWith("http") ? url : $"{baseUrl}{url}")
                        .ToList();
                }
            }
            return Success(comments);
        }
      
    }
}
