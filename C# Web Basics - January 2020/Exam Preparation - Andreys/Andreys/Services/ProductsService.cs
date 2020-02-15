using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }


        public int Add(ProductAddInputModel productAddInputModel)
        {
            var genderAsEnum = Enum.Parse<Gender>(productAddInputModel.Gender);
            var categoryAsEnum = Enum.Parse<Category>(productAddInputModel.Category);

            Product product = new Product()
            {
                Name = productAddInputModel.Name,
                Description = productAddInputModel.Description,
                ImageUrl = productAddInputModel.ImageUrl,
                Price = productAddInputModel.Price,
                Gender = genderAsEnum,
                Category = categoryAsEnum,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();

            return product.Id;
        }

        public IEnumerable<Product> GetAll() => this.db.Products.Select(x => new Product
        { 
            Id = x.Id,
           Name = x.Name,
           ImageUrl = x.ImageUrl,
           Price =  x.Price
        }).
            ToArray();

        public Product GetById(int id) => this.db.Products.FirstOrDefault(x => x.Id == id);

        public void DeleteId(int id)
        {
            var product = this.GetById(id);
            //var product = this.db.Products.FirstOrDefault(x => x.Id == id);
            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }
    }

}
