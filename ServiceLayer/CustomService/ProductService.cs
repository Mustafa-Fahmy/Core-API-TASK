using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using RepositoryLayer.Infrastructure;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;

namespace ServiceLayer.CustomService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public JsonReturn GetAllProducts(PagingInputs pagingInputs)
        {
            var products = _unitOfWork.ProductRepository.GetAll()
                .OrderBy(x => x.Id)
                .Skip((pagingInputs.PageIndex - 1) * pagingInputs.PageSize)
                .Take(pagingInputs.PageSize).ToList();
            JsonReturn jsonReturn = new JsonReturn { success = false , Message= "No Data Found" , };
            if (products.Any())
            {
                jsonReturn.success = true;
                var result = _mapper.Map<List<Products>, List<ProductsDto>>(products);
                jsonReturn.results = result;
                jsonReturn.Message = String.Empty;

            }
            return jsonReturn;
        }
        public JsonReturn GetProductById(int id)
        {
            var obj = _unitOfWork.ProductRepository.Get(id);
            JsonReturn jsonReturn = null;
            if (obj == null)
            {
                jsonReturn = new JsonReturn() { Message = "No Data Found", success = false };
            }
            else
            {
                jsonReturn = new JsonReturn() { results = _mapper.Map<ProductsDto>(obj), success = true };
            }
            return jsonReturn;
        }
        public CustomJsonReturn CreateProduct(int categoryId, ProductsForCreationsDto ProductsForCreationsDto)
        {
            CustomJsonReturn customJsonReturn = new CustomJsonReturn();
            customJsonReturn.JsonReturn = new JsonReturn() {success=false,Message= "Category is not found" };
            var category = _unitOfWork.CategoryRepository.Get(categoryId);
            if (category != null)
            {

                var products = _mapper.Map<Products>(ProductsForCreationsDto);
                _unitOfWork.ProductRepository.Insert(products);
                _unitOfWork.SaveChanges();
                customJsonReturn.JsonReturn.success = true;
                customJsonReturn.JsonReturn.Message = String.Empty;
                customJsonReturn.JsonReturn.results = _mapper.Map<ProductsDto>(products);
            }
            
            return customJsonReturn;
        }
        public JsonReturn UpdateProduct(int id, ProductsForUpdateDto productsForUpdateDto)
        {
            JsonReturn jsonReturn = new JsonReturn() { success = true };
            var category = _unitOfWork.CategoryRepository.Get(productsForUpdateDto.CategoryID);
            if (category != null)
            {

                var product = _unitOfWork.ProductRepository.Get(id);
                if (product != null)
                {
                    _mapper.Map(productsForUpdateDto, product);
                    _unitOfWork.ProductRepository.Update(product);
                }
                else
                {
                    jsonReturn.success = false;
                    jsonReturn.Message = "Product is not found";

                }
            }
            else
            {
                jsonReturn.success = false;
                jsonReturn.Message = "Category is not found";

            }

            return jsonReturn;
        }
        public bool DeleteProduct(int id)
        {
            bool isFound = true;
            var product = _unitOfWork.ProductRepository.Get(id);
            if (product != null)
            {
                isFound = true;
                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.SaveChanges();
            }
            return isFound;
        }

        public bool PartiallyUpdateProducts(int id, JsonPatchDocument<ProductsForUpdateDto> productsPatch)
        {
            bool isFound = default(bool);
            var product = _unitOfWork.ProductRepository.Get(id);
            if (product != null)
            {
                var productsDTO = _mapper.Map<ProductsForUpdateDto>(product);
                productsPatch.ApplyTo(productsDTO);
                _mapper.Map(productsDTO, product);
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.SaveChanges();
                isFound = true;
            }
            return isFound;

        }
        public JsonReturn GetProductByCategoryId(int caterogyId)
        {
            JsonReturn jsonReturn = new JsonReturn() { success = false, Message= "Category is not found" };
            var allProducts = _unitOfWork.ProductRepository.GetAll();
            if (allProducts != null && allProducts.Any())
            {

                var product = allProducts.Where(x => x.CategoryID == caterogyId).ToList();
                jsonReturn.success = true;
                jsonReturn.Message = String.Empty;
                jsonReturn.results = _mapper.Map<ProductsDto>(product);
            }
          
            return jsonReturn;
        }
    }
}
