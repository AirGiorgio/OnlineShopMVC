# OnlineShopMVC

Żeby zrozumieć istote problemu należy porównać klasę Product i ProductDetailsDTO.


 [...]

 
 public class ProductDetailsDTO : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductCategory { get; set; }   - kombinatorstwo zamiast obiektu CategoryDTO to jego Id 
        public CategoryDTO Category { get; set; }  - w założeniu kategoria do której należy produkt, która nie przesyła się jako obiekt
        public List<CategoryDTO> Categories { get; set; } - w założeniu wszystkie możliwe do wyboru kategorie w <select></select>
        public List<int> ProductTags { get; set; }  - kombinatorstwo zamiast Listy<TagDTO> to Lista<int> TagsId 
        public List<TagDTO> TagIds { get; set; }   - w założeniu tagi które teraz opisują produkt 
        public List<TagDTO> Tags { get; set; }  - w założeniu wszystkie możliwe do wyboru tagi w <select></select>, które nie przesyłają się jako lista

        [...]
     }
      public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public virtual Category? Category { get; set; }                -kategoria do której należy produkt
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<Tag> Tags { get; set; }                               -tagi które opisują produkt
    }

Następnie przejdźmy do widoku ProductDetailsDTO w folderze AdminProduct, interesują nas poniższe części

@using OnlineShopMvc.App.DTOs.ProductDTOs
@model ProductDetailsDTO

[...]

<label for="category">Kategoria:</label>
       <select id="category" asp-for="ProductCategory">
            @foreach (var category in Model.Categories)
            {
                  <option value="@category.Id" selected="@(category.Id == Model.ProductCategory ? "selected" : null)">@category.Name</option>
            }
       </select>
        </div>
        <div>
            <label for="productTags">Tagi (zaznaczając ctrl):</label>
        <select asp-for="ProductTags" multiple>
            @foreach (var tag in Model.Tags)
            {
                <option value="@tag.Id" selected="@(Model.ProductTags.Any(t => t == tag.Id) ? "selected" : null)">@tag.Name</option>
            }
        </select>
        </div>
        <button type="submit">Zatwierdź</button>

Wysyłam z widoku value= Category.Id oraz value= tag.Id, co ma swoje odzwierciedlenie w ProductDetailsDTO CategoryId i List<int> Tag,
ponieważ value=category i value=tag okazały się nullem

Zobaczmy co mamy kontrolerze UpdateProducts AdminProductController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct( ProductDetailsDTO product)
        {
            ModelState.Remove("Tag");
            ModelState.Remove("Category");
            ModelState.Remove("Tags");
            ModelState.Remove("Categories");
            if (!ModelState.IsValid)
            {
                var vproduct = _productService.PrepareModel();
                product.Tags = vproduct.Tags;
                product.Categories = vproduct.Categories;
                TempData["Message"] = "Kategorie lub tagi są niepoprawnie wybrane";
                return View("ProductDetails", product);
            }
            _logger.LogInformation("W UpdateProduct");
            var status = _productService.UpdateProduct(product);
       
            TempData["Message"] = status;
            return RedirectToAction("ViewProducts");
        }****

ProductDetailsDTO product wysłany z widoku składa się z:

product.Id = poprawnie przesłany
product.Name = poprawnie przesłany
product.Price - poprawnie przesłany 
product.Quantity - poprawnie przesłany
product.CategoryId - poprawnie przesłany z comboboxa
product.TagIds - poprawnie przesłany z comboboxa
product.ProductCategory - null
product.ProductTags - null
product.Categories - null
product.Tags - null

W związku z czym usuwam przed warunkiem ModelState 
            ModelState.Remove("TagIds");
            ModelState.Remove("Category");
            ModelState.Remove("Tags");
            ModelState.Remove("Categories");

Następnie, jeżeli któraś z wartości wpisanych przez użytkownika sie nie zgadza i trzeba wysłać produkt z powrotem do widoku, należy go uzupełnić,
bo iteracja w widoku na product.Categories jako null zakończy sie błędem
   
                var vproduct = _productService.PrepareModel();
                product.Tags = vproduct.Tags;
                product.Categories = vproduct.Categories;

Reszta kombinatorstwa w serwisach nie ma znaczenia, bo sam mogę to poprawić, gdyby z widoku poprawnie przesłano powyższe obiekty.
