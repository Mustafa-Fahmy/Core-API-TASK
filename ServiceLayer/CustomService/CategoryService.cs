using AutoMapper;
using DomainLayer.Models;
using RepositoryLayer.Infrastructure;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;

namespace ServiceLayer.CustomService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public JsonReturn GetCategories()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();
            JsonReturn jsonReturn = new JsonReturn { success = false,Message="No Data Found" };
            if (categories.Any())
            {
                jsonReturn.success = true;
                jsonReturn.Message = String.Empty;
                jsonReturn.results = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            }
            return jsonReturn;
        }

    }
}
