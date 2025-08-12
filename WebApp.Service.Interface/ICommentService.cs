using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Interface
{
    public interface ICommentService
    {
        Task<CommentViewModel> AddComment(CommentCreateRequest request);
        Task<List<CommentViewModel>> GetCommentbyProductId(int productId);
    }
}
